namespace TypedHttpClientFix.Services;

public class SampleService(HttpClient httpClient)
{
    public async Task<int[]> GetData()
    {
        return await httpClient.GetFromJsonAsync<int[]>("data") ?? [];
    }
}
