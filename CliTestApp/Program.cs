using IdentityModel.Client;
using Microsoft.Extensions.Configuration;
using Skyware.Arenal.Client;
using Skyware.Arenal.Discovery;
using Skyware.Arenal.Model;
using Skyware.Arenal.Model.Actions;
using Skyware.Arenal.Model.DocumentGeneration;
using Skyware.Arenal.Model.Exceptions;
using Skyware.Arenal.Tracking;
using Spectre.Console;
using System.Diagnostics;

namespace CliTestApp
{
    public class Program
    {

        //Holds session-wide JWT
        private static IConfiguration? _config = null;

        public static async Task Main(string[] args)
        {
            AnsiConsole.Write(new Rule("[bold green]Arenal tests[/]") { Justification = Justify.Left });
            AnsiConsole.WriteLine();
            try
            {
                BuildConfig();
                AnsiConsole.MarkupLine("Configuration: [green]OK[/]");
            }
            catch (Exception ex)
            {
                AnsiConsole.WriteException(ex);
                return;
            }

            //await GetFormAsync();
            await AnsiConsole
                .Status()
                .StartAsync($"Executing {nameof(DoOrderStuff)}", async(ctx) => { 
                    await DoOrderStuff();
                });
            //await DoOrganizationsStuff();
            //await ChangeOrderStatusDemo();

            //await PublisFakeOrders(20, "AD-G-1");
            AnsiConsole.WriteLine();
            AnsiConsole.WriteLine("The application has finished.");

        }

        /// <summary>
        /// Demonstrates how to authenticate and JWT.
        /// </summary>
        /// <returns></returns>
        private static async Task<TokenResponse> GetTokenAsync(HttpClient client, IConfiguration cfg)
        {
            return await client.RequestPasswordTokenAsync(new PasswordTokenRequest
            {
                Address = cfg["OpenIdProvider:Address"],
                ClientId = cfg["OpenIdProvider:ClientId"],
                ClientSecret = cfg["OpenIdProvider:ClientSecret"],
                Scope = cfg["OpenIdProvider:Scope"],
                UserName = cfg["OpenIdProvider:Username"],
                Password = cfg["OpenIdProvider:Password"]
            });
        }

        /// <summary>
        /// Read appsettings.json and injects user secrets.
        /// </summary>
        private static void BuildConfig()
        {
            var builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            builder.AddUserSecrets<Program>();
            _config = builder.Build();
        }

        /// <summary>
        /// CRUD on Orders.
        /// </summary>
        /// <returns></returns>
        private static async Task DoOrderStuff()
        {
            AnsiConsole.WriteLine();
            if (_config == null) { AnsiConsole.MarkupLine("[red]Missing configuration."); return; }

            using var client = new HttpClient();
            TokenResponse? tokenResponse = null;

            //Get JWT
            try
            {
                tokenResponse = await GetTokenAsync(client, _config);
                AnsiConsole.MarkupLine("Authentication: [green]OK[/]");
            }
            catch (Exception ex)
            {
                AnsiConsole.WriteException(ex);
                return;
            }


            //Create Demo Order
            Order origOrder = GetDemoOrder();

            OrderExtensions.BaseAddress = "https://localhost:7291/";


            // Create Order
            client.SetBearerToken(tokenResponse?.AccessToken);

            Order? newOrder = null;
            try
            {
                newOrder = await client.CreateOrdersAsync(origOrder);
                AnsiConsole.MarkupLine($"Order creation: [green]OK[/]");
                foreach (EntityChange change in origOrder.CompareTo(newOrder, nameof(Order))) {
                    AnsiConsole.MarkupLine($"\t[deepskyblue1]{change}[/]");
                }
            }
            catch (ArenalException ex)
            {
                AnsiConsole.WriteException(ex);
                return;
            }

            // Read as publisher
            Order? readOrder = null;
            try
            {
                readOrder = await client.GetOrderAsync(newOrder.ArenalId);
                AnsiConsole.MarkupLine($"Order retrieval (as publisher): [green]OK[/]");
                foreach (EntityChange change in newOrder.CompareTo(readOrder, nameof(Order)))
                {
                    AnsiConsole.MarkupLine($"\t[deepskyblue1]{change}[/]");
                }
            }
            catch (ArenalException ex)
            {
                AnsiConsole.WriteException(ex);
                return;
            }

            // Update
            client.SetBearerToken(tokenResponse?.AccessToken);
            readOrder.Patient = new Patient() { GivenName = "Миленов" };
            try
            {
                readOrder = await client.UpdateOrdersAsync(readOrder);
                AnsiConsole.MarkupLine($"Order update: [green]OK[/]");
            }
            catch (ArenalException ex)
            {
                AnsiConsole.WriteException(ex);
                return;
            }

            // Delete
            client.SetBearerToken(tokenResponse?.AccessToken);
            try
            {
                await client.DeleteOrdersAsync(readOrder);
                AnsiConsole.MarkupLine($"Order deletion: [green]OK[/]");
            }
            catch (ArenalException ex)
            {
                AnsiConsole.WriteException(ex);
            }

            //TODO: Other interactions with Arenal
        }

