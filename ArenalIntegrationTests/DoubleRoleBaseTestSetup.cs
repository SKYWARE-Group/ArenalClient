using Flurl.Util;
using IdentityModel.Client;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ArenalIntegrationTests
{
    internal class DoubleRoleBaseTestSetup: BaseTestSetup
    {
        protected HttpClient _actor;
        protected string? _actorId;

        [OneTimeSetUp]
        public virtual async Task SetUp()
        {
            await base.SetUp();
            _actor = new HttpClient();
            using (HttpClient client = new HttpClient())
            {
                var tknActor = await GetActorsTokenAsync(client, _config);
                _actor.SetBearerToken(tknActor?.AccessToken);
                var handler = new JwtSecurityTokenHandler();
                var jsonToken = handler.ReadToken(tknActor?.AccessToken);
                _actorId = (jsonToken.ToKeyValuePairs().FirstOrDefault(p => p.Key == "Claims").Value as List<Claim>)?
                    .Single(p => p.Type == "org_id").Value;
            }
        }

        [OneTimeTearDown]
        public override void TearDown()
        {
            _actor?.Dispose();
        }


        /// <summary>
        /// Authenticate and get JWT.
        /// </summary>
        /// <returns></returns>
        private async Task<TokenResponse> GetActorsTokenAsync(HttpClient client, IConfiguration cfg)
        {
            return await client.RequestPasswordTokenAsync(new PasswordTokenRequest()
            {
                Address = cfg["OpenIdProvider:Address"],
                ClientId = cfg["OpenIdProvider:ClientId"],
                ClientSecret = cfg["OpenIdProvider:ClientSecret"],
                Scope = cfg["OpenIdProvider:Scope"],
                UserName = cfg[$"OpenIdProvider:DoubleRole:Username"],
                Password = cfg[$"OpenIdProvider:DoubleRole:Password"]
            });
        }
    }
}
