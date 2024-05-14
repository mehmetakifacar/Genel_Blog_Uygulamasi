using System.ComponentModel.DataAnnotations;

namespace EntityLayer.Concrete
{
    public class Admin 
    {
        [Key]
        public int AdminId { get; set; }
        [StringLength(50)]
        public string UserName { get; set; }
        [StringLength(50)]
        public string Password { get; set; }
        [StringLength(1)]
        public string Role { get; set; }
    }
}
