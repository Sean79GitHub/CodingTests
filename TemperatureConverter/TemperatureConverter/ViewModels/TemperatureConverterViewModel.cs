using Caliburn.Micro;
using TemperatureConverterDomain.Interfaces;

namespace TemperatureConverter.ViewModels
{
    public class TemperatureConverterViewModel : Screen
    {
        private readonly ILanguageSettingsManager languageSettingsManager;
        private readonly ITemperatureConverterCalculator temperatureConverterCalculator;
        private double celsius;
        private double fahrenheit;

        public double Celsius
        {
            get { return celsius; }
            set
            {
                celsius = value;
                NotifyOfPropertyChange(() => Celsius);
            }
        }

        public double Fahrenheit
        {
            get { return fahrenheit; }
            set
            {
                fahrenheit = value;
                NotifyOfPropertyChange(() => Fahrenheit);
            }
        }

        // Using ctor injection here from the Autofac container
        public TemperatureConverterViewModel
        (
            ITemperatureConverterCalculator temperatureConverterCalculator,
            ILanguageSettingsManager languageSettingsManager
        )
        {
            this.temperatureConverterCalculator = temperatureConverterCalculator;
            this.languageSettingsManager = languageSettingsManager;
        }

        public void ConvertCelsiusToFahrenheit()
        {
            Fahrenheit = temperatureConverterCalculator.ConvertCelsiusToFahrenheit(Celsius);
        }
        public void ConvertFahrenheitToCelsius()
        {
            Celsius = temperatureConverterCalculator.ConvertFahrenheitToCelsius(Fahrenheit);
        }

        public void ChangeLanguage(string language)
        {
            language = language.Replace("System.Windows.Controls.ComboBoxItem: ", "");  // Sorry, clearly this needs to be sorted out to take the selected value some hom

            languageSettingsManager.ChangeLanguage(language);
        }
    }
}
