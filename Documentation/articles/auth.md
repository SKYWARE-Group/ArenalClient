# Authentication and Authorization

<img src="/images/open-id-connect.png" width=200px>

Authentication and Authorization in Arenal are based on [OpenID Connect](https://openid.net/connect/) protocol.

## Machine to machine (M2M)

In M2M case, client application request [JWT](https://jwt.io/) without interaction with the user and uses it for further interaction with Arenal.

```mermaid
sequenceDiagram
    actor CONS as Client App
    participant KC as OpenID Provider
    participant ARN as Arenal

    CONS->>+KC: Request Token
    KC-->-CONS: Token (JWT)

    loop While JWT is valid
        CONS->>+ARN: Action Request
        ARN-->-CONS: Action Answer
    end
```

Example C# code looks like this:

```csharp
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
                    ClientSecret = "secret",
                    Scope = "api-scope",
                    UserName = "user",
                    Password = "password"
                });
            }
            return _tokenResponse.AccessToken;
        }

        private static async Task<Order> PublishOrdersAsync(Order order)
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
                    GivenName = "John" 
                    //Other properties
                }
                //Other properties
            };
            Order respOrd = await PublishOrdersAsync(order);
            Console.WriteLine($"Order created, ArenalId is: {respOrd.ArenalId}");
            //Other interactions with Arenal
        }
       
    }
}

```
