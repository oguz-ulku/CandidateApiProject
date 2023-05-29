using CandidateApiProject.Interface;
using CandidateApiProject.Models.Map;
using CandidateApiProject.Session;
using FluentNHibernate.Cfg;
using NHibernate;
using static CandidateApiProject.Models.GenericEnums;
using ISession = NHibernate.ISession;

namespace CandidateApiProject.Extensions
{
    public static class NHibernateExtensions
    {
        public static IServiceCollection AddNHibernate(this IServiceCollection services, string connectionString)
        {
            services.AddSingleton<ISessionFactory>(factory =>
            {
                return Fluently.Configure()
                .Database(
                    () =>
                    {
                        return FluentNHibernate.Cfg.Db.PostgreSQLConfiguration.Standard
                        .ShowSql()
                        .ConnectionString(connectionString);
                    }
                )
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<CustomerMap>())
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<TransactionMap>())
                .BuildSessionFactory();
            }
);

            services.AddSingleton<ISession>(factory => factory.GetServices<ISessionFactory>().First().OpenSession());

            services.AddScoped<IServiceSession, NHibernateServiceSession>();

            return services;
        }

        public static void SetInsertDefaults(this IEntity entity, ISession session)
        {
            entity.IsActive = Convert.ToBoolean(RecordStatus.Active);
            entity.CreateDate = DateTime.Now.ToLong14();
        }

        public static void SetUpdateDefaults(this IEntity entity, bool isActive = true, bool setUpdateFields = true)
        {
            entity.UpdateDate = DateTime.Now.ToLong14();
        }

        public static void SetDeleteDefaults(this IEntity entity)
        {
            entity.IsActive = Convert.ToBoolean(RecordStatus.Passive);
            entity.UpdateDate = DateTime.Now.ToLong14();
        }
    }
}
