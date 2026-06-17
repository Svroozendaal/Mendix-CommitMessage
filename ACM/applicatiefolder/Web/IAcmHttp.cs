using System.Net;
using System.Text;

namespace AutoCommitMessage;

/// <summary>
/// Minimal abstraction over an incoming HTTP request so the shared request handler can run on top
/// of either <see cref="HttpListener"/> (the Mendix Studio Pro extension) or ASP.NET Core Kestrel
/// (the standalone browser app).
/// </summary>
public interface IAcmHttpRequest
{
    Uri? Url { get; }
    Encoding ContentEncoding { get; }
    Stream InputStream { get; }
}

/// <summary>
/// Minimal abstraction over an outgoing HTTP response. Members mirror the <see cref="HttpListener"/>
/// surface the handler already used so the routing/sink code stays largely unchanged.
/// </summary>
public interface IAcmHttpResponse
{
    int StatusCode { get; set; }
    string ContentType { get; set; }
    long ContentLength64 { get; set; }
    void SetHeader(string name, string value);
    Stream OutputStream { get; }
}

internal sealed class HttpListenerRequestAdapter(HttpListenerRequest request) : IAcmHttpRequest
{
    private readonly HttpListenerRequest _request = request;

    public Uri? Url => _request.Url;

    public Encoding ContentEncoding => _request.ContentEncoding ?? Encoding.UTF8;

    public Stream InputStream => _request.InputStream;
}

internal sealed class HttpListenerResponseAdapter(HttpListenerResponse response) : IAcmHttpResponse
{
    private readonly HttpListenerResponse _response = response;

    public int StatusCode
    {
        get => _response.StatusCode;
        set => _response.StatusCode = value;
    }

    public string ContentType
    {
        get => _response.ContentType ?? string.Empty;
        set => _response.ContentType = value;
    }

    public long ContentLength64
    {
        get => _response.ContentLength64;
        set => _response.ContentLength64 = value;
    }

    public void SetHeader(string name, string value) => _response.Headers[name] = value;

    public Stream OutputStream => _response.OutputStream;
}
