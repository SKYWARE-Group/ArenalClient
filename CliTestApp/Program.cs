using IdentityModel.Client;
using Microsoft.Extensions.Configuration;
using Skyware.Arenal.Client;
using Skyware.Arenal.Discovery;
using Skyware.Arenal.Model;
using Skyware.Arenal.Model.DocumentGeneration;
using Skyware.Arenal.Model.Exceptions;
using System.Diagnostics;

namespace CliTestApp
{
    public class Program
    {

        //Holds session-wide JWT
        private static TokenResponse? _tokenResponse = null;

        public static async Task Main(string[] args)
        {

            await GetFormAsync();

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

            ////Publish Demo Order
            using var client = new HttpClient();
            //client.SetBearerToken(_tokenResponse?.AccessToken);
            //try
            //{
            //    Order respOrd = await client.CreateOrdersAsync(order);
            //    Console.WriteLine($"Order created, ArenalId is: {respOrd.ArenalId}");
            //}
            //catch (ArenalException ex)
            //{
            //    Console.WriteLine($"Error: {ex.CombinedMessage()}");
            //}

            Order? readOrder = null;
            // Read one
            client.SetBearerToken(_tokenResponse?.AccessToken);
            try
            {
                readOrder = await client.GetOrderAsync("AD-O-A");
                Console.WriteLine($"Order {readOrder.ArenalId} is retrieved.");
            }
            catch (ArenalException ex)
            {
                Console.WriteLine($"Error: {ex.CombinedMessage()}");
            }

            //// Update
            //client.SetBearerToken(_tokenResponse?.AccessToken);
            //readOrder.Patient = new Patient() { GivenName = "UpdatedOrder" };
            //try
            //{
            //    readOrder = await client.UpdateOrdersAsync(readOrder);
            //    Console.WriteLine($"Order created, ArenalId is: {readOrder.ArenalId}");
            //}
            //catch (ArenalException ex)
            //{
            //    Console.WriteLine($"Error: {ex.CombinedMessage()}");
            //}

            ////delete
            //client.SetBearerToken(_tokenResponse?.AccessToken);
            //try
            //{
            //    await client.DeleteOrdersAsync(readOrder);
            //    Console.WriteLine($"Order deleted, ArenalId is: {readOrder.ArenalId}");
            //}
            //catch (ArenalException ex)
            //{
            //    Console.WriteLine($"Error: {ex.CombinedMessage()}");
            //}

            ////Other interactions with Arenal
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

        private static async Task GetFormAsync()
        {
            Compendium comp = new()
            {
                ProviderId = "misho",
                ServiceList = new[]
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