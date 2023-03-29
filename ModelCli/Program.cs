using Skyware.Arenal.Filters;
using Skyware.Arenal.Model;

namespace ModelCli;

public class Program
{

    static void Main(string[] args)
    {
        FilterExpression exp1 = new FilterExpression(new Predicate(nameof(Order.Status), ValueComparisons.Equals, OrderStatuses.FREE))
            .And(new Predicate(nameof(Order.Created), ValueComparisons.GreaterThan, DateTime.Today.AddDays(-30)));

        FilterExpression exp2 = new FilterExpression(new Predicate(nameof(Order.Version), ValueComparisons.GreaterThan, 0))
            .Or(new Predicate(nameof(Order.Workflow), ValueComparisons.Equals, Workflows.LAB_SCO));

        exp1.And(exp2);

        Console.WriteLine(exp1);
    }

}