namespace Infrastructure.EventBus.Generic.IntegrationEvents
{
    public interface IDynamicIntegrationEventHandler
    {
        Task Handle(dynamic eventData);
    }
}
