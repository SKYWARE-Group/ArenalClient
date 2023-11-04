﻿using Flurl.Util;
using IdentityModel.Client;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ArenalIntegrationTests
{
    internal class SingleRoleBaseTestSetup: BaseTestSetup
    {
        protected HttpClient _publisher;
        protected string? _publisherId;
        protected HttpClient _laboratory;
        protected string? _laboratoryId;

        [OneTimeSetUp]
        public override async Task SetUp()
        {
            await base.SetUp();
            _publisher = new HttpClient();
            _laboratory = new HttpClient();
            using (HttpClient client = new HttpClient())
            {
                var tknPublisher = await GetPublisherTokenAsync(client, _config);
                var tknLaboratory = await GetLabTokenAsync(client, _config);

                _publisher.SetBearerToken(tknPublisher?.AccessToken);
                var handler = new JwtSecurityTokenHandler();
                var jsonToken = handler.ReadToken(tknPublisher?.AccessToken);
                _publisherId = (jsonToken.ToKeyValuePairs().FirstOrDefault(p => p.Key == "Claims").Value as List<Claim>)
                    ?.Single(p => p.Type == "org_id").Value;
                _laboratory.SetBearerToken(tknLaboratory?.AccessToken);
                jsonToken = handler.ReadToken(tknLaboratory?.AccessToken);
                _laboratoryId = (jsonToken.ToKeyValuePairs().FirstOrDefault(p => p.Key == "Claims").Value as List<Claim>)
                    ?.Single(p => p.Type == "org_id").Value;
            }
        }

        [OneTimeTearDown]
        public override void TearDown()
        {
            _publisher?.Dispose();
            _laboratory?.Dispose();
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