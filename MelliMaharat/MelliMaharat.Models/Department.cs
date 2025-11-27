using System;
using System.Collections.Generic;
using System.Text;

namespace MelliMaharat.Models
{
    public class Department : BaseEntity
    {
        [Required]
        [StringLength(100)]
        public string DepartmentName { get; set; }

        public ICollection<Master> Masters { get; set; } = new List<Master>();
    }
}
