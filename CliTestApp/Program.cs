using IdentityModel.Client;
using Microsoft.Extensions.Configuration;
using Skyware.Arenal.Client;
using Skyware.Arenal.Discovery;
using Skyware.Arenal.Filters;
using Skyware.Arenal.Model;
using Skyware.Arenal.Model.DocumentGeneration;
using Skyware.Arenal.Model.Exceptions;
using Skyware.Arenal.Tracking;
using Spectre.Console;
using Spectre.Console.Json;
using System.Diagnostics;
using System.Text.Json;

namespace CliTestApp;

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
            .StartAsync($"Executing {nameof(DoOrdersSequence)}", async (ctx) =>
            {
                await DoOrdersSequence();
            });
        //await DoOrganizationsStuff();
        //await ChangeOrderStatusDemo();

        //await PublishFakeOrders(20, "AD-G-1");
        AnsiConsole.WriteLine();
        AnsiConsole.WriteLine("The application has finished.");

    }

    /// <summary>
    /// Authenticate and get JWT.
    /// </summary>
    /// <returns></returns>
    private static async Task<TokenResponse> GetTokenAsync(HttpClient client, IConfiguration cfg, string credSet)
    {
        return await client.RequestPasswordTokenAsync(new PasswordTokenRequest()
        {
            Address = cfg["OpenIdProvider:Address"],
            ClientId = cfg["OpenIdProvider:ClientId"],
            ClientSecret = cfg["OpenIdProvider:ClientSecret"],
            Scope = cfg["OpenIdProvider:Scope"],
            UserName = cfg[$"OpenIdProvider:{credSet}Username"],
            Password = cfg[$"OpenIdProvider:{credSet}Password"]
        });
        //switch (credSet)
        //{
        //    case "PubA":
        //        req.UserName = cfg["OpenIdProvider:Username"];
        //        req.Password = cfg["OpenIdProvider:Password"];
        //        break;
        //    default: 
        //        throw new ArgumentOutOfRangeException(nameof(credSet), "Credentials set isn't resolved.");
        //}
        //return await client.RequestPasswordTokenAsync(req);
    }

    /// <summary>
    /// Configure the application (appsettings.json) and injects user secrets.
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
    private static async Task DoOrdersSequence()
    {
        AnsiConsole.WriteLine();
        if (_config == null) { AnsiConsole.MarkupLine("[red]Missing configuration."); return; }

        using HttpClient pubAClient = new();
        using HttpClient laboratoryAClient = new();
        using HttpClient pubBClient = new();
        using HttpClient laboratoryBClient = new();
        TokenResponse? tknRespPubA = null;
        TokenResponse? tknRespPubB = null;
        TokenResponse? tknRespLabA = null;
        TokenResponse? tknRespLabB = null;


        //Authenticate and get JWT
        try
        {
            tknRespPubA = await GetTokenAsync(pubAClient, _config, "PubA");
            AnsiConsole.MarkupLine("Authentication (publisher A, North Health): [green]OK[/]");
            tknRespPubB = await GetTokenAsync(pubBClient, _config, "PubB");
            AnsiConsole.MarkupLine("Authentication (publisher B, Vista Vita): [green]OK[/]");
            tknRespLabA = await GetTokenAsync(laboratoryAClient, _config, "LabA");
            AnsiConsole.MarkupLine("Authentication (laboratory A, Precisio): [green]OK[/]");
            tknRespLabB = await GetTokenAsync(laboratoryBClient, _config, "LabB");
            AnsiConsole.MarkupLine("Authentication (laboratory B, Lab-O-Mat): [green]OK[/]");
        }
        catch (Exception ex)
        {
            AnsiConsole.WriteException(ex);
            return;
        }

        //Set JWT
        pubAClient.SetBearerToken(tknRespPubA?.AccessToken);
        pubBClient.SetBearerToken(tknRespPubB?.AccessToken);
        laboratoryAClient.SetBearerToken(tknRespLabA?.AccessToken);
        laboratoryBClient.SetBearerToken(tknRespLabB?.AccessToken);

        //In case of locally running server
        //OrderExtensions.BaseAddress = "https://localhost:7291/";

        //Holds created orders
        List<Order> tempOrders = new();

        //Create Demo Order
        Order origOrder = FakeOrders.GetFixedDemoOrder("AD-G-2", "AD-G-1"); //From North Health To Precisio
        if (origOrder.Validate().IsValid)
        {
            AnsiConsole.MarkupLine($"Generated Order validation: [green]OK[/]");
        }
        else
        {
            AnsiConsole.MarkupLine($"Generated Order validation: [red]Failure[/]");
        }

        //Create Order as publisher A
        Order? newOrder = null;
        try
        {
            newOrder = await pubAClient.CreateOrdersAsync(origOrder);
            AnsiConsole.MarkupLine($"Order creation (as publisher A North Health to Lab A Precisio): [green]OK[/]");
            foreach (EntityChange change in origOrder.CompareTo(newOrder, nameof(Order)))
            {
                AnsiConsole.MarkupLine($"\t[deepskyblue1]{change}[/]");
            }
            tempOrders.Add(newOrder);
        }
        catch (ArenalException ex)
        {
            AnsiConsole.WriteException(ex);
            return;
        }

        //Show JSON
        string newOrderJson = JsonSerializer.Serialize(
            newOrder,
            new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
            });
        AnsiConsole.Write(
            new Panel(new JsonText(newOrderJson))
                .Header("Newly created Order")
                .Collapse()
                .RoundedBorder()
                .BorderColor(Color.Yellow));

        // Read Order as publisher A (North Health)
        Order? readOrderAsPubA = null;
        try
        {
            readOrderAsPubA = await pubAClient.GetOrderAsync(newOrder.ArenalId);
            AnsiConsole.MarkupLine($"Order retrieval (as publisher A, North Health): [green]OK[/]");
            foreach (EntityChange change in newOrder.CompareTo(readOrderAsPubA, nameof(Order)))
            {
                AnsiConsole.MarkupLine($"\t[deepskyblue1]{change}[/]");
            }
        }
        catch (ArenalException ex)
        {
            AnsiConsole.WriteException(ex);
            return;
        }

        // Read Order as publisher B (Vista Vita)
        Order? readOrderAsPubB = null;
        try
        {
            readOrderAsPubB = await pubBClient.GetOrderAsync(newOrder.ArenalId);
            AnsiConsole.MarkupLine($"Order retrieval (as publisher B, Vista Vita): [green]OK[/]");
            if (readOrderAsPubB is not null)
            {
                AnsiConsole.MarkupLine($"Order retrieval should fail: [red]Security issue![/]");
            }
            foreach (EntityChange change in newOrder.CompareTo(readOrderAsPubA, nameof(Order)))
            {
                AnsiConsole.MarkupLine($"\t[deepskyblue1]{change}[/]");
            }
        }
        catch (ArenalException ex)
        {
            AnsiConsole.WriteException(ex);
            return;
        }

        // Read Order as laboratory A (Precisio)
        Order? readOrderAsLabA = null;
        try
        {
            readOrderAsLabA = await laboratoryAClient.GetOrderAsync(newOrder.ArenalId);
            AnsiConsole.MarkupLine($"Order retrieval (as laboratory A, Precisio): [green]OK[/]");
        }
        catch (ArenalException ex)
        {
            AnsiConsole.WriteException(ex);
            return;
        }

        // Read Order as laboratory B (Lab-O-Mat)
        Order? readOrderAsLabB = null;
        try
        {
            readOrderAsLabB = await laboratoryBClient.GetOrderAsync(newOrder.ArenalId);
            AnsiConsole.MarkupLine($"Order retrieval (as laboratory B, Lab-O-Mat): [green]OK[/]");
            if (readOrderAsLabB is not null)
            {
                AnsiConsole.MarkupLine($"Order retrieval should fail: [red]Security issue![/]");
            }
            foreach (EntityChange change in newOrder.CompareTo(readOrderAsPubA, nameof(Order)))
            {
                AnsiConsole.MarkupLine($"\t[deepskyblue1]{change}[/]");
            }
        }
        catch (ArenalException ex)
        {
            AnsiConsole.WriteException(ex);
            return;
        }

        // Search Order as laboratory A
        Filter[] searchCases = new[]
        {
            new Filter(nameof(Order.PlacerId), ValueComparisons.Equals, newOrder.PlacerId)
                .And(nameof(Order.Created), ValueComparisons.GreaterThan, DateTime.Now.ToUniversalTime().AddMinutes(-10)),
            new Filter(nameof(Order.Status), ValueComparisons.Equals, OrderStatuses.AVAILABLE)
                .And(nameof(Order.Created), ValueComparisons.GreaterThan, DateTime.Now.ToUniversalTime().AddMinutes(-10))
                .And(Predicate.OrdersByPid(newOrder.Patient.Identifiers.First().Value)),
            new Filter(nameof(Order.Created), ValueComparisons.GreaterThan, DateTime.Now.ToUniversalTime().AddMinutes(-10))
                .And(Predicate.OrdersBySampleId(newOrder.Samples.First().SampleId.Value)),
            new Filter(nameof(Order.Created), ValueComparisons.GreaterThan, DateTime.Now.ToUniversalTime().AddMinutes(-10))
                .And(new Predicate($"{nameof(Order.Services)}_.{nameof(Service.ServiceId)}.{nameof(Identifier.Value)}", ValueComparisons.Equals, "14749-6")),
            new Filter(nameof(Order.Created), ValueComparisons.GreaterThan, DateTime.Now.ToUniversalTime().AddMinutes(-10))
                .And(new Predicate($"{nameof(Order.Services)}_.{nameof(Service.AlternateIdentifiers)}_.{nameof(Identifier.Value)}", ValueComparisons.Equals, "03-002")),
            new Filter(nameof(Order.Created), ValueComparisons.GreaterThan, DateTime.Now.ToUniversalTime().AddMinutes(-10))
                .And(new Predicate($"{nameof(Order.Services)}_.{nameof(Service.AlternateIdentifiers)}_.{nameof(Identifier.Value)}", ValueComparisons.Equals, "01.11")),
            new Filter(nameof(Order.Created), ValueComparisons.GreaterThan, DateTime.Now.ToUniversalTime().AddMinutes(-10))
                .And(new Predicate($"{nameof(Order.Services)}_.{nameof(Service.AlternateIdentifiers)}_.{nameof(Identifier.Value)}", ValueComparisons.Equals, "0-155"))

        };

        for (int i = 0; i < searchCases.Length; i++)
        {
            try
            {

                AnsiConsole.MarkupLine($"Search case #{i + 1} (as laboratory A): [green]OK[/]");
                AnsiConsole.MarkupLine($"\t[yellow3_1]where: {searchCases[i]}[/]");
                Order[] labAfoundOrders = await laboratoryAClient.GetOrdersAsync(searchCases[i]);
                AnsiConsole.MarkupLine($"\t[yellow3_1]Found orders (as laboratory A): {labAfoundOrders?.Length}[/]");
                if (labAfoundOrders?.FirstOrDefault() is not null && labAfoundOrders.FirstOrDefault()?.ArenalId == newOrder.ArenalId)
                {
                    AnsiConsole.MarkupLine($"\t[green]First found order matches previously created one ({newOrder.ArenalId}).[/]");
                }
                else
                {
                    AnsiConsole.MarkupLine($"\t[red]Orders not found or first one does not matches previously created one ({newOrder.ArenalId}).[/]");
                }
            }
            catch (ArenalException ex)
            {
                AnsiConsole.WriteException(ex);
                return;
            }
        }


        // Update Order as publisher A
        readOrderAsPubA.Patient = new Patient() { GivenName = "Миленов" };
        try
        {
            readOrderAsPubA = await pubAClient.UpdateOrdersAsync(readOrderAsPubA);
            AnsiConsole.MarkupLine($"Order update (as publisher A): [green]OK[/]");
        }
        catch (ArenalException ex)
        {
            AnsiConsole.WriteException(ex);
            return;
        }

        // Delete Order as publisher A
        try
        {
            await pubAClient.DeleteOrdersAsync(readOrderAsPubA);
            AnsiConsole.MarkupLine($"Order deletion (as publisher A): [green]OK[/]");
        }
        catch (ArenalException ex)
        {
            AnsiConsole.WriteException(ex);
        }

    }

    //private static async Task ChangeOrderStatusDemo()
    //{
    //    if (_config == null) { Console.WriteLine("Missing configuration."); return; }

    //    using var client = new HttpClient();

    //    //Get JWT
    //    TokenResponse? tokenResponse = null;

    //    tokenResponse = await GetTokenAsync(client, _config, "LabA");

    //    OrderExtensions.BaseAddress = "https://localhost:7291/";

    //    // Create Order
    //    Order changeStatOrder = FakeOrders.GetFixedDemoOrder("AD-G-2");
    //    client.SetBearerToken(tokenResponse?.AccessToken);

    //    Order? order = null;
    //    try
    //    {
    //        order = await client.GetOrderAsync("AD-O-L");
    //        Console.WriteLine($"Order taken, ArenalId is: {order.ArenalId}");
    //    }
    //    catch (ArenalException ex)
    //    {
    //        Console.WriteLine($"Error: {ex.CombinedMessage()}");
    //        return;
    //    }

    //    //Get Organization
    //    Organization provider = null;
    //    try
    //    {
    //        provider = await client.GetProviderAsync("AD-G-2");
    //        Console.WriteLine($"Organization taken, ArenalId is: {provider.ArenalId}");
    //    }
    //    catch (ArenalException ex)
    //    {
    //        Console.WriteLine($"Error: {ex.CombinedMessage()}");
    //        return;
    //    }

    //    OrderStatusRequest statusRequest = GetDemoStatusRequest(provider);

    //    //Change Status
    //    //Order changedStatusOrder = await pubAClient.ChangeOrderStatusAsync(origOrder.ArenalId, statusRequest);
    //    Order changedStatusOrder = await client.ChangeOrderStatusAsync(order, statusRequest);
    //}

    //private static async Task DoOrganizationsStuff()
    //{
    //    if (_config == null) { Console.WriteLine("Missing configuration."); return; }

    //    using var client = new HttpClient();
    //    TokenResponse? tokenResponse = null;

    //    //Get JWT
    //    tokenResponse = await GetTokenAsync(client, _config, "LabA");

    //    OrderExtensions.BaseAddress = "https://localhost:7291/";


    //    tokenResponse = await GetTokenAsync(client, _config, "LabA");
    //    //Get Organization
    //    Organization provider = null;
    //    try
    //    {
    //        provider = await client.GetProviderAsync("AD-G-2");
    //        Console.WriteLine($"Organization taken, ArenalId is: {provider.ArenalId}");
    //    }
    //    catch (ArenalException ex)
    //    {
    //        Console.WriteLine($"Error: {ex.CombinedMessage()}");
    //        return;
    //    }

    //    IEnumerable<Organization> providers = null;
    //    try
    //    {
    //        providers = await client.GetProvidersAsync();
    //        foreach (var ctxProvider in providers)
    //            Console.WriteLine($"Organization taken, ArenalId is: {ctxProvider.ArenalId}");
    //    }
    //    catch (ArenalException ex)
    //    {
    //        Console.WriteLine($"Error: {ex.CombinedMessage()}");
    //        return;
    //    }
    //}

    //private static OrderStatusRequest GetDemoStatusRequest(Organization provider)
    //{
    //    return new OrderStatusRequest()
    //    {
    //        NewStatus = OrderStatuses.IN_PROGRESS,
    //        ProviderNote = new Note()
    //        {
    //            Value = "This is demo provider note."
    //        },
    //        //ProviderId = provider.ArenalId
    //    };
    //}

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

    //private static async Task PublishFakeOrders(int numOfOrders, string providerId)
    //{

    //    if (_config == null) { Console.WriteLine("Missing configuration."); return; }

    //    using var client = new HttpClient();

    //    //Get JWT
    //    await GetTokenAsync(client, _config, "PubA");

    //    //OrderExtensions.BaseAddress = "https://localhost:7291/";

    //    // Create Order
    //    TokenResponse? _tokenResponse = null;

    //    _tokenResponse = await GetTokenAsync(client, _config, "PubA");
    //    Order? respOrd = null;

    //    for (int i = 0; i < numOfOrders; i++)
    //    {
    //        var o = FakeOrders.Generate(providerId);
    //        try
    //        {
    //            respOrd = await client.CreateOrdersAsync(o);
    //            Console.WriteLine($"Order created, ArenalId is: {respOrd.ArenalId}");
    //        }
    //        catch (ArenalException ex)
    //        {
    //            Console.WriteLine($"Error: {ex.CombinedMessage()}");
    //        }
    //    }

    //}


}