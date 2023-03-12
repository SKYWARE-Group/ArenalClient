using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Skyware.Arenal.Client
{

    /// <summary>
    /// HttpClient extensions
    /// </summary>
    public static class OrderExtensions
    {

        /// <summary>
        /// All orders endpoint
        /// </summary>
        public const string ORDERS_URL = "https://arenal2.azurewebsites.net/api/orders";

        /// <summary>
        /// Single order endpoint (CRUD)
        /// </summary>
        public const string SINGLE_ORDER_URL = "https://arenal2.azurewebsites.net/api/order";


        /// <summary>
        /// Retrieves orders
        /// </summary>
        /// <param name="client"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async Task<Model.Order[]> GetOrdersAsync(this HttpMessageInvoker client, CancellationToken cancellationToken = default)
        {

            HttpRequestMessage request = new HttpRequestMessage
            {
                RequestUri = new Uri(ORDERS_URL)
            };
            request.Headers.Add("Accept", "application/json");

            HttpResponseMessage response;
            try
            {
                response = await client.SendAsync(request, cancellationToken);
                return await response.Content.ReadFromJsonAsync<Model.Order[]>();
            }
            catch (Exception ex)
            {
                //TODO: Typed exception here
                throw ex;
            }
        }

        /// <summary>
        /// Creates an order
        /// </summary>
        /// <param name="client"></param>
        /// <param name="order"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async Task<Model.Order> CreateOrdersAsync(this HttpMessageInvoker client, Model.Order order, CancellationToken cancellationToken = default)
        {

            HttpRequestMessage request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(ORDERS_URL),
                Content = JsonContent.Create(order, typeof(Model.Order))
            };
            request.Headers.Add("Accept", "application/json");

            HttpResponseMessage response;
            try
            {
                response = await client.SendAsync(request, cancellationToken);
                return await response.Content.ReadFromJsonAsync<Model.Order>();
            }
            catch (Exception ex)
            {
                //TODO: Typed exception here
                throw ex;
            }
        }

    }

}
