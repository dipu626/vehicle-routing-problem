using Base.Application.Helpers;
using Base.Domain.Entities;
using CVRP.Application.Dtos;
using CVRP.Application.Solver;
using CVRP.Domain.Entities;
using CVRP.Presentation.Test;

internal class Program
{
    private static async Task Main(string[] args)
    {
        Console.WriteLine("Hello, World!");

        var connectionTest = new ConnectionTest();
        connectionTest.Test();

        var dhaka = new BaseLocation() { Lattitude = 23.810331, Longitude = 90.412521 };

        var khulna = new BaseLocation() { Lattitude = 22.820000, Longitude = 89.550003 };
        var satkhira = new BaseLocation() { Lattitude = 22.723406, Longitude = 89.075127 };
        var jeshore = new BaseLocation() { Lattitude = 23.170664, Longitude = 89.212418 };
        
        var chattogram = new BaseLocation() { Lattitude = 22.341900, Longitude = 91.815536 };
        var chadpur = new BaseLocation() { Lattitude = 23.232100, Longitude = 90.663078 };
        var rangamati = new BaseLocation() { Lattitude = 22.6533, Longitude = 92.1789 };
        
        var sylhet = new BaseLocation() { Lattitude = 24.894802, Longitude = 91.869034 };
        var kishoreganj = new BaseLocation() { Lattitude = 24.433123, Longitude = 90.786568 };
        
        var rangpur = new BaseLocation() { Lattitude = 25.744860, Longitude = 89.275589 };
        var saidpur = new BaseLocation() { Lattitude = 25.778522, Longitude = 88.897377 };
        var rajshahi = new BaseLocation() { Lattitude = 24.363588, Longitude = 88.624138 };
        var pabna = new BaseLocation() { Lattitude = 24.006355, Longitude = 89.249298 };
        var jamalpur = new BaseLocation() { Lattitude = 24.923025, Longitude = 89.950111 };

        var request = new CvrpRequest
        {
            Deput = 0,
            Vehicles = 3,
            VehicleCapacities = new long[] { 20, 20, 20 },
            Locations = new BaseLocation[]
            {
                dhaka,
                khulna,
                kishoreganj,
                sylhet,
                rangpur,
                saidpur,
                satkhira,
                chattogram,
                chadpur,
                rangamati,
                jeshore,
                rajshahi,
                pabna,
                jamalpur,
            },
            Demands = new[] { 0L, 1L, 1L, 1L, 1L, 1L, 4L, 1L, 1L, 5L, 1L, 1L, 1L, 1L }
        };

        // Test: calc distance (Test OK)
        //var khulnaToSatkhira = await Calculations.GetDistanceAsync(khulna, satkhira);

        Dictionary<int, string> map = new Dictionary<int, string>
        {
            {0, "dhaka" },
            {1,"khulna"},
            {2,"kishoreganj"},
            {3,"sylhet"},
            {4,"rangpur"},
            {5,"saidpur"},
            {6,"satkhira"},
            {7,"chattogram"},
            {8,"chadpur"},
            {9,"rangamati"},
            {10,"jeshore"},
            {11,"rajshahi"},
            {12,"pabna"},
            {13,"jamalpur"},
        };

        var cvrpSolver = new CvrpSolver();
        var response = await cvrpSolver.GetCvrpSolutionAsync(request);

        Console.WriteLine($"{nameof(CvrpResponse.TotalDistance)}: {response.TotalDistance}");
        Console.WriteLine($"{nameof(CvrpResponse.TotalLoad)}: {response.TotalLoad}");

        foreach (BaseRoute route in response.Routes)
        {
            Console.WriteLine($"{nameof(route.Vehicle)}: {route.Vehicle}");

            foreach (var node in route.Nodes)
            {
                Console.Write($"({map[node.Node]} {node.Distance} {node.Load}) ");
            }
            Console.WriteLine("");

            Console.WriteLine($"{nameof(route.RouteDistance)}: {route.RouteDistance}");
            Console.WriteLine($"{nameof(route.RouteLoad)}: {route.RouteLoad}");

            Console.WriteLine("");
        }
    }
}