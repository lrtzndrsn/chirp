namespace Chirp.Web.Tests;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Text.RegularExpressions;

//Code taken from lecture-slides-05 and small parts adapted by: Oline <okre@itu.dk>, Anton <anlf@itu.dk> & Clara <clwj@itu.dk>
public partial class IntegrationTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _fixture;
    private readonly HttpClient _client;

    public IntegrationTests(WebApplicationFactory<Program> fixture)
    {
        _fixture = fixture;
        _client = _fixture.CreateClient(new WebApplicationFactoryClientOptions { AllowAutoRedirect = true, HandleCookies = true });
        Environment.SetEnvironmentVariable("GITHUB_CLIENT_ID", "YEHA");
        Environment.SetEnvironmentVariable("GITHUB_CLIENT_SECRET", "NO?");
    }

    [Fact]
    public async void CanSeePublicTimeline()
    {
        // Arrange
        HttpResponseMessage response = await _client.GetAsync("/");
        response.EnsureSuccessStatusCode();

        // Act
        string content = await response.Content.ReadAsStringAsync();

        // Assert
        Assert.Contains("Chirp!", content);
        Assert.Contains("Public Timeline", content);
    }

    [Theory]
    [InlineData("Helge")]
    [InlineData("Rasmus")]
    public async void CanSeeUserTimeline(string author)
    {
        // Arrange
        HttpResponseMessage response = await _client.GetAsync($"/{author}");
        response.EnsureSuccessStatusCode();

        // Act
        string content = await response.Content.ReadAsStringAsync();

        // Assert
        Assert.Contains("Chirp!", content);
        Assert.Contains($"{author}'s Timeline", content);
    }

    [Fact]
    public async void PageContainsMax32Cheeps()
    {
        // Arrange
        HttpResponseMessage response = await _client.GetAsync("/");
        response.EnsureSuccessStatusCode();

        // Act
        string content = await response.Content.ReadAsStringAsync();
        int cheepCount = 0;

        int listStart = content.IndexOf("<ul id=\"messagelist\" class=\"cheeps\">");
        if (listStart >= 0)
        {
            int listEnd = content.IndexOf("</ul>", listStart);

            if (listEnd >= 0)
            {
                string listContent = content.Substring(listStart, listEnd - listStart);

                // Count the number of list items (list items are represented as "<li>")
                cheepCount = MyRegex().Matches(listContent).Count;
            }
        }

        // Assert
        Assert.Equal(32, cheepCount);

    }

    [Fact]
    public async void HomePageIsEqualToPage1()
    {
        // Arrange
        HttpResponseMessage homePage = await _client.GetAsync("/");
        homePage.EnsureSuccessStatusCode();
        string HPContent = await homePage.Content.ReadAsStringAsync();

        HttpResponseMessage pageOne = await _client.GetAsync("/?pageNumber=1");
        pageOne.EnsureSuccessStatusCode();
        string pageOneContent = await pageOne.Content.ReadAsStringAsync();

        // Assert
        Assert.Equal(HPContent, pageOneContent);

    }

    [GeneratedRegex("<li>")]
    private static partial Regex MyRegex();
}