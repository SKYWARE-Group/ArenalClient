using Skyware.Arenal.Filters;
using Skyware.Arenal.Model;

namespace ModelCli;

public class Program
{

    static void Main(string[] args)
    {

        Filter exp1 = new Filter(nameof(Order.Status), ValueComparisons.Equals, OrderStatuses.AVAILABLE)
            .And(nameof(Order.Created), ValueComparisons.GreaterThan, DateTime.Today.AddDays(-30));

        Filter exp2 = new Filter(nameof(Order.Version), ValueComparisons.GreaterThan, 0)
            .Or(nameof(Order.Workflow), ValueComparisons.Equals, Workflows.LAB_SCO);

        exp1.And(exp2);

        Console.WriteLine(exp1);

    }

}