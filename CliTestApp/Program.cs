using Skyware.Arenal.Model;
using System.Text.Json;

namespace CliTestApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //CaseGen();
            CaseJson1();
        }

        private static void CaseJson1()
        {
            Order o = new() { 
                ArenalId = "n1-0-as6g", 
                Patient = new Patient() { 
                    Identifiers = new [] { new Identifier() { Authority = "bg.egn", Value = "8005071254" } },
                    GivenName = "John", 
                    FamilyName = "Doe", 
                    DateOfBirth = new DateTime(1980, 5, 7),
                    IsMale = true
                },
                Sevrices = new[] { new Service() { Id = new Identifier() { Authority = "org.loinc", Value = "45685-8" }, Name = "Glucose" } }
            };
            Console.WriteLine(JsonSerializer.Serialize(
                o, 
                options: new JsonSerializerOptions() { 
                    WriteIndented = true, 
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase, 
                    DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull 
                }) + "\n");
        }

        private static void CaseGen()
        {
            int cnt = 0;
            Parallel.For(0, 100, (x) => { 
                Interlocked.Increment(ref cnt);
                Console.WriteLine(cnt);
            });
        }

    }
}