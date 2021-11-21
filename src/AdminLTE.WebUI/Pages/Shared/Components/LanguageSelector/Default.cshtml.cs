using System.Globalization;

namespace AdminLTE.WebUI.Pages.Shared.Components.LanguageSelector;

public class DefaultModel
{
    public CultureInfo CurrentUICulture { get; set; }
    public List<CultureInfo> SupportedCultures { get; set; }
}
