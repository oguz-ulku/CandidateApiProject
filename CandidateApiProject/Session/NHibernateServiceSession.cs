using CandidateApiProject.Extensions;
using CandidateApiProject.Interface;
using NHibernate;
using static CandidateApiProject.Models.GenericEnums;
using ISession = NHibernate.ISession;

namespace CandidateApiProject.Session
{
    public class NHibernateServiceSession : IServiceSession
    {
        private readonly ISession _session;
        private ITransaction _transaction;

        public NHibernateServiceSession(ISession session)
        {
            _session = session;
        }

        public void BeginTransaction()
        {
            _transaction = _session.BeginTransaction();
        }

        public void CloseTransaction()
        {
            if (_transaction != null)
            {
                _transaction.Dispose();
                _transaction = null;
            }
        }

        public void Commit()
        {
            _transaction.Commit();
        }

        public void Rollback()
        {
            _transaction.Rollback();
        }

        List<T> IServiceSession.Alls<T>()
        {
            return _session.Query<T>().Where(x => x.IsActive == Convert.ToBoolean(RecordStatus.Active)).ToList();
        }

        void IServiceSession.Delete<T>(T entity)
        {
            entity.SetDeleteDefaults();
            _session.Update(entity);
        }

        T IServiceSession.Find<T>(long id)
        {
            return _session.Query<T>().Where(x => x.Id == id && x.IsActive == Convert.ToBoolean(RecordStatus.Active)).FirstOrDefault();
        }

        void IServiceSession.Save<T>(T entity)
        {
            entity.SetInsertDefaults(_session);
            _session.Save(entity);
        }

        void IServiceSession.Update<T>(T entity)
        {
            entity.SetUpdateDefaults();
            _session.Update(entity);
        }
    }
}
