using System.Globalization;

namespace SmartAdmin.WebUI.Pages.Shared.Components.LanguageSelector
{

    public class DefaultModel
    {
        public CultureInfo CurrentUICulture { get; set; }
        public List<CultureInfo> SupportedCultures { get; set; }
    }

}
