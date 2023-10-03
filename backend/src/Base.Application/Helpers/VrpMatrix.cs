using Base.Domain.Entities;

namespace Base.Application.Helpers
{
    public static class VrpMatrix
    {
        public static double[,] GetDistanceMatrix(Location[] locations)
        {
            int rows = locations.Length;
            int cols = locations.Length;
            double[,] distanceMatrix = new double[rows, cols];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    double distanceBetweenTwoLocation = Calculations.CalculateDistanceBetweenTwoLocation(locations[i], locations[j]);
                    distanceMatrix[i, j] = distanceBetweenTwoLocation;
                }
            }

            return distanceMatrix;
        }
    }
}
