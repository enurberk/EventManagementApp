using EventManagementApp.Business.Interfaces;
using EventManagementApp.Core.DTOs;
using EventManagementApp.Data.Context;
using EventManagementApp.UI.Models;
using EventManagementApp.UI.Models.Events;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System.Diagnostics;

namespace EventManagementApp.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<HomeController> _logger;
        private readonly IEventService _eventService;
        private IMemoryCache _memoryCache;
        private const string EventsCacheKey = "eventsCache";

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context, IEventService eventService, IMemoryCache memoryCache)
        {
            _logger = logger;
            _context = context;
            _eventService = eventService;
            _memoryCache = memoryCache;
        }

        public async Task<IActionResult> Index()
        {
            // Cache'de veri olup olmadığını kontrol et
            if (!_memoryCache.TryGetValue(EventsCacheKey, out List<EventViewModel> model))
            {
                // Cache'de yoksa veritabanından verileri al
                var events = await _eventService.GetAllEventsAsync();
                model = events.Select(e => new EventViewModel
                {
                    Id = e.Id,
                    Title = e.Title,
                    Location = e.Location,
                    Time = e.Time,
                    IsFree = e.IsFree,
                    Price = e.Price,
                    Description = e.Description,
                    Image = e.Image,
                    EventType = e.EventType
                }).ToList();

                // Cache ayarları
                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(5))
                    .SetSlidingExpiration(TimeSpan.FromMinutes(2));

                // Verileri cache'e ekle
                _memoryCache.Set(EventsCacheKey, model, cacheEntryOptions);
            }

            return View(model);
        }

        public async Task<IActionResult> Details(Guid id)
        {
            var eventDetails = await _eventService.GetEventByIdAsync(id);
            if (eventDetails == null)
            {
                return NotFound();
            }
            var eventViewModel = new EventViewModel
            {
                Id = eventDetails.Id,
                Title = eventDetails.Title,
                Location = eventDetails.Location,
                Time = eventDetails.Time,
                IsFree = eventDetails.IsFree,
                Price = eventDetails.Price,
                Description = eventDetails.Description,
                Image = eventDetails.Image,
                EventType = eventDetails.EventType
            };
            return View("Details", eventViewModel);
        }

        public IActionResult AddEvent()
        {
            var eventViewModel = new EventViewModel();
            return View("AddEvent", eventViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> AddEvent(EventViewModel model)
        {
            if (ModelState.IsValid)
            {
                var eventDto = new EventDto
                {
                    Title = model.Title,
                    Location = model.Location,
                    Time = model.Time,
                    IsFree = !model.IsFree,
                    Price = model.Price,
                    Description = model.Description,
                    Image = model.Image,
                    EventType = model.EventType
                };

                await _eventService.AddEventAsync(eventDto);

                // Yeni etkinlik eklendiğinde cache'i temizle
                _memoryCache.Remove(EventsCacheKey);

                return RedirectToAction("Index");
            }

            return View(model);
        }

        public async Task<IActionResult> EditEvent(Guid id)
        {
            var eventDto = await _eventService.GetEventByIdAsync(id);
            var eventViewModel = new EventViewModel
            {
                Id = eventDto.Id,
                Title = eventDto.Title,
                Location = eventDto.Location,
                Time = eventDto.Time,
                IsFree = eventDto.IsFree,
                Price = eventDto.Price,
                Description = eventDto.Description,
                Image = eventDto.Image,
                EventType = eventDto.EventType
            };
            return View("EditEvent", eventViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> EditEvent(EventViewModel eventViewModel)
        {
            if (ModelState.IsValid)
            {
                var eventDto = new EventDto
                {
                    Id = eventViewModel.Id,
                    Title = eventViewModel.Title,
                    Location = eventViewModel.Location,
                    Time = eventViewModel.Time,
                    IsFree = eventViewModel.IsFree,
                    Price = eventViewModel.IsFree ? null : eventViewModel.Price,
                    Description = eventViewModel.Description,
                    Image = eventViewModel.Image,
                    EventType = eventViewModel.EventType
                };
                _memoryCache.Remove(EventsCacheKey);
                await _eventService.UpdateEventAsync(eventDto);
                return RedirectToAction("Index");
            }
            return View(eventViewModel);
        }

        public async Task<IActionResult> DeleteEvent(Guid id)
        {
            _memoryCache.Remove(EventsCacheKey);
            await _eventService.DeleteEventAsync(id);
            return RedirectToAction("Index");
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