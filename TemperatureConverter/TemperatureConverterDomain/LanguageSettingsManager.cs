using System.Globalization;
using System.Threading;
using TemperatureConverterDomain.Interfaces;

namespace TemperatureConverterDomain
{
    public class LanguageSettingsManager : ILanguageSettingsManager
    {
        public void ChangeLanguage(string language)
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(language);
        }
    }
}
