using Base.Domain.Entities;

namespace CVRP.Domain.Entities
{
    public class CvrpEntity : BaseEntity
    {
        public int Deput { get; set; } = 0;
        public int Vehicles { get; set; } = 0;
        public long[] VehicleCapacities { get; set; } = Array.Empty<long>();
        public Location[] Locations { get; set;} = Array.Empty<Location>();
        public long[] Demands { get; set; } = Array.Empty<long>();
    }
}
