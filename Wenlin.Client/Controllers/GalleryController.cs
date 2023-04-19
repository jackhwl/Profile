using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net.Http;
using System.Text.Json;
using Wenlin.Client.Models;
using Wenlin.Client.ViewModels;

namespace Wenlin.Client.Controllers;
public class GalleryController : Controller
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ILogger<GalleryController> _logger;

    public GalleryController(IHttpClientFactory httpClientFactory, ILogger<GalleryController> logger)
    {
        _httpClientFactory = httpClientFactory;
        _logger = logger;
    }

    public async Task<IActionResult> Index()
    {
        //await LogIdentityInformation();

        var httpClient = _httpClientFactory.CreateClient("APIClient");

        var request = new HttpRequestMessage(
            HttpMethod.Get,
            "/api/images/");

        var response = await httpClient.SendAsync(
            request, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);

        response.EnsureSuccessStatusCode();

        using (var responseStream = await response.Content.ReadAsStreamAsync())
        {
            var images = await JsonSerializer.DeserializeAsync<List<ImageDto>>(responseStream);
            return View(new GalleryIndexViewModel(images ?? new List<ImageDto>()));
        }
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
