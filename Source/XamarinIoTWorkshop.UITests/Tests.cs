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
        IApp _app;

        public Tests(Platform platform) => _platform = platform;

        [SetUp]
        public void BeforeEachTest() => _app = AppInitializer.StartApp(_platform);

        [Test]
        public void AppLaunches()
        {
            
            try
            {
                _app.WaitForElement(ButtonTextConstants.EndDataCollectionText);
            }
            catch
            {
                _app.WaitForElement(ButtonTextConstants.BeginDataCollectionText);
            }

            _app.Screenshot("App Launched");
        }
    }
}
