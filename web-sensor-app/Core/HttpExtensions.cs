using System.Net.Http.Headers;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace web_sensor_app.Core;
public class ApiResultMessage<T>
{
    public T? Data { get; set; }
    public bool IsSuccessful { get; set; }
    public string UserMessage { get; set; } = "";
    public string UserMessageCode { get; set; } = "0";
    public string Message { get; set; } = "";
    public int ErrorCode { get; set; }
    public string? Exception { get; set; }

}

public static class HttpExtensions
{
    public static async Task<R?> GetAsync<R>(this HttpClient httpClient, string url, Dictionary<string, string?> queryParams)
    {
        var urlWithQuery = QueryHelpers.AddQueryString(url, queryParams);
        var response = await httpClient.GetAsync(urlWithQuery);
        return await ReadResponse<R>(response);
    }

    public static async Task<R?> GetAsync<R>(
        this HttpClient httpClient, string url)
    {
        var response = await httpClient.GetAsync(url);
        return await ReadResponse<R>(response);
    }


    public static async Task<R?> PostAsJsonAsync<R>(
        this HttpClient httpClient, string url, object data)
    {
        StringContent content = CreateContent(data);

        var response = await httpClient.PostAsync(url, content);
        return await ReadResponse<R>(response);
    }

    public static async Task<R?> PostAsJsonAsync<R>(
        this HttpClient httpClient, string url)
    {
        StringContent content = CreateContent();

        var response = await httpClient.PostAsync(url, content);
        return await ReadResponse<R>(response);
    }



    private static async Task<R?> ReadResponse<R>(HttpResponseMessage response)
    {
        if (response.IsSuccessStatusCode)
        {
            try
            {
                var result = await response.Content.ReadAsJsonAsync<ApiResultMessage<R>>();
                if (typeof(R) == typeof(EmptyResult))
                {
                    return default;
                }
                else if (result.IsSuccessful)
                {
                    return result.Data;
                }
                else
                {
                    switch (result.ErrorCode)
                    {
                        default:
                            throw new BeException(result.UserMessage, result.ErrorCode, result.UserMessageCode,
                                                  result.Message, null, result.Exception);
                    }
                }
            }
            catch
            {
                var result = await response.Content.ReadAsJsonAsync<ApiResultMessage<object>>();
                throw new BeException(result.UserMessage, result.ErrorCode, result.UserMessageCode, result.Message,
                                      null, result.Exception);
            }
        }
        else
        {
            throw new BeException("Something is wrong!!!", 500, "ServerError", "Error on calling service .x.");
        }
    }

    private static StringContent CreateContent()
    {
        var options = new JsonSerializerSettings
        {
            ContractResolver = new DefaultContractResolver { NamingStrategy = new CamelCaseNamingStrategy() },
            Formatting = Formatting.Indented
        };

        var dataAsString = "{}";
        var content = new StringContent(dataAsString);
        content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
        return content;
    }

    private static StringContent CreateContent(object data)
    {
        var options = new JsonSerializerSettings
        {
            ContractResolver = new DefaultContractResolver { NamingStrategy = new CamelCaseNamingStrategy() },
            Formatting = Formatting.Indented
        };

        var dataAsString = JsonConvert.SerializeObject(data, options);
        var content = new StringContent(dataAsString);
        content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
        return content;
    }

    public static async Task<T> ReadAsJsonAsync<T>(this HttpContent content)
    {
        var dataAsString = await content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<T>(dataAsString);
    }

    public static string PagingQuery(this PagedRequest request)
    {
        string result =
            $"&page={request.Page}&pageSize={request.PageSize}&order={(int)request.Order}&search={request.Search}";
        return result;
    }
    public static void MapPagedRequest<T>(this T model, PagedRequest request) where T : PagedRequest
    {

        model.Page = request.Page;
        model.PageSize = request.PageSize;
        model.Order = request.Order;
        model.Search = request.Search;

    }
    public static Dictionary<string, string> ToQueryString<T>(this T obj)
    {
        var properties = obj?.GetType().GetProperties()
            .Where(p => p.GetValue(obj, null) != null)
            .ToDictionary(k => k.Name, v => HttpUtility.UrlDecode(GetStringValue(v.GetValue(obj, null)!)));

        if (properties == null)
        {
            properties = new Dictionary<string, string>();
        }

        return properties;
    }
    private static string? GetStringValue(object v)
    {
        var dateTimeValue = v as DateTime?;
        if (dateTimeValue != null)
        {
            return dateTimeValue.Value.ToLongDateString();
        }
        else
        {
            return v.ToString();
        }
    }
}
