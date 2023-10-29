using Microsoft.Extensions.Configuration;

namespace ArenalIntegrationTests
{
    [TestFixture]
    internal class BaseTestSetup
    {
        protected IConfiguration _config;

        [OneTimeSetUp]
        public virtual async Task SetUp()
        {
            BuildConfig();
            await Task.CompletedTask;
            OrderExtensions.ARENAL_BASE = _config["APIBaseAddress"];
        }

        [OneTimeTearDown]
        public virtual void TearDown()
        {
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
    }
}
