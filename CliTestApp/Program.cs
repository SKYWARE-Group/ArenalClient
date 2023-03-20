using IdentityModel.Client;
using Microsoft.Extensions.Configuration;
using Skyware.Arenal.Client;
using Skyware.Arenal.Model;
using Skyware.Arenal.Model.Exceptions;

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
            try
            {
                Order respOrd = await client.CreateOrdersAsync(order);
                Console.WriteLine($"Order created, ArenalId is: {respOrd.ArenalId}");
            }
            catch (ArenalException ex)
            {
                Console.WriteLine($"Error: {ex.CombinedMessage()}");
            }


            //Other interactions with Arenal

        }


        private static Order GetDemoOrder()
        {

            return new Order(
                Workflows.LAB_SCO,
                new Patient()
                {
                    Identifiers = new[] { new Identifier() { Authority = Authorities.BG_GRAO, Value = "7505051234" } },
                    GivenName = "Борис", MiddleName = "Иванов", FamilyName = "Хаджийски",
                    DateOfBirth = new DateTime(1975, 5, 5), IsMale = true,
                    Contacts = new[] { new Contact() { Type = ContactTypes.PHONE, Value = "0878005006" } }
                },
                new[] { new Service("14749-6", "Глюкоза"), },
                new[] { new Sample("SERUM", null, "S05FT5") }
            );

        }



    }
}