namespace Sherite.ERP.Common
{
    public class Parameters
    {
        public long? FromId { get; set; }
        public long? ToId { get; set; } 
        public Pagination Pagination { get; set; }
        public Ordination Ordination { get; set; }
    }
}
