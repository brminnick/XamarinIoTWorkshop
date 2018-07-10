using System;

using Xamarin.Forms;

namespace XamarinIoTWorkshop
{
    public static class AppCenterConstants
    {
        public static string AppCenterApiKey => GetAppCenterApiKey();

        const string AppCenterApiKey_iOS = "e7b15fd0-4a6a-49cf-9e6d-c965470c3739";
        const string AppCenterApiKey_Android = "ee48e083-26d6-4516-b843-82afdff026f1";

        static string GetAppCenterApiKey()
        {
            switch (Device.RuntimePlatform)
            {
                case Device.iOS:
                    return AppCenterApiKey_iOS;
                case Device.Android:
                    return AppCenterApiKey_Android;
                default:
                    throw new NotSupportedException();
            }
        }
    }
}
