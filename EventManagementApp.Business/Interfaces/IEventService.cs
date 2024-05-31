using EventManagementApp.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagementApp.Business.Interfaces
{
    public interface IEventService
    {
        Task<IEnumerable<EventDto>> GetAllEventsAsync();
        Task<EventDto> GetEventByIdAsync(Guid id);
        Task AddEventAsync(EventDto eventDto);
        Task UpdateEventAsync(EventDto eventDto);
        Task DeleteEventAsync(Guid id);
    }
}
