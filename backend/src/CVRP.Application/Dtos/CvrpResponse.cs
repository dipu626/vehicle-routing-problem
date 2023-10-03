using CVRP.Domain.Entities;

namespace CVRP.Application.Dtos
{
    public class CvrpResponse
    {
        public long TotalDistance { get; set; } = 0;
        public long TotalLoad { get; set; } = 0;
        public List<BaseRoute> Routes { get; set; } = new List<BaseRoute> { };
    }
}
