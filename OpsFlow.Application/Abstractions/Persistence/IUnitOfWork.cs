namespace OpsFlow.Application.Abstractions.Persistence
{
    public interface IUnitOfWork : IDisposable
    {
        void CommitAsync(CancellationToken cancellationToken);
        void Dispose();
    }
}