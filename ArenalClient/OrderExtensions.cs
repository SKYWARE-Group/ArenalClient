using Flurl;
using Skyware.Arenal.Filters;
using Skyware.Arenal.Model;
using Skyware.Arenal.Model.Actions;
using Skyware.Arenal.Model.Forms;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Skyware.Arenal.Client;


/// <summary>
/// HttpClient extensions for accessing Orders
/// </summary>
public static class OrderExtensions
{

    #region Constant and static variables

    /// <summary>
    /// All orders endpoint
    /// </summary>
#if LOCAL_SERVER
    public const string ARENAL_BASE = "https://localhost:7291/";
#elif ARENAL_FORMS
    public const string ARENAL_BASE = "https://arenal-forms.azurewebsites.net/";
#elif TESTING
    public static string ARENAL_BASE = "https://localhost:7291/";
#else
    public static string ARENAL_BASE = "https://arenal2.azurewebsites.net/";
#endif

    private static string _BaseAddress = ARENAL_BASE;

    /// <summary>
    /// Gets or set base Arenal URL.
    /// </summary>
    public static string BaseAddress { get => _BaseAddress; set => _BaseAddress = value; }


    private static readonly JsonSerializerOptions _jOpts = new JsonSerializerOptions()
    {
        DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull,
        PropertyNameCaseInsensitive = true,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

    #endregion

    //TODO: CRUD Generic?
    #region Orders

    /// <summary>
    /// Retrieves orders
    /// </summary>
    /// <param name="client"><see cref="HttpClient"/> to deal with.</param>
    /// <param name="filter"></param>
    /// <param name="offset"></param>
    /// <param name="limit"></param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns>Array of <see cref="Order"/>.</returns>
    /// <exception cref="Model.Exceptions.ArenalException">Arenal returned an error.</exception>
    /// <exception cref="ArgumentNullException">The request or content was null.</exception>
    public static async Task<Order[]> GetOrdersAsync(
        this HttpMessageInvoker client,
        Filter filter = null, int? offset = null, int? limit = null,
        CancellationToken cancellationToken = default)
    {
        Url url = _BaseAddress
            .AppendPathSegment("api")
            .AppendPathSegment("orders");

        if (filter is not null) url.SetQueryParam("where", filter.ToString()); //SetQueryParam makes html escape
        if (offset is not null && offset > 0) url.SetQueryParam("offset", offset.ToString());
        if (limit is not null && limit > 0) url.SetQueryParam("limit", limit.ToString());


        HttpRequestMessage request = new()
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
    /// <returns>Array of <see cref="Order"/>.</returns>
    /// <exception cref="Model.Exceptions.ArenalException">Arenal returned an error.</exception>
    /// <exception cref="ArgumentNullException">The request or content was null.</exception>
    public static async Task<Model.Order> GetOrderAsync(
        this HttpMessageInvoker client,
        string orderId,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrEmpty(orderId)) throw new ArgumentNullException(nameof(orderId));

        Url url = _BaseAddress
            .AppendPathSegment("api")
            .AppendPathSegment("orders")
            .AppendPathSegment(orderId);


        HttpRequestMessage request = new()
        {
            RequestUri = url.ToUri()
        };
        request.Headers.Add("Accept", "application/json");
        request.Headers.CacheControl = new System.Net.Http.Headers.CacheControlHeaderValue() { NoCache = true };

        HttpResponseMessage response = await client.SendAsync(request, cancellationToken).ConfigureAwait(false);
        if (response.StatusCode == System.Net.HttpStatusCode.OK)
        {
            return await response.Content.ReadFromJsonAsync<Order>(cancellationToken: cancellationToken).ConfigureAwait(false);
        }
        else
        {
            if (string.IsNullOrWhiteSpace(response.Content?.ToString())) throw new Model.Exceptions.ArenalException((int)response.StatusCode, null);
            throw new Model.Exceptions.ArenalException(
                (int)response.StatusCode,
                await response.Content.ReadFromJsonAsync<Model.Exceptions.ArenalError>(_jOpts, cancellationToken).ConfigureAwait(false));
        }
    }

    /// <summary>
    /// Creates an order
    /// </summary>
    /// <param name="client"><see cref="HttpClient"/> to deal with.</param>
    /// <param name="order"><see cref="Model.Order"/> to create.</param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns>Saved in Arenal <see cref="Order"/>.</returns>
    /// <exception cref="Model.Exceptions.ArenalException">Arenal returned an error.</exception>
    /// <exception cref="ArgumentNullException">The request or content was null.</exception>
    public static async Task<Model.Order> CreateOrdersAsync(
        this HttpMessageInvoker client,
        Order order,
        CancellationToken cancellationToken = default)
    {

        Url url = _BaseAddress
            .AppendPathSegment("api")
            .AppendPathSegment("orders");


        HttpRequestMessage request = new()
        {
            Method = HttpMethod.Post,
            RequestUri = url.ToUri(),
            Content = JsonContent.Create(order, typeof(Order), options: _jOpts)
        };
        request.Headers.Add("Accept", "application/json");
        request.Headers.CacheControl = new System.Net.Http.Headers.CacheControlHeaderValue() { NoCache = true };

        HttpResponseMessage response = await client.SendAsync(request, cancellationToken).ConfigureAwait(false);
        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<Order>(cancellationToken: cancellationToken).ConfigureAwait(false);
        }
        else
        {
            string ans = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            if (string.IsNullOrWhiteSpace(ans)) throw new Model.Exceptions.ArenalException((int)response.StatusCode, null);
            throw new Model.Exceptions.ArenalException(
                (int)response.StatusCode,
                JsonSerializer.Deserialize<Model.Exceptions.ArenalError>(ans, _jOpts));
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

        if (order is null) throw new NullReferenceException(nameof(order));
        if (string.IsNullOrEmpty(order.ArenalId)) throw new NullReferenceException(nameof(order.ArenalId));

        Url url = _BaseAddress
            .AppendPathSegment("api")
            .AppendPathSegment("orders")
            .AppendPathSegment(order.ArenalId);

        HttpRequestMessage request = new()
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
    /// Updates an order
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

        if (order is null) throw new NullReferenceException(nameof(order));
        if (string.IsNullOrEmpty(order.ArenalId)) throw new NullReferenceException(nameof(order.ArenalId));

        Url url = _BaseAddress
            .AppendPathSegment("api")
            .AppendPathSegment("orders")
            .AppendPathSegment(order.ArenalId);

        HttpRequestMessage request = new()
        {
            Method = HttpMethod.Put,
            RequestUri = url.ToUri(),
            Content = JsonContent.Create(order, typeof(Order), options: _jOpts)
        };
        request.Headers.Add("Accept", "application/json");
        request.Headers.CacheControl = new System.Net.Http.Headers.CacheControlHeaderValue() { NoCache = true };

        HttpResponseMessage response = await client.SendAsync(request, cancellationToken).ConfigureAwait(false);
        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<Order>(cancellationToken: cancellationToken).ConfigureAwait(false);
        }
        else
        {
            if (string.IsNullOrWhiteSpace(response.Content?.ToString())) throw new Model.Exceptions.ArenalException((int)response.StatusCode, null);
            throw new Model.Exceptions.ArenalException(
                (int)response.StatusCode,
                await response.Content.ReadFromJsonAsync<Model.Exceptions.ArenalError>(_jOpts, cancellationToken).ConfigureAwait(false));

        }
    }

