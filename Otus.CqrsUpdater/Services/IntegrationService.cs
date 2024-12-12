namespace Otus.CqrsUpdater.Services
{
    public class IntegrationService
    {
        public Task SendAsync<T>(T entity)
        {
            return Task.CompletedTask;
        }
    }
}
