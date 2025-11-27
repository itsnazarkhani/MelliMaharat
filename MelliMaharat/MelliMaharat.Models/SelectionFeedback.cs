using System;
using System.Collections.Generic;
using System.Text;

namespace MelliMaharat.Models
{
    public class SelectionFeedback : BaseEntity
    {
        [Range(1, 5)]
        public int Rating { get; set; }

        public Guid SelectionId { get; set; }
        public Selection Selection { get; set; }
    }
}
