namespace XamarinIoTWorkshop
{
    public class GeolocationDataModel
    {
        public GeolocationDataModel(double latitude, double longitude) =>
            (Latitude, Longitude) = (latitude, longitude);

        public double Latitude { get; }
        public double Longitude { get; }
    }
}
