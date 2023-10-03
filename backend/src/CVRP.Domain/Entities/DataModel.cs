namespace CVRP.Domain.Entities
{
    public class DataModel
    {
        public double[,] DistabceMatrix { get; set; } = new double[0, 0];
        public long[] Demands { get; set; } = Array.Empty<long>();
        public int Vehicles { get; set; } = 0;
        public long[] VehicleCapacities { get; set; } = Array.Empty<long>();
        public int Deput { get; set; } = 0;
    }
}
