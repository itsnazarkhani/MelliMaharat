using System;
using System.Collections.Generic;
using System.Text;

namespace MelliMaharat.UseCases.DTOs
{
    public record class StudentSelectionsDto(
            Guid StudentId,
            List<Guid> PresentationIds
        );
}
