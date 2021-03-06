using System.Reflection;
using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.Test;
using Microsoft.WindowsAzure.MobileServices.TestFramework;

namespace Microsoft.WindowsAzure.Mobile.Android.Test
{
    static class App
    {
        public static TestHarness Harness { get; private set; }

        static App()
        {
            CurrentPlatform.Init();

            Harness = new TestHarness();
            Harness.Platform = TestPlatform.XamarinAndroid;

            Harness.Reporter = Listener;
            Harness.LoadTestAssembly(typeof(FunctionalTestBase).Assembly);
            Harness.LoadTestAssembly(typeof(PushFunctional).Assembly);
        }

        public static readonly TestListener Listener = new TestListener();
    }
}