using Base.Domain.Entities;

namespace Base.Application.Helpers
{
    public static class Calculations
    {
        private const long EarthRadius = 6371;

        public static double CalculateDistanceBetweenTwoLocation(Location from, Location to)
        {
            // Convert latitude and longitude from degrees to radians
            double latFromRad = DegreesToRadians(from.Lattitude);
            double lngFromRad = DegreesToRadians(from.Longitude);
            double latToRad = DegreesToRadians(to.Lattitude);
            double lngToRad = DegreesToRadians(to.Longitude);



            // Haversine formula
            double dLat = latToRad - latFromRad;
            double dLon = lngToRad - lngFromRad;
            double a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                       Math.Cos(latFromRad) * Math.Cos(latToRad) *
                       Math.Sin(dLon / 2) * Math.Sin(dLon / 2);
            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

            // Calculate the distance in kilometers
            double distanceKm = EarthRadius * c;

            return distanceKm;
        }

        private static double DegreesToRadians(double degrees)
        {
            return degrees * Math.PI / 180.0;
        }

        public static double CaclculateDistanceBetweenTwoPoint(Location from, Location to)
        {
            return Math.Abs(from.Lattitude - to.Lattitude) + Math.Abs(from.Longitude - to.Longitude);
        }
    }
}
