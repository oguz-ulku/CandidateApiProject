using CandidateApiProject.Interface;
using FluentNHibernate.Mapping;

namespace CandidateApiProject.Models.Map
{
    public class BaseClassMap<T> : ClassMap<T> where T : class, IEntity, new()
    {
        public BaseClassMap()
        {
            Id(x => x.Id).GeneratedBy.Assigned().Column("id");
            Map(x => x.CreateDate).Column("create_date").Not.Nullable();
            Map(x => x.IsActive).Column("is_active").Not.Nullable();
            Map(x => x.UpdateDate).Column("update_date");
        }
    }
}
