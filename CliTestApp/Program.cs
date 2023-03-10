using IdentityModel.Client;
using Microsoft.Extensions.Configuration;
using Skyware.Arenal.Client;
using Skyware.Arenal.Model;

namespace CliTestApp
{
    public class Program
    {

        //Holds session-wide JWT
        private static TokenResponse? _tokenResponse = null;

        /// <summary>
        /// Demonstrates how to obtain and cache JWT.
        /// </summary>
        /// <returns></returns>
        private static async Task GetTokenAsync(IConfiguration cfg)
        {
            if (_tokenResponse == null)
            {
                using var client = new HttpClient();
                //Authenticate
                _tokenResponse = await client.RequestPasswordTokenAsync(new PasswordTokenRequest
                {
                    Address = cfg["OpenIdProvider:Address"],
                    ClientId = cfg["OpenIdProvider:ClientId"],
                    ClientSecret = cfg["OpenIdProvider:ClientSecret"],
                    Scope = cfg["OpenIdProvider:Scope"],
                    UserName = cfg["OpenIdProvider:Username"],
                    Password = cfg["OpenIdProvider:Password"]
                });
            }
        }

        public static async Task Main(string[] args)
        {
            var builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            builder.AddUserSecrets<Program>();
            IConfiguration config = builder.Build();

            //Get JWT
            await GetTokenAsync(config);

            //Create Demo Order
            Order order = GetDemoOrder();

            //Publish Demo Order
            using var client = new HttpClient();
            client.SetBearerToken(_tokenResponse?.AccessToken);
            Order respOrd = await client.CreateOrdersAsync(order);
            Console.WriteLine($"Order created, ArenalId is: {respOrd.ArenalId}");

            //Other interactions with Arenal

        }


        private static Order GetDemoOrder()
        {

            return new Order()
            {
                Patient = new Person()
                {
                    Identifiers = new[] {
                        new Identifier() { Authority = Authorities.BG_GRAO, Value = "8006061234" } },
                    GivenName = "Ivan",
                    MiddleName = "Petrov",
                    FamilyName = "Vasilev",
                    DateOfBirth = new DateTime(1980, 6, 6),
                    IsMale = true,
                    Contacts = new[] {
                        new Contact() { Type = ContactTypes.PHONE, Value = "0888123123" } }
                },
                Sevrices = new[] {
                    new Service() { Id = new Identifier() { Authority = Authorities.LOINC, Value = "14749-6" }, Name = "Glucose" },
                    new Service() { Id = new Identifier() { Authority = Authorities.LOINC, Value = "54347-0" }, Name = "Albumin" } },
                Samples = new[] {
                    new Sample() {
                        TypeId = new Identifier() { Authority = Authorities.HL7, Dictionary = Dictionaries.HL7_0487_SampleType, Value = "SER" },
                        Id = new Identifier() { Authority = Authorities.LOCAL, Value = "S02F25" },
                        Taken = DateTime.Now.AddHours(-2) } },
            };

        }

        private static void DoConst()
        {
            Console.WriteLine(string.Join(Environment.NewLine, Helpers.GetAllConstants(typeof(Authorities))));
        }


    }
}