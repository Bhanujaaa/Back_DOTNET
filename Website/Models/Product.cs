using System.ComponentModel.DataAnnotations;

namespace Website.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        public string PName { get; set; }
        public string PCost { get; set; }
    
        
    }
}
