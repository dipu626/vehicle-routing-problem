using Base.Application.Helpers;
using Base.Domain.Entities;
using CVRP.Application.Dtos;
using CVRP.Domain.Entities;
using Google.OrTools.ConstraintSolver;

namespace CVRP.Application.Solver
{
    public class CvrpSolver
    {
        public async Task<CvrpResponse> GetCvrpSolutionAsync(CvrpRequest cvrpEntity)
        {
            var distanceMatrix = await VrpMatrix.GetDistanceMatrixAsync(cvrpEntity.Locations); 

            // Instantiate the data problem
            var data = new DataModel()
            {
                DistanceMatrix = distanceMatrix,
                Vehicles = cvrpEntity.Vehicles,
                //VehicleCapacities = cvrpEntity.VehicleCapacities,
                //Demands = cvrpEntity.Demands,
                Deput = cvrpEntity.Deput,
            };

            // Create Routing Index Manager
            var manager = new RoutingIndexManager(num_nodes: data.DistanceMatrix.GetLength(0),
                                                  num_vehicles: data.Vehicles,
                                                  depot: data.Deput);

            // Create Routing Model
            var routing = new RoutingModel(manager);

            // Create and register a transit callback
            var transitCallbackIndex = routing.RegisterTransitCallback((long fromIndex, long toIndex) =>
            {
                // Convert from routing VariableIndex to distance matrix NodeIndex
                var fromNode = manager.IndexToNode(fromIndex);
                var toNode = manager.IndexToNode(toIndex);

                // Returns the distance between two nodes
                return (long)data.DistanceMatrix[fromNode, toNode];
            });

            // Define cost of each arc
            routing.SetArcCostEvaluatorOfAllVehicles(transitCallbackIndex);

            //// Add Capacity Constraints
            //var demandCallbackIndex = routing.RegisterUnaryTransitCallback((long fromIndex) =>
            //{
            //    // Convert from routing VariableIndex to demands NodeIndex
            //    var fromNode = manager.IndexToNode(fromIndex);

            //    // Returns the demand of the node
            //    return data.Demands[fromNode];
            //});

            // Add distance constraints with Vehicle Capacity
            routing.AddDimension(evaluator_index: transitCallbackIndex,
                                 slack_max: 0,
                                 capacity: 30000000,
                                 fix_start_cumul_to_zero: true,
                                 name: "Distance");

            var distanceDimension = routing.GetMutableDimension("Distance");
            distanceDimension.SetGlobalSpanCostCoefficient(coefficient: 2);

            // Setting first solution heuristic
            var searchParameters = operations_research_constraint_solver.DefaultRoutingSearchParameters();
            searchParameters.FirstSolutionStrategy = FirstSolutionStrategy.Types.Value.PathCheapestArc;

            // Solve the problem
            var solution = routing.SolveWithParameters(searchParameters);

            if (solution is not null)
            {
                return PrintSolution(data: data,
                                     manager: manager,
                                     routing: routing,
                                     solution: solution);
            }

            throw new Exception("Error, couldn't find solution.");
        }

        public CvrpResponse PrintSolution(DataModel data, RoutingIndexManager manager, RoutingModel routing, Assignment solution)
        {
            var result = new CvrpResponse();

            for (int vehicle = 0; vehicle < data.Vehicles; vehicle++)
            {
                var index = routing.Start(vehicle);
                var path = new BaseRoute
                {
                    Vehicle = vehicle,
                };
                var routeLoad = 0L;
                var routeDistance = 0L;

                while (!routing.IsEnd(index))
                {
                    var nodeIndex = manager.IndexToNode(index);
                    //var currentNodeLoad = data.Demands[nodeIndex];

                    var previousIndex = index;
                    index = solution.Value(routing.NextVar(index));

                    var currentArcDistance = routing.GetArcCostForVehicle(previousIndex, index, 0);

                    path.Nodes.Add(new BaseNode
                    {
                        Node = nodeIndex,
                        //Load = currentNodeLoad,
                        //Distance = currentArcDistance
                    });

                    //routeLoad += currentNodeLoad;
                    routeDistance += currentArcDistance;
                }

                result.TotalDistance += routeDistance;
                result.TotalLoad += routeLoad;
                result.Routes.Add(path);
            }

            return result;
        }
    }
}
