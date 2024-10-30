using System.Data.Entity;

namespace EventManagerMvc.Models
{
    public class EventContext : DbContext
    {
        public EventContext() : base("name=EventContext")
        {
        }

        public DbSet<Event> Events { get; set; }
    }
}
