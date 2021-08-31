using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanArchitecture.Razor.Infrastructure.Constants.Localization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SmartAdmin.WebUI.Models;
using SmartAdmin.WebUI.Pages.Shared.Components.LanguageSelector;

namespace SmartAdmin.WebUI.ViewComponents
{
    public class LanguageSelectorViewComponent : ViewComponent
    {
        private readonly IOptions<RequestLocalizationOptions> _localizationOptions;

        public LanguageSelectorViewComponent(
            IOptions<RequestLocalizationOptions> localizationOptions
            )
        {
            _localizationOptions = localizationOptions;
        }
        public IViewComponentResult Invoke()
        {
            var cultureFeature = HttpContext.Features.Get<IRequestCultureFeature>();
            var model = new DefaultModel
            {
                SupportedCultures = _localizationOptions.Value.SupportedUICultures.ToList(),
                CurrentUICulture = cultureFeature.RequestCulture.UICulture
            };
            return View(model);
        }
    }
}
