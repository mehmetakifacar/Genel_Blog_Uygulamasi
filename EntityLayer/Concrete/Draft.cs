using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class Draft
    {
        [Key]
        public int DraftId { get; set; }
        [StringLength(50)]
        public string ReceiverMail { get; set; }
        [StringLength(60)]
        public string Subject { get; set; }
        public string Content { get; set; }
        public DateTime Date { get; set; }
    }
}
