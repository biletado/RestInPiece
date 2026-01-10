using System.Net;

public class StatusControllerTests : IClassFixture<TestWebApplicationFactory>
{
    private readonly HttpClient _client;

    public StatusControllerTests(TestWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task Health_Returns_200()
    {
        var response = await _client.GetAsync("/api/v3/reservations/status/health");
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task Ready_Returns_200()
    {
        var response = await _client.GetAsync("/api/v3/reservations/status/health/ready");
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task Live_Returns_200()
    {
        var response = await _client.GetAsync("/api/v3/reservations/status/health/live");
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
}