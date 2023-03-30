﻿using Flurl;
using Skyware.Arenal.Filters;
using Skyware.Arenal.Model;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Encodings.Web;
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
        public const string ARENAL_BASE = "https://localhost:7291/";
#else
        public const string ARENAL_BASE = "https://arenal2.azurewebsites.net/";
#endif


        private static readonly JsonSerializerOptions _jOpts = new JsonSerializerOptions()
        {
            DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull,
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        /// <summary>
        /// Retrieves orders
        /// </summary>
        /// <param name="client"><see cref="HttpClient"/> to deal with.</param>
        /// <param name="filter"></param>
        /// <param name="offset"></param>
        /// <param name="limit"></param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
        /// <returns>Array of <see cref="Model.Order"/>.</returns>
        /// <exception cref="Model.Exceptions.ArenalException">Arenal returned an error.</exception>
        /// <exception cref="ArgumentNullException">The request or content was null.</exception>
        public static async Task<Order[]> GetOrdersAsync(
            this HttpMessageInvoker client, 
            Filter filter = null, int? offset = null, int? limit = null, 
            CancellationToken cancellationToken = default)
        {
            Url url = ARENAL_BASE
                .AppendPathSegment("api")
                .AppendPathSegment("orders");

            if (filter != null) url.SetQueryParam("where", WebUtility.HtmlEncode(filter.ToString()));
            if (offset != null && offset > 0) url.SetQueryParam("offset", offset.ToString());
            if (limit != null && limit > 0) url.SetQueryParam("limit", limit.ToString());


            HttpRequestMessage request = new HttpRequestMessage
            {
                RequestUri = url.ToUri()
            };
            request.Headers.Add("Accept", "application/json");
            request.Headers.CacheControl = new System.Net.Http.Headers.CacheControlHeaderValue() { NoCache = true };

            HttpResponseMessage response = await client.SendAsync(request, cancellationToken).ConfigureAwait(false);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return await response.Content.ReadFromJsonAsync<Order[]>(cancellationToken: cancellationToken).ConfigureAwait(false);
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
        /// Retrieves one order
        /// </summary>
        /// <param name="client"><see cref="HttpClient"/> to deal with.</param>
        /// <param name="orderId">order id to get</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
        /// <returns>Array of <see cref="Model.Order"/>.</returns>
        /// <exception cref="Model.Exceptions.ArenalException">Arenal returned an error.</exception>
        /// <exception cref="ArgumentNullException">The request or content was null.</exception>
        public static async Task<Model.Order> GetOrderAsync(
            this HttpMessageInvoker client, 
            string orderId, 
            CancellationToken cancellationToken = default)
        {
            if(string.IsNullOrEmpty(orderId)) throw new ArgumentNullException(nameof(orderId));

            Url url = ARENAL_BASE
                .AppendPathSegment("api")
                .AppendPathSegment("orders")
                .AppendPathSegment(orderId);


            HttpRequestMessage request = new HttpRequestMessage
            {
                RequestUri = url.ToUri()
            };
            request.Headers.Add("Accept", "application/json");
            request.Headers.CacheControl = new System.Net.Http.Headers.CacheControlHeaderValue() { NoCache = true };

            HttpResponseMessage response = await client.SendAsync(request, cancellationToken).ConfigureAwait(false);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return await response.Content.ReadFromJsonAsync<Model.Order>(cancellationToken: cancellationToken).ConfigureAwait(false);
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
        public static async Task<Model.Order> CreateOrdersAsync(
            this HttpMessageInvoker client, 
            Order order, 
            CancellationToken cancellationToken = default)
        {

            Url url = ARENAL_BASE
                .AppendPathSegment("api")
                .AppendPathSegment("orders");


            HttpRequestMessage request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = url.ToUri(),
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

        /// <summary>
        /// Deletes an order
        /// </summary>
        /// <param name="client"><see cref="HttpClient"/> to deal with.</param>
        /// <param name="order"><see cref="Model.Order"/> to create.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
        /// <exception cref="Model.Exceptions.ArenalException">Arenal returned an error.</exception>
        /// <exception cref="ArgumentNullException">The request or content was null.</exception>
        /// <exception cref="NullReferenceException">The order parameter is null or ArenalId of the order is null</exception>
        public static async Task DeleteOrdersAsync(
            this HttpMessageInvoker client, 
            Order order, 
            CancellationToken cancellationToken = default)
        {

            if (order == null) throw new NullReferenceException(nameof(order));
            if (string.IsNullOrEmpty(order.ArenalId)) throw new NullReferenceException(nameof(order.ArenalId));

            Url url = ARENAL_BASE
                .AppendPathSegment("api")
                .AppendPathSegment("orders")
                .AppendPathSegment(order.ArenalId);

            HttpRequestMessage request = new HttpRequestMessage
            {
                Method = HttpMethod.Delete,
                RequestUri = url.ToUri()
            };
            request.Headers.Add("Accept", "application/json");
            request.Headers.CacheControl = new System.Net.Http.Headers.CacheControlHeaderValue() { NoCache = true };

            HttpResponseMessage response = await client.SendAsync(request, cancellationToken).ConfigureAwait(false);
            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                if (string.IsNullOrWhiteSpace(response.Content?.ToString())) throw new Model.Exceptions.ArenalException((int)response.StatusCode, null);
                throw new Model.Exceptions.ArenalException(
                    ((int)response.StatusCode),
                    await response.Content.ReadFromJsonAsync<Model.Exceptions.ArenalError>(_jOpts, cancellationToken).ConfigureAwait(false));
            }
        }

        /// <summary>
        /// Deletes an order
        /// </summary>
        /// <param name="client"><see cref="HttpClient"/> to deal with.</param>
        /// <param name="order"><see cref="Model.Order"/> to delete.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
        /// <exception cref="Model.Exceptions.ArenalException">Arenal returned an error.</exception>
        /// <exception cref="ArgumentNullException">The request or content was null.</exception>
        /// <exception cref="NullReferenceException">The order parameter is null or ArenalId of the order is null</exception>
        public static async Task<Model.Order> UpdateOrdersAsync(
            this HttpMessageInvoker client, 
            Order order, 
            CancellationToken cancellationToken = default)
        {

            if (order == null) throw new NullReferenceException(nameof(order));
            if (string.IsNullOrEmpty(order.ArenalId)) throw new NullReferenceException(nameof(order.ArenalId));

            Url url = ARENAL_BASE
                .AppendPathSegment("api")
                .AppendPathSegment("orders")
                .AppendPathSegment(order.ArenalId);

            HttpRequestMessage request = new HttpRequestMessage
            {
                Method = HttpMethod.Put,
                RequestUri = url.ToUri(),
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
                if (string.IsNullOrWhiteSpace(response.Content?.ToString())) throw new Model.Exceptions.ArenalException((int)response.StatusCode, null);
                throw new Model.Exceptions.ArenalException(
                    ((int)response.StatusCode),
                    await response.Content.ReadFromJsonAsync<Model.Exceptions.ArenalError>(_jOpts, cancellationToken).ConfigureAwait(false));

            }
        }
    }
}