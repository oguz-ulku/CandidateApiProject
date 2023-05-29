namespace CandidateApiProject.Models.Map
{
    public class TransactionMap : BaseClassMap<Transaction>
    {
        public TransactionMap()
        {
            Schema("public");
            Table("transaction");
            Id(x => x.Id).Column("id").GeneratedBy.SequenceIdentity("transaction_id_seq");
            Map(x => x.ResponseMessage).Column("response_message");
            Map(x => x.ResponseCode).Column("response_code");
            Map(x => x.Amount).Column("amount");
            Map(x => x.CardPan).Column("card_pan");
            Map(x => x.OrderId).Column("order_id");
            Map(x => x.Status).Column("status");
            Map(x => x.TypeId).Column("type_id");
        }
    }
}
