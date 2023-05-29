namespace CandidateApiProject.Interface
{
    public interface IEntity
    {
        int Id { get; set; }
        long CreateDate { get; set; }
        long UpdateDate { get; set; }
        bool IsActive { get; set; }
    }
}
