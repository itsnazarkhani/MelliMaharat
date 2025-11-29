using System;
using System.Collections.Generic;
using System.Text;

namespace MelliMaharat.Models
{
    public class SelectionTime : BaseEntity
    {
        [Required]
        public DateTime SelectionStart { get; set; }
        [Required]
        public DateTime SelectionEnd { get; set; }
    }
}
