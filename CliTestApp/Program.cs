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
                Patient = new Person() { 
                    Identifiers = new [] { 
                        new Identifier() { Authority = Authorities.BG_GRAO, Value = "8005071254" } },
                    GivenName = "Ivan",
                    MiddleName = "Petrov", 
                    FamilyName = "Vasilev", 
                    DateOfBirth = new DateTime(1980, 5, 7),
                    IsMale = true,
                    Contacts = new[] { 
                        new Contact() { Type = ContactTypes.PHONE, Value = "0878133001" } }
                },
                Sevrices = new[] { 
                    new Service() { Id = new Identifier() { Authority = Authorities.LOINC, Value = "14749-6" }, Name = "Glucose" },
                    new Service() { Id = new Identifier() { Authority = Authorities.LOINC, Value = "54347-0" }, Name = "Albumin" } },
                Samples = new[] { 
                    new Sample() { 
                        TypeId = new Identifier() { Authority = Authorities.LOINC, Dictionary = Dictionaries.LOINC_0487_SampleType, Value = "SER" }, 
                        Id = new Identifier() { Authority = Authorities.LOCAL, Value = "S02F25" }, 
                        Taken = DateTime.Now.AddHours(-2) } },
                //LinkedReferrals = new[] { 
                //    new LinkedReferral() { 
                //        Id = new Identifier() { Authority = Authorities.BG_HIS, Value = "2023123456F2"} } }
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