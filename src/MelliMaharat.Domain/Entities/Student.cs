using MelliMaharat.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace MelliMaharat.Domain.Entities
{
    public class Student : BaseEntity
    {
        public string StudentNumber { get; set; } = null!;

        public DateOnly? DateOfBirth { get; set; }

        public string UserId { get; set; } = null!;
    }
}
