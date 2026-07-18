using System;
using System.Collections.Generic;
using System.Text;

namespace MelliMaharat.UseCases.DTOs
{
    public record class MasterSummaryDto(
            Guid Id,
            string FullName,
            string Graduation
        );
}
