using System.Globalization;
using System.Threading;
using TemperatureConverter.ViewModels;

namespace TemperatureConverter
{
    public class AppBootStrapper : TypedAutofacBooTStrapper<TemperatureConverterViewModel>
    {
        public AppBootStrapper()
        {
            Configure();

            Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-GB");
        }
    }
}