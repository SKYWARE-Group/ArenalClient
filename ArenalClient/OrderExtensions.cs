using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Skyware.Arenal.Client
{

    /// <summary>
    /// HttpClient extensions for accessing Orders
    /// </summary>
    public static class OrderExtensions
    {

        /// <summary>
        /// All orders endpoint
        /// </summary>
#if LOCAL_SERVER 
        public const string ORDERS_URL = "https://localhost:7291/api/orders";
#else
        public const string ORDERS_URL = "https://arenal2.azurewebsites.net/api/orders";
#endif

        /// <summary>
        /// Single order endpoint (CRUD)
        /// </summary>

#if LOCAL_SERVER
        public const string SINGLE_ORDER_URL = "https://localhost:7291/api/order";
#else
        public const string SINGLE_ORDER_URL = "https://arenal2.azurewebsites.net/api/order";
#endif

        private static readonly JsonSerializerOptions _jOpts = new JsonSerializerOptions() { 
            DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull, 
            PropertyNameCaseInsensitive = true, 
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase 
        };

        /// <summary>
        /// Retrieves orders
        /// </summary>
        /// <param name="client"><see cref="HttpClient"/> to deal with.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
        /// <returns>Array of <see cref="Model.Order"/>.</returns>
        /// <exception cref="Model.Exceptions.ArenalException">Arenal returned an error.</exception>
        /// <exception cref="ArgumentNullException">The request or content was null.</exception>
        public static async Task<Model.Order[]> GetOrdersAsync(this HttpMessageInvoker client, CancellationToken cancellationToken = default)
        {

            HttpRequestMessage request = new HttpRequestMessage
            {
                RequestUri = new Uri(ORDERS_URL)
            };
            request.Headers.Add("Accept", "application/json");
            request.Headers.CacheControl = new System.Net.Http.Headers.CacheControlHeaderValue() { NoCache = true };

            HttpResponseMessage response = await client.SendAsync(request, cancellationToken).ConfigureAwait(false);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return await response.Content.ReadFromJsonAsync<Model.Order[]>(cancellationToken: cancellationToken).ConfigureAwait(false);
            }
            else
            {
                if (string.IsNullOrWhiteSpace(response.Content?.ToString())) throw new Model.Exceptions.ArenalException((int)response.StatusCode, null);
                throw new Model.Exceptions.ArenalException(
                    ((int)response.StatusCode),
                    await response.Content.ReadFromJsonAsync<Model.Exceptions.ArenalError>(_jOpts, cancellationToken).ConfigureAwait(false));
            }
        }

        /// <summary>
        /// Creates an order
        /// </summary>
        /// <param name="client"><see cref="HttpClient"/> to deal with.</param>
        /// <param name="order"><see cref="Model.Order"/> to create.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
        /// <returns>Saved in Arenal <see cref="Model.Order"/>.</returns>
        /// <exception cref="Model.Exceptions.ArenalException">Arenal returned an error.</exception>
        /// <exception cref="ArgumentNullException">The request or content was null.</exception>
        public static async Task<Model.Order> CreateOrdersAsync(this HttpMessageInvoker client, Model.Order order, CancellationToken cancellationToken = default)
        {

            HttpRequestMessage request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(ORDERS_URL),
                Content = JsonContent.Create(order, typeof(Model.Order), options: _jOpts)
            };
            request.Headers.Add("Accept", "application/json");
            request.Headers.CacheControl = new System.Net.Http.Headers.CacheControlHeaderValue() { NoCache = true };

            HttpResponseMessage response = await client.SendAsync(request, cancellationToken).ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<Model.Order>(cancellationToken: cancellationToken).ConfigureAwait(false);
            } 
            else
            {
                string ans = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                if (string.IsNullOrWhiteSpace(ans)) throw new Model.Exceptions.ArenalException((int)response.StatusCode, null);
                throw new Model.Exceptions.ArenalException(
                    ((int)response.StatusCode), JsonSerializer.Deserialize<Model.Exceptions.ArenalError>(ans, _jOpts));
            }

        }

    }

}
