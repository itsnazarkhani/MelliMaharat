using System;
using System.Collections.Generic;
using System.Text;

namespace MelliMaharat.UseCases.DTOs
{
    public record class SelectionDetailsDto (
            Guid Id,
            string LessonName,
            string MasterFullName,
            int Unit,
            string DayHold,
            TimeOnly StartTime,
            TimeOnly EndTime,
            decimal Score
        );
}
