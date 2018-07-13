namespace XamarinIoTWorkshop.SyncFusion.Shared
{
    public static class SyncfusionServices
    {
        public static void InitializeSyncfusion()
        {
#if __IOS__
            Syncfusion.SfGauge.XForms.iOS.SfGaugeRenderer.Init();
#endif
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense(SyncfusionConstants.LicenseKey);
        }
    }
}
