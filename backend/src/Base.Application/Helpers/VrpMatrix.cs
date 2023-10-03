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
            double[,] distanceMatrix = new double[rows, cols];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    //double distanceBetweenTwoLocation = Calculations.CalculateDistanceBetweenTwoLocation(locations[i], locations[j]);
                    double distanceBetweenTwoLocation = await Calculations.GetDistanceAsync(locations[i], locations[j]);
                    distanceMatrix[i, j] = distanceBetweenTwoLocation;

                    Debug.Print($"{i} - {j} : {distanceBetweenTwoLocation}");
                }
            }

            return distanceMatrix;
        }
    }
}
