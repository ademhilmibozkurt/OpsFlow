namespace OpsFlow.Application.Abstractions.Persistence
{
    public interface IUnitOfWork : IDisposable
    {
        void CommitAsync();
        void Dispose();
    }
}