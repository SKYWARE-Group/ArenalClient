using IdentityModel.Client;
using Skyware.Arenal.Client;
using Skyware.Arenal.Model;
using System.Text.Json;

namespace CliTestApp
{
    public class Program
    {

        private static TokenResponse? _tokenResponse = null;

        /// <summary>
        /// Demonstrates how to obtain and cache JWT
        /// </summary>
        /// <returns></returns>
        private static async Task<string> GetTokenAsync()
        {
            if (_tokenResponse == null)
            {
                using var client = new HttpClient();
                //Authenticate
                _tokenResponse = await client.RequestPasswordTokenAsync(new PasswordTokenRequest
                {
                    Address = "https://kc-dev.skyware-group.com/realms/arenal-dev/protocol/openid-connect/token",
                    ClientId = "api-client",
                    ClientSecret = "8ML4h83ruQ5G5NgvrGpNkMtnl8bJT1UH",
                    Scope = "api-scope",
                    UserName = "Mani",
                    Password = "123123"
                });
            }
            return _tokenResponse.AccessToken;
        }

        public static async Task<Order> PublisOrderAsync(Order order)
        {
            using var client = new HttpClient();
            client.SetBearerToken(await GetTokenAsync());
            return await client.CreateOrdersAsync(order);
        }

        public static async Task Main(string[] args)
        {
            Order order = new Order()
            {
                Patient = new Person() 
                { 
                    GivenName = "Misho" 
                    //Other properties
                }
                //Other properties
            };
            Order respOrd = await PublisOrderAsync(order);
            Console.WriteLine($"Order created, ArenalId is: {respOrd.ArenalId}");
            //Other interactions with Arenal
        }




        private static void CaseJson1()
        {

            Order o = new()
            {
                ArenalId = "n1-0-as6g",
                Patient = new Person()
                {
                    Identifiers = new[] {
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
                options: new JsonSerializerOptions()
                {
                    WriteIndented = true,
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
                }) + "\n");
        }

        private static void DoConst()
        {
            Console.WriteLine(string.Join(Environment.NewLine, Helpers.GetAllConstants(typeof(Authorities))));
        }

       
    }
}