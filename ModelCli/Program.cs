using Skyware.Arenal.Filters;
using Skyware.Arenal.Model;

namespace ModelCli;

public class Program
{

    static void Main(string[] args)
    {
        //FilterExpression exp = new();

        Predicate p = new(nameof(Order.Doctor), Predicate.ValueComparisons.Equals, "123");

        Console.WriteLine(p);

    }

}