using System;
using Xamarin.Forms;

namespace XamarinIoTWorkshop
{
    public static class AppCenterConstants
    {
        public static string AppCenterApiKey => Device.RuntimePlatform switch
        {
            Device.iOS => AppCenterApiKey_iOS,
            Device.Android => AppCenterApiKey_Android,
            _ => throw new NotSupportedException()
        };

        const string AppCenterApiKey_iOS = "e7b15fd0-4a6a-49cf-9e6d-c965470c3739";
        const string AppCenterApiKey_Android = "ee48e083-26d6-4516-b843-82afdff026f1";
    }
}
