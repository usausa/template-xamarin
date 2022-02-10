namespace Template.FormsApp.Services;

using System.Net;
using System.Net.Http;

using Rester;

public sealed class NetworkService : IDisposable
{
    private HttpClient client;

    private readonly Dictionary<string, object> headers = new();

    public NetworkService()
    {
        client = CreateHttpClient();
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:DisposeObjectsBeforeLosingScope", Justification = "Ignore")]
    private static HttpClient CreateHttpClient()
    {
        return new(new HttpClientHandler
        {
            AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate,
            ServerCertificateCustomValidationCallback = (_, _, _, _) => true
        })
        {
            Timeout = new TimeSpan(0, 0, 1, 0)
        };
    }

    public void SetAddress(string address)
    {
        if (client.BaseAddress is not null)
        {
            client.Dispose();
            client = CreateHttpClient();
        }

        client.BaseAddress = String.IsNullOrEmpty(address) ? null : new Uri(address);
    }

    public void SetToken(string token)
    {
        headers["Token"] = token;
    }

    public void Dispose()
    {
        client.Dispose();
    }

    //--------------------------------------------------------------------------------
    // TODO
    //--------------------------------------------------------------------------------

    public async ValueTask<IRestResponse<PingRequest>> PostPingAsync()
    {
        // TODO
        return await client.PostAsync<PingRequest>(
            "api/ping",
            new PingRequest(),
            headers);
    }
}
