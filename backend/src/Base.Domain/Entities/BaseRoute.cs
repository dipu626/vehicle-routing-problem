using Base.Domain.Entities;

namespace CVRP.Domain.Entities
{
    public class BaseRoute
    {
        public int Vehicle { get; set; } = 0;
        public long RouteDistance { get; set; } = 0;
        public long RouteLoad { get; set; } = 0;
        public List<BaseNode> Nodes { get; set; } = new List<BaseNode>();
    }
}
