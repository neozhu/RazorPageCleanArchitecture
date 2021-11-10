using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CleanArchitecture.Razor.Application.Common.Extensions;
using CleanArchitecture.Razor.Application.Common.Interfaces;
using CleanArchitecture.Razor.Infrastructure.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Localization;
using System.Linq.Dynamic.Core;
using CleanArchitecture.Razor.Application.Common.Mappings;
using System.ComponentModel.DataAnnotations;
using CleanArchitecture.Razor.Application.Common.Models;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.IO;
using Microsoft.AspNetCore.Http;
using System.Data;
using System;
using CleanArchitecture.Razor.Infrastructure.Constants.Permission;
using System.ComponentModel;
using System.Reflection;
using CleanArchitecture.Razor.Infrastructure.Constants.ClaimTypes;

namespace SmartAdmin.WebUI.Areas.Authorization.Pages
{
    [Authorize(policy: Permissions.Roles.View)]
    public class RoleModel : PageModel
    {
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly IMapper _mapper;
        private readonly IExcelService _excelService;
        private readonly IStringLocalizer<RoleModel> _localizer;
        [BindProperty]
        public EditRoleModel Input { get; set; } = new();
        [BindProperty]
        public string NavJsonStr { get; set; }
        [BindProperty]
        public IEnumerable<string> AssignedPermissions { get; set; }
        [BindProperty]
        public string RoleId { get; set; }
        [BindProperty]
        public Dictionary<string, IEnumerable<PermissionModel>> GroupedPermissions { get; set; }=new ();
        [BindProperty]
        public IFormFile UploadedFile { get; set; }

