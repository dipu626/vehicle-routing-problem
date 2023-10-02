using Base.Domain.Entities;

namespace CVRP.Domain.Entities
{
    public class CvrpEntity : BaseEntity
    {
        public int Deput { get; set; } = 0;
        public decimal[] VehicleCapacity { get; set; } = Array.Empty<decimal>();
        public decimal[] Lattitudes { get; set; } = Array.Empty<decimal>();
        public decimal[] Longitudes { get; set; } = Array.Empty<decimal>();
    }
}
