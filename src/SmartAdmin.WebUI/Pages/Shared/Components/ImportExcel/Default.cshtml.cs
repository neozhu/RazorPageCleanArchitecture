using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SmartAdmin.WebUI.Pages.Shared.Components.ImportExcel
{
    public class DefaultModel 
    {
        [BindProperty, Display(Name = "File")]
        public IFormFile UploadedFile { get; set; }
        public string ImportUri { get; set; }
        public string AntiForgeryToken { get; set; }
        public string OnImportedSucceeded { get; set; }
    }
}
