using Base.Domain.Entities;
using Google.OrTools.ConstraintSolver;
using GoogleApi;
using GoogleApi.Entities.Maps.Common;
using GoogleApi.Entities.Maps.DistanceMatrix.Request;
using System.Diagnostics;

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

        public static async Task<List<List<double>>> GetDistanceAsync(List<BaseLocation> froms, List<BaseLocation> tos)
        {
            var origins = new List<LocationEx>();
            foreach (var from in froms)
            {
                origins.Add(new LocationEx(coordinate: new CoordinateEx(latitude: from.Lattitude, longitude: from.Longitude)));
            }
            var destinations = new List<LocationEx>();
            foreach (var to in tos)
            {
                destinations.Add(new LocationEx(coordinate: new CoordinateEx(latitude: to.Lattitude, longitude: to.Longitude)));
            }

            var request = new DistanceMatrixRequest
            {
                Origins = origins,
                Destinations = destinations,
                Key = "AIzaSyD3GG7Qq1XgRMAcjPejT9spgnR4RZ9xzbU"
            };

            var response = await GoogleMaps.DistanceMatrix.QueryAsync(request);

            int rows = froms.Count;
            int columss = tos.Count;
            var distanceMatrix = new List<List<double>>();

            int i = 0;
            foreach (var row in response.Rows)
            {
                Debug.Print($"Row[{i++}]: ");
                int j = 0;
                var columDistances = new List<double>();
                foreach (var elements in row.Elements)
                {
                    var distance = elements.Distance?.Value ?? 0;
                    var duration = elements.Duration?.Value ?? 0;
                    var durationInTrafic = elements.DurationInTraffic?.Value ?? 0;
                    var fare = elements.Fare?.Value ?? 0;
                    var status = elements.Status;

                    columDistances.Add(distance);

                    //Debug.Print($"({j++}, {distance}, {duration}, {status})");
                }
                distanceMatrix.Add(columDistances);
            }

            return distanceMatrix;
            //return response.Rows.First().Elements.First().Distance.Value;
        }
    }
}
