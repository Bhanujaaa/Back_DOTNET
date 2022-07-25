using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Website.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        public int ContactId { get; set; }
        public int ProductId { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public DateTime ModifiedDateTime { get; set; }
        public int timeLeftSec { get; set; }

    }
}
