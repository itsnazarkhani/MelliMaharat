using System;
using System.Collections.Generic;
using System.Text;

namespace MelliMaharat.UseCases.DTOs
{
    public record class NewSelectionTimeDto(
            DateTime SelectionStart,
            DateTime SelectionEnd
        );

    public record class SelectionTimeDto(
            Guid Id,
            DateTime SelectionStart,
            DateTime SelectionEnd
        );
}
