using System;
using System.Collections.Generic;
using System.Text;

namespace MelliMaharat.UseCases.DTOs
{
    public record class PresentationDto(
            Guid Id,
            Guid MasterId,
            string MasterName,
            Guid LessonId,
            string LessonName,
            int Unit,
            string DayHold,
            TimeOnly StartTime,
            TimeOnly EndTime
        );
}
