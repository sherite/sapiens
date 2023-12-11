using System.ComponentModel.DataAnnotations;

namespace Entities.DTOs
{
    public class UserInfoOverview
    {
        [Key]
        public string ID { get; set; }
        public string NAME { get; set; }
        public string NETWORKALIAS { get; set; }
        public string NETWORKDOMAIN { get; set; }
        public string COMPANY { get; set; }
        public int? ENABLE { get; set; }
        public int? EXTERNALUSER { get; set; }
    }
}
