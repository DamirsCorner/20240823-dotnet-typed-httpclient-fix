using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using System.Net.Http.Json;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
using WireMock.Server;

namespace TypedHttpClientFix.Tests;

public class SampleControllerTests
{
    private WebApplicationFactory<Program> factory;
    private WireMockServer server;

    [SetUp]
    public void Setup()
    {
        server = WireMockServer.Start();
        factory = new WebApplicationFactory<Program>().WithWebHostBuilder(builder =>
        {
            builder.ConfigureAppConfiguration(
                (context, config) =>
                {
                    config.AddInMemoryCollection(
                        new Dictionary<string, string?> { ["BaseAddress"] = server.Url }
                    );
                }
            );
        });
    }

    [TearDown]
    public void TearDown()
    {
        factory.Dispose();
        server.Dispose();
    }

    [Test]
    public async Task Get_ReturnsData()
    {
        var client = factory.CreateClient();

        server
            .Given(Request.Create().WithPath("/data").UsingGet())
            .RespondWith(Response.Create().WithBodyAsJson(Enumerable.Range(0, 10)));

        var response = await client.GetFromJsonAsync<int[]>("/api/sample");

        response.Should().BeEquivalentTo(Enumerable.Range(0, 10));
    }
}
