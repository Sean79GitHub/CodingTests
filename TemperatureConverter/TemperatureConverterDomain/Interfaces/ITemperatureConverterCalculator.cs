namespace TemperatureConverterDomain.Interfaces
{
    public interface ITemperatureConverterCalculator
    {
        double ConvertCelsiusToFahrenheit(double celsiusValue);
        double ConvertFahrenheitToCelsius(double fahrenheitValue);
    }
}