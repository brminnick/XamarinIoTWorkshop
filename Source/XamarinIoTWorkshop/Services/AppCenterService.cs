using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Crashes;
using Microsoft.AppCenter.Analytics;

namespace XamarinIoTWorkshop
{
    public static class AppCenterService
    {
        public static void Start() => Start(AppCenterConstants.AppCenterApiKey);

        [Conditional("DEBUG")]
        public static void CrashApp() => Crashes.GenerateTestCrash();

        public static void TrackEvent(string trackIdentifier, IDictionary<string, string>? table = null) =>
            Analytics.TrackEvent(trackIdentifier, table);

        public static void TrackEvent(string trackIdentifier, string key, string value)
        {
            if (string.IsNullOrWhiteSpace(key) && string.IsNullOrWhiteSpace(value))
                TrackEvent(trackIdentifier);
            else
                TrackEvent(trackIdentifier, new Dictionary<string, string> { { key, value } });
        }

        public static void Report(Exception exception,
                          IDictionary<string, string>? properties = null,
                          [CallerMemberName] string callerMemberName = "",
                          [CallerLineNumber] int lineNumber = 0,
                          [CallerFilePath] string filePath = "")
        {

            PrintException(exception, callerMemberName, lineNumber, filePath);

            Crashes.TrackError(exception, properties);
        }

        [Conditional("DEBUG")]
        static void PrintException(Exception exception, string callerMemberName, int lineNumber, string filePath)
        {
            var fileName = System.IO.Path.GetFileName(filePath);

            Debug.WriteLine(exception.GetType());
            Debug.WriteLine($"Error: {exception.Message}");
            Debug.WriteLine($"Line Number: {lineNumber}");
            Debug.WriteLine($"Caller Name: {callerMemberName}");
            Debug.WriteLine($"File Name: {fileName}");
        }

        static void Start(string appSecret) => AppCenter.Start(appSecret, typeof(Analytics), typeof(Crashes));
    }
}
