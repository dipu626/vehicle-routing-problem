namespace CVRP.Domain.Entities
{
    public class DataModel
    {
        public decimal[,] DistabceMatrix { get; set; } = new decimal[0, 0];
        public int Vehicles { get; set; } = 0;
        public int Deput { get; set; } = 0;
    }
}
