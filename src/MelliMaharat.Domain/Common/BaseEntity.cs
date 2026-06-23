using System;
using System.Collections.Generic;
using System.Text;

namespace MelliMaharat.Domain.Common
{
    public class BaseEntity
    {
        public Guid Id { get; set; }

        /// <summary>
        /// Represents the timestamp when the entity was first stored in the database.
        /// </summary>
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public byte[]? RowVersion { get; set;  }
    }
}
