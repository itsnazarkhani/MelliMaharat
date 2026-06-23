using MelliMaharat.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace MelliMaharat.Domain.Entities
{
    public class Instructor : BaseEntity
    {
        public string EmployeeNumber { get; set; } = null!;

        public string UserId { get; set; } = null!;
    }
}
