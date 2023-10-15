using Flurl.Util;
using IdentityModel;
using IdentityModel.Client;
using Microsoft.Extensions.Configuration;
using Skyware.Arenal.Client;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Security.Claims;

namespace ArenalIntegrationTests
{
    [TestFixture]
    internal class BaseTestSetup
    {
        protected HttpClient _publisher;
        protected string _publisherId;
        protected HttpClient _laboratory;
        protected string _laboratoryId;
        protected IConfiguration _config;

        [OneTimeSetUp]
        public virtual async Task SetUp()
        {
            BuildConfig();
            _publisher = new HttpClient();
            _laboratory = new HttpClient();
            using (HttpClient client = new HttpClient())
            {
                var tknPublisher = await GetPublisherTokenAsync(client, _config);
                var tknLaboratory = await GetLabTokenAsync(client, _config);

                _publisher.SetBearerToken(tknPublisher?.AccessToken);
                var handler = new JwtSecurityTokenHandler();
                var jsonToken = handler.ReadToken(tknPublisher?.AccessToken);
                _publisherId = (jsonToken.ToKeyValuePairs()
                    .FirstOrDefault(p => p.Key == "Claims").Value as List<Claim>)
                    .FirstOrDefault(p => p.Type == "org_id").Value;
                _laboratory.SetBearerToken(tknLaboratory?.AccessToken);
                jsonToken = handler.ReadToken(tknLaboratory?.AccessToken);
                _laboratoryId = (jsonToken.ToKeyValuePairs()
                    .FirstOrDefault(p => p.Key == "Claims").Value as List<Claim>)
                    .FirstOrDefault(p => p.Type == "org_id").Value;
            }

            OrderExtensions.ARENAL_BASE = _config["APIBaseAddress"];
        }

        [OneTimeTearDown]
        public virtual void TearDown()
        {
            _publisher?.Dispose();
            _laboratory?.Dispose();
        }

        /// <summary>
        /// Configure the application (appsettings.json) and injects user secrets.
        /// </summary>
        private void BuildConfig()
        {
            var builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile("config.json", optional: false, reloadOnChange: true);
            builder.AddUserSecrets<BaseTestSetup>();
            _config = builder.Build();
        }

        /// <summary>
        /// Authenticate and get JWT.
        /// </summary>
        /// <returns></returns>
        private async Task<TokenResponse> GetPublisherTokenAsync(HttpClient client, IConfiguration cfg)
        {
            return await client.RequestPasswordTokenAsync(new PasswordTokenRequest()
            {
                Address = cfg["OpenIdProvider:Address"],
                ClientId = cfg["OpenIdProvider:ClientId"],
                ClientSecret = cfg["OpenIdProvider:ClientSecret"],
                Scope = cfg["OpenIdProvider:Scope"],
                UserName = cfg[$"OpenIdProvider:Publisher:Username"],
                Password = cfg[$"OpenIdProvider:Publisher:Password"]
            });
        }

        /// <summary>
        /// Authenticate  and get JWT.
        /// </summary>
        /// <returns></returns>
        private async Task<TokenResponse> GetLabTokenAsync(HttpClient client, IConfiguration cfg)
        {
            return await client.RequestPasswordTokenAsync(new PasswordTokenRequest()
            {
                Address = cfg["OpenIdProvider:Address"],
                ClientId = cfg["OpenIdProvider:ClientId"],
                ClientSecret = cfg["OpenIdProvider:ClientSecret"],
                Scope = cfg["OpenIdProvider:Scope"],
                UserName = cfg[$"OpenIdProvider:Laboratory:Username"],
                Password = cfg[$"OpenIdProvider:Laboratory:Password"]
            });
        }
    }
}
