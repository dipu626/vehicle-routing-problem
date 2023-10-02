namespace Base.Application.Helpers
{
    public static class VrpMatrix
    {
        public static decimal[,] GetDistanceMatrix(decimal[] lattitudes, decimal[] longitudes)
        {
            int rows = lattitudes.Length;
            int cols = longitudes.Length;
            decimal[,] distanceMatrix = new decimal[rows, cols];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    decimal distanceBetweenTwoLocation = Math.Abs(lattitudes[i] - lattitudes[j]) + Math.Abs(longitudes[i] - longitudes[j]);
                    distanceMatrix[i, j] = distanceBetweenTwoLocation;
                }
            }

            return distanceMatrix;
        }
    }
}
