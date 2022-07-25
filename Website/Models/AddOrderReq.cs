using System.ComponentModel.DataAnnotations;

namespace Website.Models
{
    public class AddOrderReq
    {
        [Key]
        public int Id { get; set; }
        public int ContactId { get; set; }
        public int ProductId { get; set; }
        public int timeLeftSec { get; set; }
    }
}
