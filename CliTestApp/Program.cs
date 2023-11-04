//Ignore Spelling: cfg Precisio

using IdentityModel.Client;
using Microsoft.Extensions.Configuration;
using Skyware.Arenal.Client;
using Skyware.Arenal.Filters;
using Skyware.Arenal.Forms.Bg;
using Skyware.Arenal.Model;
using Skyware.Arenal.Model.Exceptions;
using Skyware.Arenal.Model.Forms;
using Skyware.Arenal.Tracking;
using Spectre.Console;
using Spectre.Console.Json;
using System.Diagnostics;
using System.Text.Json;

namespace CliTestApp;

public class Program
{
    private static IConfiguration? _config = null;

    public static async Task Main(string[] args)
    {
        AnsiConsole.Write(new FigletText("Arenal").LeftJustified().Color(Color.Green));
        AnsiConsole.WriteLine();
        try
        {
            BuildConfig();
            AnsiConsole.MarkupLine("[gray]Configuration: OK[/]");
        }
        catch (Exception ex)
        {
            AnsiConsole.WriteException(ex);
            return;
        }

        var sequenceName = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Please, select the test to run:")
                .AddChoices(new[] {
                    "Post Sale Orders", "Lab to Lab test",
                }));




        //await GetFormAsync();
        //await AnsiConsole
        //   .Status()
        //   .StartAsync($"Executing {nameof(DoOrdersSequence)}", async (ctx) =>
        //   {
        //       await DoOrdersSequence();
        //   }
        //);

        //await AnsiConsole
        //    .Status()
        //    .StartAsync($"Executing {nameof(TakeAndReleseOrder)}", async (ctx) =>
        //    {
        //        await TakeAndReleseOrder();
        //    });

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
    /// CreatesOrder and changes its Status with convenience functions
    /// </summary>
    /// <returns></returns>
    private static async Task TakeAndReleseOrder()
    {
        AnsiConsole.WriteLine();
        if (_config == null) { AnsiConsole.MarkupLine("[red]Missing configuration."); return; }

        using HttpClient publisher = new();
        using HttpClient laboratory = new();

        TokenResponse? tknPublisher = null;
        TokenResponse? tknLaboratory = null;

        try
        {
            tknPublisher = await GetTokenAsync(publisher, _config, "PubA");
            AnsiConsole.MarkupLine("Authentication (publisher A, North Health): [green]OK[/]");
            tknLaboratory = await GetTokenAsync(laboratory, _config, "LabA");
            AnsiConsole.MarkupLine("Authentication (laboratory A, Precisio): [green]OK[/]");
        }
        catch (Exception ex)
        {
            AnsiConsole.WriteException(ex);
            return;
        }

        // local server testing
        //OrderExtensions.BaseAddress = "https://localhost:7291/";

        publisher.SetBearerToken(tknPublisher?.AccessToken);
        laboratory.SetBearerToken(tknLaboratory?.AccessToken);

        Order contextOrder = FakeOrders.GetFixedDemoOrder("AD-G-2", "AD-G-1");
        AnsiConsole.MarkupLine($"Order generated: [green]OK[/]");
        Order createdOrder = await publisher.CreateOrdersAsync(contextOrder);

        if (createdOrder == null)
        {
            AnsiConsole.MarkupLine($"Order published by (publisher A, North Health): [red]Failure[/]");
            return;
        }
        else
        {
            AnsiConsole.MarkupLine($"Order published by (publisher A, North Health): [green]OK[/]");
        }

        string createdOrderJson = JsonSerializer.Serialize(
           createdOrder,
           new JsonSerializerOptions()
           {
               PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
               DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
           });
        AnsiConsole.Write(
            new Panel(new JsonText(createdOrderJson))
                .Header("Newly created Order")
                .Collapse()
                .RoundedBorder()
                .BorderColor(Color.Yellow));

        try
        {
            await laboratory.TakeOrderAsync(createdOrder);
            AnsiConsole.MarkupLine($"Order taken: [green]OK[/]");
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"Order taken: [red]Failure[/]");
            AnsiConsole.WriteException(ex);
            return;
        }

