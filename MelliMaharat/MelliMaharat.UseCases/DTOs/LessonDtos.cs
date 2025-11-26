using System;
using System.Collections.Generic;
using System.Text;

namespace MelliMaharat.UseCases.DTOs
{
    public record class LessonDto(
            Guid Id,
            string Name,
            int Unit
        );
}
