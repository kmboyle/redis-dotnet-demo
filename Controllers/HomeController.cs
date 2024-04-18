using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using RedisDotnet.Data;
using RedisDotnet.Helpers;
using RedisDotnet.Models;

namespace RedisDotnet.Controllers
{
    public class HomeController : Controller
    {
        private IDistributedCache _cache;

        private readonly ILogger<HomeController> _logger;
        private readonly IUserRepository _userRepository;

        public HomeController(ILogger<HomeController> logger, IDistributedCache cache, IUserRepository userRepository)
        {
            _logger = logger;
            _cache = cache;
            _userRepository = userRepository;
        }

        public async Task<IActionResult> Index()
        {
            List<User>? users;
            string loadLocation;
            string isCacheData;
            string recordKey = $"Users_{DateTime.Now:yyyyMMdd_hhmm}";

            users = await _cache.GetRecordAsync<List<User>>(recordKey);

            if (users is null) // Data not available in the Cache
            {
                users = await _userRepository.GetUsersAsync();
                loadLocation = $"Loaded from DB at {DateTime.Now}";
                isCacheData = "text-danger";

                await _cache.SetRecordAsync<List<User>>(recordKey, users);
            }
            else // Data available in the Cache
            {
                loadLocation = $"Loaded from Cache at {DateTime.Now}";
                isCacheData = "text-success";
            }

            ViewData["Style"] = isCacheData;
            ViewData["Location"] = loadLocation;

            return View(users);
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