        /// Try to take it again must fail
        try
        {
            await laboratory.TakeOrderAsync(createdOrder);
            AnsiConsole.MarkupLine($"Try to take taken order by (laboratory A, Precisio) failure: [red]Failure[/]");
            AnsiConsole.MarkupLine($"Order was taken while was with {OrderStatuses.IN_PROGRESS} Status: [red]");
            return;
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"Try to take taken order failure: [green]OK[/]");
        }

        /// Try to release it by publisher A (North Health) while in taken state must fail
        try
        {
            await publisher.ReleaseOrderAsync(createdOrder);
            AnsiConsole.MarkupLine($"Order released by (publisher A, North Health) must fail: [red]Failure[/]");
            return;
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"Order released by (publisher A, North Health) must fail: [green]OK[/]");
        }

        /// Try to release it
        try
        {
            await laboratory.ReleaseOrderAsync(createdOrder);
            AnsiConsole.MarkupLine($"Order released by (laboratory A, Precisio): [green]OK[/]");
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"Order released by (laboratory A, Precisio): [red]Failure[/]");
            AnsiConsole.WriteException(ex);
            return;
        }

        /// Try to release it again must fail
        try
        {
            await laboratory.ReleaseOrderAsync(createdOrder, "note");
            AnsiConsole.MarkupLine($"Try to release released order  by (laboratory A, Precisio) failure: [red]Failure[/]");
            AnsiConsole.MarkupLine($"Order was released while was with {OrderStatuses.AVAILABLE} Status: [red]");
            return;
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"Try to release released order  by (laboratory A, Precisio) failure: [green]OK[/]");
        }
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
        readOrderAsPubA.Patient = new Skyware.Arenal.Model.Patient() { GivenName = "Миленов" };
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

        LabReferral lr = new()
        {
            Nrn = "23184A000A1F",
            AmbulatoryNrn = "2316A7000AA1",
            Issued = DateTime.Today.AddDays(-3),
            SampleDate = DateTime.Today.AddDays(-2),
            LabPracticeCode = "0302141842",
            MainDiagnosis = "Z00.0",
            Patient = new()
            {
                NationalIdentifier = "9912055612",
                DateOfBirth = new DateTime(1999, 12, 5),
                Rhif = "03",
                HealthRegion = "12",
                GivenName = "Мария",
                MiddleName = "Василева",
                FamilyName = "Борисова",
                Address = new()
                {
                    Town = "Варна",
                    Area = "Младост",
                    Street = "Йонко Вапцаров",
                    StreetNumber = "22"
                }
            },
            Doctor = new()
            {
                PracticeCode = "0305111422",
                Uin = "0400045236",
                SpecialityCode = "00"
            },
            Examinations = new()
            {
                new ReferralItem()
                {
                    NhifCode = "01.01",
                    StatisticsCode = "91910-04",
                    SpecialtyCode = "14",
                    Uin = "1600004145"
                }
            }
        };

        using HttpClient client = new();
        HttpClientExtensions.BaseAddress = "https://arenal-forms.azurewebsites.net/";

        Stopwatch s = new();
        s.Start();

        DocumentAnswer? ans = await client.GetFormAsync("bg.nhif.referral.f4", lr.GetBase64Data());

        s.Stop();
        await Console.Out.WriteLineAsync($"Form is generated in {s.ElapsedMilliseconds}ms.");

        await SaveAndOpen(ans.GetRawData());
    }

    private static async Task SaveAndOpen(byte[]? pdfData)
    {
        if (pdfData == null) return;
        string fn = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "arenal-jasper-demo.pdf");
        using var fs = new FileStream(fn, FileMode.Create, FileAccess.Write);
        await fs.WriteAsync(pdfData);
        fs.Flush();
        fs.Close();
        await Console.Out.WriteLineAsync($"File {fn} is saved, size is {pdfData.Length} bytes.");
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