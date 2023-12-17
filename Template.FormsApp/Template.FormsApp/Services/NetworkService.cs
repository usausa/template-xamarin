namespace Template.FormsApp.Services;

using Rester;

public sealed class NetworkService : IDisposable
{
    private HttpClient client;

    private readonly Dictionary<string, object> headers = new();

    public NetworkService()
    {
        client = CreateHttpClient();
    }

    private static HttpClient CreateHttpClient()
    {
#pragma warning disable CA2000
        return new(new HttpClientHandler
        {
            AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate,
            ServerCertificateCustomValidationCallback = (_, _, _, _) => true
        })
        {
            Timeout = new TimeSpan(0, 0, 1, 0)
        };
#pragma warning restore CA2000
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

    public ValueTask<IRestResponse<PingRequest>> PostPingAsync()
    {
        // TODO
        return client.PostAsync<PingRequest>(
            "api/ping",
            new PingRequest(),
            headers);
    }
}
