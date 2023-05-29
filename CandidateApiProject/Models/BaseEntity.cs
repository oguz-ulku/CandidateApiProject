using CandidateApiProject.Interface;

namespace CandidateApiProject.Models
{
    public class BaseEntity : IEntity
    {
        public virtual int Id { get; set; }
        public virtual long CreateDate { get; set; }
        public virtual long UpdateDate { get; set; }
        public virtual bool IsActive { get; set; }
    }
}
