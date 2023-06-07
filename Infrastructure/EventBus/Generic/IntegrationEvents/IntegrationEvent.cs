using Newtonsoft.Json;

namespace Infrastructure.EventBus.Generic.IntegrationEvents
{
    public class IntegrationEvent
    {
        public Guid Id { get; private set; } = Guid.NewGuid();

        public DateTime CreationDate { get; private set; } = DateTime.UtcNow;
    }
}