        public RoleModel(
             RoleManager<ApplicationRole> roleManager,
            IMapper mapper,
            IExcelService excelService,
            IStringLocalizer<RoleModel> localizer
            )
        {
            _roleManager = roleManager;
            _mapper = mapper;
            _excelService = excelService;
            _localizer = localizer;
        }
        public  Task OnGetAsync()
        {
            var allPermissions = GetAllPermissions();
            foreach(var group in allPermissions.GroupBy(x => x.Group))
            {
                GroupedPermissions.Add(group.Key, group.ToArray());
            }
            return Task.CompletedTask;
        }
        public IActionResult OnGetNavgitation()
        {
            var jsonText = System.IO.File.ReadAllText("nav.json");
            return Content(jsonText);
        }
        public async  Task<IActionResult> OnPostUpdateNavgitationAsync()
        {
            try
            {
                //using (var sw = new StreamWriter(@"nav.json", false))
                //{
                //  await sw.WriteAsync(this.NavJsonStr);
                //  await  sw.FlushAsync();
                //}
                await Task.CompletedTask;
            }
            catch
            {

            }
            return RedirectToPage();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                if (Input.Id == string.Empty)
                {
                    var role=await _roleManager.FindByIdAsync(Input.Id);
                    role.Name=Input.Name;
                    role.Description=Input.Description;
                    var result= await _roleManager.UpdateAsync(role);
                    return new JsonResult(result.ToApplicationResult());
                }
                else
                {
                    var role = new ApplicationRole();
                    role.Name = Input.Name;
                    role.Description = Input.Description;
                    var result = await _roleManager.CreateAsync(role);
                    return new JsonResult(result.ToApplicationResult());
                }
                
            }
            catch (Exception e)
            {
                return BadRequest(Result.Failure(new string[] { e.Message }));
            }
        }
        public async Task<IActionResult> OnGetDataAsync(int page = 1, int rows = 15, string sort = "Name", string order = "asc", string filterRules = "")
        {
            var filters = PredicateBuilder.FromFilter<ApplicationRole>(filterRules);
            var data = await _roleManager.Roles.Where(filters)
                   .OrderBy($"{sort} {order}")
                   .PaginatedDataAsync(page, rows);
            return new JsonResult(data);
        }
        public async Task<IActionResult> OnGetDeleteAsync(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role.Name == "Admin")
            {
                return BadRequest(Result.Failure(new string[] { "Please do not delete the default role." }));
            }
            var result = await _roleManager.DeleteAsync(role);
            return new JsonResult(result.ToApplicationResult());
        }
        public async Task<IActionResult> OnGetDeleteCheckedAsync([FromQuery] string[] id)
        {
            foreach (var key in id)
            {
                var role = await _roleManager.FindByIdAsync(key);
                if (role.Name == "Admin")
                {
                    return BadRequest(Result.Failure(new string[] { "Please do not delete the default role." }));
                }
                var result = await _roleManager.DeleteAsync(role);
            }
            return new JsonResult(Result.Success());
        }
        public async Task<FileResult> OnPostExportAsync(string sort = "Name", string order = "asc", string filterRules = "")
        {
            var filters = PredicateBuilder.FromFilter<ApplicationRole>(filterRules);
            var data = await _roleManager.Roles.Where(filters)
                   .OrderBy($"{sort} {order}")
                   .ToListAsync();
            var result = await _excelService.ExportAsync(data, new Dictionary<string, System.Func<ApplicationRole, object>>()
            {
                { _localizer["Name"], item => item.Name },
                { _localizer["Description"], item => item.Description }
            }, _localizer["ApplicationRoles"]);
            return File(result, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", _localizer["ApplicationRoles"] + ".xlsx");
        }
        public async Task<FileResult> OnGetCreateTemplate()
        {
            var fields = new string[] {
                _localizer["Name"],
                _localizer["Description"]
              };
            var result = await _excelService.CreateTemplateAsync(fields, _localizer["ApplicationRoles"]);
            return File(result, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", _localizer["ApplicationRoles"] + ".xlsx");
        }
        public async Task<IActionResult> OnPostImportAsync()
        {
            try
            {
                var stream = new MemoryStream();
                await UploadedFile.CopyToAsync(stream);
                var data = stream.ToArray();
                var result = await _excelService.ImportAsync(data, mappers: new Dictionary<string, Func<DataRow, EditRoleModel, object>>
            {
                { _localizer["Name"], (row,item) => item.Name = row[_localizer["Name"]]?.ToString() },
                { _localizer["Description"], (row,item) => item.Description = row[_localizer["Description"]]?.ToString() },
            }, _localizer["ApplicationRoles"]);
                if (result.Succeeded)
                {
                    var importItems = result.Data;
                    foreach (var item in importItems)
                    {
                        var role = new ApplicationRole
                        {
                            Description= item.Description,
                            Name= item.Name
                        };
                        if (!(await _roleManager.RoleExistsAsync(role.Name)))
                        {
                            var iresult = await _roleManager.CreateAsync(role);
                            if (iresult.Succeeded == false)
                            {
                                return BadRequest(Result.FailureAsync(iresult.Errors.Select(x => x.Description)));
                            }
                        }
                    }
                }
                return new JsonResult(Result.Success());
            }
            catch (Exception e)
            {
                return BadRequest(Result.Failure(new string[] { e.Message }));
            }
        }

        public async Task<IActionResult> OnGetAssignedPermissionsAsync(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            var claims = await _roleManager.GetClaimsAsync(role);
            return new JsonResult(claims.Select(x=>x.Value));
        }
        public async Task<IActionResult> OnPostAssignPermissionsAsync()
        {
            var role = await _roleManager.FindByIdAsync(RoleId);
            var claims = await _roleManager.GetClaimsAsync(role);
            foreach (var claim in claims)
            {
               await  _roleManager.RemoveClaimAsync(role, claim);
            }
            foreach(var name in AssignedPermissions)
            {
                await _roleManager.AddClaimAsync(role, new System.Security.Claims.Claim(ApplicationClaimTypes.Permission, name));
            }
           
            return new JsonResult(Result.Success());
        }

        private IEnumerable<PermissionModel> GetAllPermissions()
        {
            var allPermissions = new List<PermissionModel>();
            var modules = typeof(Permissions).GetNestedTypes();

            foreach (var module in modules)
            {
                var moduleName = string.Empty;
                var moduleDescription = string.Empty;

                if (module.GetCustomAttributes(typeof(DisplayNameAttribute), true)
                    .FirstOrDefault() is DisplayNameAttribute displayNameAttribute)
                    moduleName = displayNameAttribute.DisplayName;

                if (module.GetCustomAttributes(typeof(DescriptionAttribute), true)
                    .FirstOrDefault() is DescriptionAttribute descriptionAttribute)
                    moduleDescription = descriptionAttribute.Description;

                var fields = module.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy);

                foreach (var fi in fields)
                {
                    var propertyValue = fi.GetValue(null);

                    if (propertyValue is not null)
                        allPermissions.Add(new PermissionModel { ClaimValue = propertyValue.ToString(), ClaimType = ApplicationClaimTypes.Permission, Group = moduleName, Description = moduleDescription });
                }
            }

            return allPermissions;
        }
        public class EditRoleModel
        {
            public string Id { get; set; }
            [Display(Name = "Name")]
            [Required]
            public string Name { get; set; }
            [Display(Name = "Description")]
            public string Description { get; set; }
        }
        public class PermissionModel {
            public string Description { get; set; }
            public string Group { get; set; }
            public string ClaimType { get; set; }
            public string ClaimValue { get; set; }
        }
    }
}