        private static async Task ChangeOrderStatusDemo()
        {
            if (_config == null) { Console.WriteLine("Missing configuration."); return; }

            using var client = new HttpClient();

            //Get JWT
            TokenResponse? tokenResponse = null;

            tokenResponse = await GetTokenAsync(client, _config);

            OrderExtensions.BaseAddress = "https://localhost:7291/";

            // Create Order
            Order changeStatOrder = GetDemoOrder();
            client.SetBearerToken(tokenResponse?.AccessToken);

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
            //Order changedStatusOrder = await client.ChangeOrderStatusAsync(origOrder.ArenalId, statusRequest);
            Order changedStatusOrder = await client.ChangeOrderStatusAsync(order, statusRequest);
        }

        private static async Task DoOrganizationsStuff()
        {
            if (_config == null) { Console.WriteLine("Missing configuration."); return; }

            using var client = new HttpClient();
            TokenResponse? tokenResponse = null;

            //Get JWT
            tokenResponse = await GetTokenAsync(client, _config);

            OrderExtensions.BaseAddress = "https://localhost:7291/";


            tokenResponse = await GetTokenAsync(client, _config);
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
                    DateOfBirth = new DateTime(1975, 5, 5).ToUniversalTime(),
                    IsMale = true,
                    Contacts = new[] { new Contact() { Type = ContactTypes.PHONE, Value = "0878005006" } }
                },
                new[] { new Service("14749-6", "Глюкоза"), },
                new[] { new Sample("SER", null, "S05FT5") }
            )
            {
                ProviderId = "AD-G-2"
            };

        }

        private static OrderStatusRequest GetDemoStatusRequest(Organization provider)
        {
            return new OrderStatusRequest()
            {
                NewStatus = OrderStatuses.IN_PROGRESS,
                ProviderNote = new Note()
                {
                    Value = "This is demo provider note."
                },
                //ProviderId = provider.ArenalId
            };
        }

        private static async Task GetFormAsync()
        {
            Compendium comp = new()
            {
                ProviderId = "misho",
                Services = new[]
                {
                    new CompendiumEntry() { Value = "Glucose"},
                    new CompendiumEntry() { Value = "Albumin"},
                    new CompendiumEntry() { Value = "Cholesterol"},
                    new CompendiumEntry() { Value = "Phlebotomy"},
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

        private static async Task PublisFakeOrders(int numOfOrders, string providerId)
        {

            if (_config == null) { Console.WriteLine("Missing configuration."); return; }

            using var client = new HttpClient();

            //Get JWT
            await GetTokenAsync(client, _config);

            //OrderExtensions.BaseAddress = "https://localhost:7291/";

            // Create Order
            TokenResponse? _tokenResponse = null;

            _tokenResponse = await GetTokenAsync(client, _config);
            Order? respOrd = null;

            for (int i = 0; i < numOfOrders; i++)
            {
                var o = FakeOrders.Generate(providerId);
                try
                {
                    respOrd = await client.CreateOrdersAsync(o);
                    Console.WriteLine($"Order created, ArenalId is: {respOrd.ArenalId}");
                }
                catch (ArenalException ex)
                {
                    Console.WriteLine($"Error: {ex.CombinedMessage()}");
                }
            }

        }


    }
}