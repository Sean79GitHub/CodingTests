using TemperatureConverterDomain.Interfaces;

namespace TemperatureConverterDomain
{
    public class TemperatureConverterCalculator : ITemperatureConverterCalculator
    {
        public double ConvertCelsiusToFahrenheit(double celsiusValue)
        {
            double result = celsiusValue * (9.0d / 5.0d) + 32.0d;

            return result;
        }

        public double ConvertFahrenheitToCelsius(double fahrenheitValue)
        {
            double result = (fahrenheitValue - 32) * (5.0d / 9.0d);

            return result;
        }
    }
}
