using IdentityModel.Client;
using Microsoft.Extensions.Configuration;
using Skyware.Arenal.Client;
using Skyware.Arenal.Discovery;
using Skyware.Arenal.Model;
using Skyware.Arenal.Model.Actions;
using Skyware.Arenal.Model.DocumentGeneration;
using Skyware.Arenal.Model.Exceptions;
using System.Diagnostics;
using System.Security.Cryptography;

namespace CliTestApp
{
    public class Program
    {

        //Holds session-wide JWT
        private static TokenResponse? _tokenResponse = null;

        public static async Task Main(string[] args)
        {

            //await GetFormAsync();
            //await DoOrderStuff();
            await DoOrganizationsStuff();
            //await ChangeOrderStatusDemo();
        }

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

        private static async Task DoOrderStuff()
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

            using var client = new HttpClient();
            OrderExtensions.BaseAddress = "https://localhost:7291/";


            // Create Order
            client.SetBearerToken(_tokenResponse?.AccessToken);

            Order? respOrd = null;
            try
            {
                respOrd = await client.CreateOrdersAsync(order);
                Console.WriteLine($"Order created, ArenalId is: {respOrd.ArenalId}");
            }
            catch (ArenalException ex)
            {
                Console.WriteLine($"Error: {ex.CombinedMessage()}");
                return;
            }

            // Read
            client.SetBearerToken(_tokenResponse?.AccessToken);
            Order? readOrder = null;
            try
            {
                readOrder = await client.GetOrderAsync(respOrd.ArenalId);
                Console.WriteLine($"Order {readOrder.ArenalId} is retrieved.");
            }
            catch (ArenalException ex)
            {
                Console.WriteLine($"Error: {ex.CombinedMessage()}");
                return;
            }

            // Update
            client.SetBearerToken(_tokenResponse?.AccessToken);
            readOrder.Patient = new Patient() { GivenName = "Миленов" };
            try
            {
                readOrder = await client.UpdateOrdersAsync(readOrder);
                Console.WriteLine($"Order updated, ArenalId is: {readOrder.ArenalId}");
            }
            catch (ArenalException ex)
            {
                Console.WriteLine($"Error: {ex.CombinedMessage()}");
                return;
            }

            // Delete
            client.SetBearerToken(_tokenResponse?.AccessToken);
            try
            {
                await client.DeleteOrdersAsync(readOrder);
                Console.WriteLine($"Order deleted, ArenalId is: {respOrd.ArenalId}");
            }
            catch (ArenalException ex)
            {
                Console.WriteLine($"Error: {ex.CombinedMessage()}");
            }

            //TODO: Other interactions with Arenal
        }

        private static async Task ChangeOrderStatusDemo()
        {
            var builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            builder.AddUserSecrets<Program>();
            IConfiguration config = builder.Build();

            //Get JWT
            await GetTokenAsync(config);

            using var client = new HttpClient();
            OrderExtensions.BaseAddress = "https://localhost:7291/";

            // Create Order
            Order changeStatOrder = GetDemoOrder();
            client.SetBearerToken(_tokenResponse?.AccessToken);

            Order? order = null;
            try
            {
                order = await client.GetOrderAsync("AD-O-L");
                Console.WriteLine($"Order taken, ArenalId is: {order.ArenalId}");
            }
            catch (ArenalException ex)
            {
                Console.WriteLine($"Error: {ex.CombinedMessage()}");
                return;
            }

            //Get Organization
            Organization provider = null;
            try
            {
                provider = await client.GetProviderAsync("AD-G-2");
                Console.WriteLine($"Organization taken, ArenalId is: {provider.ArenalId}");
            }
            catch (ArenalException ex)
            {
                Console.WriteLine($"Error: {ex.CombinedMessage()}");
                return;
            }

            OrderStatusRequest statusRequest = GetDemoStatusRequest(provider);

            //Change Status
            //Order changedStatusOrder = await client.ChangeOrderStatusAsync(order.ArenalId, statusRequest);
            Order changedStatusOrder = await client.ChangeOrderStatusAsync(order, statusRequest);
        }

        private static async Task DoOrganizationsStuff()
        {
            var builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            builder.AddUserSecrets<Program>();
            IConfiguration config = builder.Build();

            //Get JWT
            await GetTokenAsync(config);

            using var client = new HttpClient();
            OrderExtensions.BaseAddress = "https://localhost:7291/";

            client.SetBearerToken(_tokenResponse?.AccessToken);
            //Get Organization
            Organization provider = null;
            try
            {
                provider = await client.GetProviderAsync("AD-G-2");
                Console.WriteLine($"Organization taken, ArenalId is: {provider.ArenalId}");
            }
            catch (ArenalException ex)
            {
                Console.WriteLine($"Error: {ex.CombinedMessage()}");
                return;
            }

            IEnumerable<Organization> providers = null;
            try
            {
                providers = await client.GetProvidersAsync();
                foreach (var ctxProvider in providers)
                    Console.WriteLine($"Organization taken, ArenalId is: {ctxProvider.ArenalId}");
            }
            catch (ArenalException ex)
            {
                Console.WriteLine($"Error: {ex.CombinedMessage()}");
                return;
            }
        }

        private static Order GetDemoOrder()
        {

            return new Order(
                Workflows.LAB_SCO,
                new Patient()
                {
                    Identifiers = new[] { new Identifier() { Authority = Authorities.BG_GRAO, Value = "7505051234" } },
                    GivenName = "Борис",
                    MiddleName = "Иванов",
                    FamilyName = "Хаджийски",
                    DateOfBirth = new DateTime(1975, 5, 5),
                    IsMale = true,
                    Contacts = new[] { new Contact() { Type = ContactTypes.PHONE, Value = "0878005006" } }
                },
                new[] { new Service("14749-6", "Глюкоза"), },
                new[] { new Sample("SERUM", null, "S05FT5") }
            )
            {
                ProviderId = "AD-G-2"
            };

        }

        private static OrderStatusRequest GetDemoStatusRequest(Organization provider)
        {
            return new OrderStatusRequest()
            {
                NewStatus = OrderStatuses.TAKEN,
                ProviderNote = new Note()
                {
                    Value = "This is demo provider note."
                },
                ProviderId = provider.ArenalId
            };
        }

        private static async Task GetFormAsync()
        {
            Compendium comp = new()
            {
                ProviderId = "misho",
                Services = new[]
                {
                    new CompendiumEntry() { Name = "Glucose"},
                    new CompendiumEntry() { Name = "Albumin"},
                    new CompendiumEntry() { Name = "Cholesterol"},
                    new CompendiumEntry() { Name = "Phlebotomy"},
                }
            };

            using HttpClient client = new();
            OrderExtensions.BaseAddress = "https://arenal-forms.azurewebsites.net/";

            Stopwatch s = new();

            s.Start();
            DocumentAnswer ans = await client.GetFormAsync("bg.nhif.lab-referral", comp);
            s.Stop();

            await Console.Out.WriteLineAsync($"Form is generated in {s.ElapsedMilliseconds}ms.");

            byte[] pdfData = Convert.FromBase64String(ans.Data);


            string fn = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "arenal-jasper-demo.pdf");
            using var fs = new FileStream(fn, FileMode.Create, FileAccess.Write);
            await fs.WriteAsync(pdfData, 0, pdfData.Length);
            fs.Flush();
            fs.Close();

            Process.Start(new ProcessStartInfo() { CreateNoWindow = true, FileName = "cmd.exe", Arguments = $"/C start {fn}" });

        }

    }
}