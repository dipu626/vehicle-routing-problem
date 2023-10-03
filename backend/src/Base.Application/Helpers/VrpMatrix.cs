using Base.Domain.Entities;
using System.Diagnostics;

namespace Base.Application.Helpers
{
    public static class VrpMatrix
    {
        public static async Task<double[,]> GetDistanceMatrixAsync(BaseLocation[] locations)
        {
            int rows = locations.Length;
            int cols = locations.Length;
            var distanceMatrix = new List<List<double>>();

            for (int i = 0; i < rows; i++)
            {
                var origins = new List<BaseLocation>();
                var destinations = locations.ToList();
                int total = 0;

                // This logic need to change in real project
                while (i < rows && total + cols < 100)
                {
                    origins.Add(locations[i]);
                    total += cols;
                    i++;
                }
                i--;

                var distanceBetweenLocations = await Calculations.GetDistanceAsync(origins, destinations);

                foreach (var item in distanceBetweenLocations)
                {
                    distanceMatrix.Add(item);
                }
                //for (int j = 0; j < cols; j++)
                //{
                //    //double distanceBetweenTwoLocation = Calculations.CalculateDistanceBetweenTwoLocation(locations[i], locations[j]);
                //    double distanceBetweenTwoLocation = await Calculations.GetDistanceAsync(locations[i], locations[j]);
                //    distanceMatrix[i, j] = distanceBetweenTwoLocation;

                //    Debug.Print($"{i} - {j} : {distanceBetweenTwoLocation}");
                //}
            }

            var result = new double[rows, cols];
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    result[i, j] = distanceMatrix[i][j];
                    Debug.Print($"{i} - {j} : {result[i, j]}");
                }
            }

            return result;
        }
    }
}
