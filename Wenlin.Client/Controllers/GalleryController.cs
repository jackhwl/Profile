﻿using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using System.Diagnostics;
using System.Text;
using System.Text.Json;
using Wenlin.Client.Models;
using Wenlin.Client.ViewModels;

namespace Wenlin.Client.Controllers;
[Authorize]
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
        await LogIdentityInformation();

        var httpClient = _httpClientFactory.CreateClient("APIClient");

        var request = new HttpRequestMessage(
            HttpMethod.Get,
            "/api/images/");

        var response = await httpClient.SendAsync(
            request, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);

        response.EnsureSuccessStatusCode();

        using (var responseStream = await response.Content.ReadAsStreamAsync())
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            var images = await JsonSerializer.DeserializeAsync<List<ImageDto>>(responseStream, options);
            return View(new GalleryIndexViewModel(images ?? new List<ImageDto>()));
        }
    }

    //[Authorize(Roles = "PayingUser")]
    [Authorize(Policy = "UserCanAddImage")]
    public IActionResult AddImage()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    //[Authorize(Roles = "PayingUser")]
    [Authorize(Policy = "UserCanAddImage")]
    public async Task<IActionResult> AddImage(AddImageViewModel addImageViewModel)
    {
        if (!ModelState.IsValid)
        {
            return View();
        }

        // create an ImageForCreation instance
        ImageForCreation? imageForCreation = null;

        // take the first (only) file in the Files list
        var imageFile = addImageViewModel.Files.First();

        if (imageFile.Length > 0)
        {
            using (var fileStream = imageFile.OpenReadStream())
            using (var ms = new MemoryStream())
            {
                fileStream.CopyTo(ms);
                imageForCreation = new ImageForCreation(
                    addImageViewModel.Title, ms.ToArray());
            }
        }

        // serialize it
        var serializedImageForCreation = JsonSerializer.Serialize(imageForCreation);

        var httpClient = _httpClientFactory.CreateClient("APIClient");

        var request = new HttpRequestMessage(
            HttpMethod.Post,
            $"/api/images")
        {
            Content = new StringContent(
                serializedImageForCreation,
                System.Text.Encoding.Unicode,
                "application/json")
        };

        var response = await httpClient.SendAsync(
            request, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);

        response.EnsureSuccessStatusCode();

        return RedirectToAction("Index");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
    public async Task LogIdentityInformation()
    {
        // get the saved identity token
        var identityToken = await HttpContext.GetTokenAsync(OpenIdConnectParameterNames.IdToken);

        // get the saved access token
        var accessToken = await HttpContext.GetTokenAsync(OpenIdConnectParameterNames.AccessToken);

        // get the refresh token
        var refreshToken = await HttpContext.GetTokenAsync(OpenIdConnectParameterNames.RefreshToken);

        var userClaimsStringBuilder = new StringBuilder();
        foreach (var claim in User.Claims)
        {
            userClaimsStringBuilder.AppendLine($"Claim type: {claim.Type} - Claim value: {claim.Value}");
        }

        // log token & claims
        _logger.LogInformation($"Identity token & user claims: " + $"\n{identityToken} \n{userClaimsStringBuilder}");
        _logger.LogInformation($"Access token: " + $"\n{accessToken}");
        _logger.LogInformation($"Refresh token: " + $"\n{refreshToken}");
    }

    public async Task<IActionResult> EditImage(Guid id)
    {

        var httpClient = _httpClientFactory.CreateClient("APIClient");

        var request = new HttpRequestMessage(
            HttpMethod.Get,
            $"/api/images/{id}");

        var response = await httpClient.SendAsync(
            request, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);

        response.EnsureSuccessStatusCode();

        using (var responseStream = await response.Content.ReadAsStreamAsync())
        {
            var deserializedImage = await JsonSerializer.DeserializeAsync<Image>(responseStream,
        new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

            if (deserializedImage == null)
            {
                throw new Exception("Deserialized image must not be null.");
            }

            var editImageViewModel = new EditImageViewModel()
            {
                Id = deserializedImage.Id,
                Title = deserializedImage.Title
            };

            return View(editImageViewModel);
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EditImage(EditImageViewModel editImageViewModel)
    {
        if (!ModelState.IsValid)
        {
            return View();
        }

        // create an ImageForUpdate instance
        var imageForUpdate = new ImageForUpdate(editImageViewModel.Title);

        // serialize it
        var serializedImageForUpdate = JsonSerializer.Serialize(imageForUpdate);

        var httpClient = _httpClientFactory.CreateClient("APIClient");

        var request = new HttpRequestMessage(
            HttpMethod.Put,
            $"/api/images/{editImageViewModel.Id}")
        {
            Content = new StringContent(
                serializedImageForUpdate,
                System.Text.Encoding.Unicode,
                "application/json")
        };

        var response = await httpClient.SendAsync(
            request, HttpCompletionOption.ResponseHeadersRead);

        response.EnsureSuccessStatusCode();

        return RedirectToAction("Index");
    }

    public async Task<IActionResult> DeleteImage(Guid id)
    {
        var httpClient = _httpClientFactory.CreateClient("APIClient");

        var request = new HttpRequestMessage(
            HttpMethod.Delete,
            $"/api/images/{id}");

        var response = await httpClient.SendAsync(
            request, HttpCompletionOption.ResponseHeadersRead);

        response.EnsureSuccessStatusCode();

        return RedirectToAction("Index");
    }
}