    /// <summary>
    /// Updates order status
    /// </summary>
    /// <param name="client"><see cref="HttpClient"/> to deal with.</param>
    /// <param name="order"><see cref="Model.Order"/> order to change status.</param>
    /// <param name="statusRequest"><see cref="Model.Actions.OrderStatusRequest"/> description of the action.</param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns></returns>
    /// <exception cref="Model.Exceptions.ArenalException">Arenal returned an error.</exception>
    /// <exception cref="NullReferenceException">orderId is null or statusRequest is null.</exception>
    public static async Task<Model.Order> ChangeOrderStatusAsync(
        this HttpMessageInvoker client,
        string orderId,
        OrderStatusRequest statusRequest,
        CancellationToken cancellationToken = default)
    {

        if (statusRequest is null) throw new NullReferenceException(nameof(statusRequest));
        if (string.IsNullOrEmpty(orderId)) throw new NullReferenceException(nameof(orderId));

        Url url = _BaseAddress
            .AppendPathSegment("api")
            .AppendPathSegment("orders")
            .AppendPathSegment(orderId)
            .AppendPathSegment("status");

        HttpRequestMessage request = new()
        {
            Method = HttpMethod.Put,
            RequestUri = url.ToUri(),
            Content = JsonContent.Create(statusRequest, typeof(OrderStatusRequest), options: _jOpts)
        };
        request.Headers.Add("Accept", "application/json");
        request.Headers.CacheControl = new System.Net.Http.Headers.CacheControlHeaderValue() { NoCache = true };

        HttpResponseMessage response = await client.SendAsync(request, cancellationToken).ConfigureAwait(false);
        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<Order>(cancellationToken: cancellationToken).ConfigureAwait(false);
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
    /// Updates order status
    /// </summary>
    /// <param name="client"><see cref="HttpClient"/> to deal with.</param>
    /// <param name="order"><see cref="Model.Order"/> order to change status.</param>
    /// <param name="statusRequest"><see cref="Model.Actions.OrderStatusRequest"/> description of the action.</param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <exception cref="Model.Exceptions.ArenalException">Arenal returned an error.</exception>
    /// <exception cref="ArgumentNullException">The request or content was null.</exception>
    /// <exception cref="NullReferenceException">Order is null or statusRequest is null.</exception>
    public static async Task<Model.Order> ChangeOrderStatusAsync(
        this HttpMessageInvoker client,
        Order order,
        OrderStatusRequest statusRequest,
        CancellationToken cancellationToken = default)
    {
        return await client.ChangeOrderStatusAsync(order.ArenalId, statusRequest, cancellationToken);
    }


    /// <summary>
    /// Takes the order 
    /// </summary>
    /// <param name="client"><see cref="HttpClient"/> to deal with.</param>
    /// <param name="order"><see cref="Model.Order"/> order to change status.</param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <exception cref="Model.Exceptions.ArenalException">Arenal returned an error.</exception>
    /// <exception cref="ArgumentNullException">The request or content was null.</exception>
    /// <exception cref="NullReferenceException">Order is null or statusRequest is null.</exception>
    public static async Task<Order> TakeOrderAsync(
        this HttpMessageInvoker client,
        Order order,
        string providerNote = null,
        CancellationToken cancellationToken = default)
    {
        OrderStatusRequest statusRequest = new OrderStatusRequest()
        {
            NewStatus = OrderStatuses.IN_PROGRESS,
            ProviderNote = new Note() { Value = providerNote }
        };
        return await client.ChangeOrderStatusAsync(order.ArenalId, statusRequest, cancellationToken);
    }

    /// <summary>
    /// Releases the order 
    /// </summary>
    /// <param name="client"><see cref="HttpClient"/> to deal with.</param>
    /// <param name="order"><see cref="Model.Order"/> order to change status.</param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <exception cref="Model.Exceptions.ArenalException">Arenal returned an error.</exception>
    /// <exception cref="ArgumentNullException">The request or content was null.</exception>
    /// <exception cref="NullReferenceException">Order is null or statusRequest is null.</exception>
    public static async Task<Order> ReleaseOrderAsync(
        this HttpMessageInvoker client,
        Order order,
        string providerNote = null,
        CancellationToken cancellationToken = default)
    {
        OrderStatusRequest statusRequest = new OrderStatusRequest()
        {
            NewStatus = OrderStatuses.AVAILABLE,
            ProviderNote = new Note() { Value = providerNote }
        };
        return await client.ChangeOrderStatusAsync(order.ArenalId, statusRequest, cancellationToken);
    }
    #endregion

    //TODO: CRUD Generic?
    #region Providers

    /// <summary>
    /// Retrieves providers
    /// </summary>
    /// <param name="client"><see cref="HttpClient"/> to deal with.</param>
    /// <param name="filter"></param>
    /// <param name="offset"></param>
    /// <param name="limit"></param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns>Array of <see cref="Organization"/>.</returns>
    /// <exception cref="Model.Exceptions.ArenalException">Arenal returned an error.</exception>
    /// <exception cref="ArgumentNullException">The request or content was null.</exception>
    public static async Task<Organization[]> GetProvidersAsync(
        this HttpMessageInvoker client,
        Filter filter = null, int? offset = null, int? limit = null,
        CancellationToken cancellationToken = default)
    {
        Url url = _BaseAddress
            .AppendPathSegment("api")
            .AppendPathSegment("providers");

        if (filter is not null) url.SetQueryParam("where", filter.ToString());//SetQueryParam makes html escape
        if (offset is not null && offset > 0) url.SetQueryParam("offset", offset.ToString());
        if (limit is not null && limit > 0) url.SetQueryParam("limit", limit.ToString());


        HttpRequestMessage request = new()
        {
            RequestUri = url.ToUri()
        };
        request.Headers.Add("Accept", "application/json");
        request.Headers.CacheControl = new System.Net.Http.Headers.CacheControlHeaderValue() { NoCache = true };

        HttpResponseMessage response = await client.SendAsync(request, cancellationToken).ConfigureAwait(false);
        if (response.StatusCode == System.Net.HttpStatusCode.OK)
        {
            return await response.Content.ReadFromJsonAsync<Organization[]>(cancellationToken: cancellationToken).ConfigureAwait(false);
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
    /// Retrieves one provider
    /// </summary>
    /// <param name="client"><see cref="HttpClient"/> to deal with.</param>
    /// <param name="providerId">order id to get</param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns>Array of <see cref="Organization"/>.</returns>
    /// <exception cref="Model.Exceptions.ArenalException">Arenal returned an error.</exception>
    /// <exception cref="ArgumentNullException">The request or content was null.</exception>
    public static async Task<Model.Organization> GetProviderAsync(
        this HttpMessageInvoker client,
        string providerId,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrEmpty(providerId)) throw new ArgumentNullException(nameof(providerId));

        Url url = _BaseAddress
            .AppendPathSegment("api")
            .AppendPathSegment("providers")
            .AppendPathSegment(providerId);


        HttpRequestMessage request = new()
        {
            RequestUri = url.ToUri()
        };
        request.Headers.Add("Accept", "application/json");
        request.Headers.CacheControl = new System.Net.Http.Headers.CacheControlHeaderValue() { NoCache = true };

        HttpResponseMessage response = await client.SendAsync(request, cancellationToken).ConfigureAwait(false);
        if (response.StatusCode == System.Net.HttpStatusCode.OK)
        {
            return await response.Content.ReadFromJsonAsync<Organization>(cancellationToken: cancellationToken).ConfigureAwait(false);
        }
        else
        {
            if (string.IsNullOrWhiteSpace(response.Content?.ToString())) throw new Model.Exceptions.ArenalException((int)response.StatusCode, null);
            throw new Model.Exceptions.ArenalException(
                ((int)response.StatusCode),
                await response.Content.ReadFromJsonAsync<Model.Exceptions.ArenalError>(_jOpts, cancellationToken).ConfigureAwait(false));
        }
    }

    #endregion

    #region Forms

    /// <summary>
    /// Generates a form (document) from Arenal.
    /// </summary>
    /// <param name="client"></param>
    /// <param name="reportType"></param>
    /// <param name="base64data"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="NullReferenceException"></exception>
    /// <exception cref="Model.Exceptions.ArenalException"></exception>
    public static async Task<DocumentAnswer> GetFormAsync(
                this HttpMessageInvoker client,
                string reportType, string base64data,
                CancellationToken cancellationToken = default)
    {

        if (base64data == null) throw new NullReferenceException(nameof(base64data));
        if (string.IsNullOrEmpty(reportType)) throw new NullReferenceException(nameof(reportType));

        Url url = _BaseAddress
            .AppendPathSegment("api")
            .AppendPathSegment("forms");

        DocumentRequest docReq = new()
        {
            DocumentType = reportType,
            DocumentFormat = "pdf",
            Data = base64data
        };

        HttpRequestMessage request = new()
        {
            Method = HttpMethod.Post,
            RequestUri = url.ToUri(),
            Content = JsonContent.Create(docReq, typeof(DocumentRequest), options: _jOpts)
        };
        request.Headers.Add("Accept", "application/json");
        request.Headers.CacheControl = new System.Net.Http.Headers.CacheControlHeaderValue() { NoCache = true };

        HttpResponseMessage response = await client.SendAsync(request, cancellationToken).ConfigureAwait(false);
        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<DocumentAnswer>(cancellationToken: cancellationToken).ConfigureAwait(false);
        }
        else
        {
            string ans = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            if (string.IsNullOrWhiteSpace(ans)) throw new Model.Exceptions.ArenalException((int)response.StatusCode, null);
            throw new Model.Exceptions.ArenalException(
                ((int)response.StatusCode), JsonSerializer.Deserialize<Model.Exceptions.ArenalError>(ans, _jOpts));
        }


        #endregion

    }

}