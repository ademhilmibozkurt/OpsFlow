namespace OpsFlow.Application.Abstractions.Persistence
{
    public interface IUnitOfWork : IDisposable
    {
        void Commit();
        void Dispose();
    }
}