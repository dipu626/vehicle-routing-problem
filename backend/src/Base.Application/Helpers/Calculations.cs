using Base.Domain.Entities;
using GoogleApi;
using GoogleApi.Entities.Maps.Common;
using GoogleApi.Entities.Maps.DistanceMatrix.Request;

namespace Base.Application.Helpers
{
    public static class Calculations
    {
        private const long EarthRadius = 6371;

        public static double CalculateDistanceBetweenTwoLocation(BaseLocation from, BaseLocation to)
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

        public static double CaclculateDistanceBetweenTwoPoint(BaseLocation from, BaseLocation to)
        {
            return Math.Abs(from.Lattitude - to.Lattitude) + Math.Abs(from.Longitude - to.Longitude);
        }

        public static async Task<double> GetDistanceAsync(BaseLocation from, BaseLocation to)
        {
            var request = new DistanceMatrixRequest
            {
                Origins = new List<LocationEx>
                {
                    new LocationEx(coordinate: new CoordinateEx(latitude: from.Lattitude, longitude: from.Longitude))
                },
                Destinations = new List<LocationEx>
                {
                    new LocationEx(coordinate: new CoordinateEx(latitude:to.Lattitude, longitude:to.Longitude))
                },
                Key = "AIzaSyD3GG7Qq1XgRMAcjPejT9spgnR4RZ9xzbU"
            };

            var response = await GoogleMaps.DistanceMatrix.QueryAsync(request);

            return response.Rows.First().Elements.First().Distance.Value;
        }
    }
}
