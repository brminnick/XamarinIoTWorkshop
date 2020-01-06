using NUnit.Framework;
using Xamarin.UITest;
using XamarinIoTWorkshop.Shared;

namespace XamarinIoTWorkshop.UITests
{
    [TestFixture(Platform.Android)]
    [TestFixture(Platform.iOS)]
    public class Tests
    {
        readonly Platform _platform;

        public Tests(Platform platform) => _platform = platform;

        [Test]
        public void AppLaunches()
        {
            var app = AppInitializer.StartApp(_platform);

            try
            {
                app.WaitForElement(ButtonTextConstants.EndDataCollectionText);
            }
            catch
            {
                app.WaitForElement(ButtonTextConstants.BeginDataCollectionText);
            }

            app.Screenshot("App Launched");
        }
    }
}
