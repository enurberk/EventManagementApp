using EventManagementApp.Business.Interfaces;
using EventManagementApp.Core.DTOs;
using EventManagementApp.Data.Context;
using EventManagementApp.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagementApp.Business.Services
{
    public class EventService : IEventService
    {
        private readonly ApplicationDbContext _context;

        public EventService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<EventDto>> GetAllEventsAsync()
        {
            var eventList = await _context.Events
                .Select(e => new EventDto
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
                }).ToListAsync();
            return eventList;
        }

        public async Task<EventDto> GetEventByIdAsync(Guid id)
        {
            var e = await _context.Events
                .FirstOrDefaultAsync(e => e.Id == id);

            if (e == null) return null;

            return new EventDto
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
            };
        }

        public async Task AddEventAsync(EventDto eventDto)
        {
            var eventEntity = new Event
            {
                Title = eventDto.Title,
                Location = eventDto.Location,
                Time = eventDto.Time,
                IsFree = eventDto.IsFree,
                Price = eventDto.Price,
                Description = eventDto.Description,
                Image = eventDto.Image,
                EventType = eventDto.EventType
            };

            _context.Events.Add(eventEntity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateEventAsync(EventDto eventDto)
        {
            var eventEntity = await _context.Events.FindAsync(eventDto.Id);
            if (eventEntity != null)
            {
                eventEntity.Title = eventDto.Title;
                eventEntity.Location = eventDto.Location;
                eventEntity.Time = eventDto.Time;
                eventEntity.IsFree = eventDto.IsFree;
                eventEntity.Price = eventDto.Price;
                eventEntity.Description = eventDto.Description;
                eventEntity.Image = eventDto.Image;
                eventEntity.EventType = eventDto.EventType;

                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteEventAsync(Guid id)
        {
            var eventEntity = await _context.Events.FindAsync(id);
            if (eventEntity != null)
            {
                _context.Events.Remove(eventEntity);
                await _context.SaveChangesAsync();
            }
        }
    }
}
