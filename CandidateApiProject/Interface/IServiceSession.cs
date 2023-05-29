using System.Security.Principal;

namespace CandidateApiProject.Interface
{
    public interface IServiceSession
    {
        void BeginTransaction();
        void Commit();
        void Rollback();
        void CloseTransaction();
        void Save<T>(T entity) where T : class, IEntity, new();
        void Update<T>(T entity) where T : class, IEntity, new();
        void Delete<T>(T entity) where T : class, IEntity, new();
        T Find<T>(long id) where T : class, IEntity, new();
        List<T> Alls<T>() where T : class, IEntity, new();
    }
}
