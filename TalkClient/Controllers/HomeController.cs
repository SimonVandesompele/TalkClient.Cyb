using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TalkClient.Models;

namespace TalkClient.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public HomeController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            var httpClient = _httpClientFactory.CreateClient("TalkApi");

            var route = "api/chat-channels";
            var response = await httpClient.GetAsync(route);

            response.EnsureSuccessStatusCode();

            var channels = await response.Content.ReadFromJsonAsync<IList<ChatChannel>>();

            return View(channels);
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
}
