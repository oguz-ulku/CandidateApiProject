namespace CandidateApiProject.Models.Map
{
    public class CustomerMap : BaseClassMap<Customer>
    {
        public CustomerMap()
        {
            Schema("public");
            Table("customer");
            Id(x => x.Id).Column("id").GeneratedBy.SequenceIdentity("customer_id_seq");
            Map(x => x.BirthDate).Column("birth_date");
            Map(x => x.IdentityNo).Column("identity_no");
            Map(x => x.IdentityNoVerified).Column("identity_no_verified");
            Map(x => x.Surname).Column("surname");
            Map(x => x.Name).Column("name");
            Map(x => x.Status).Column("status");
        }
    }
}
