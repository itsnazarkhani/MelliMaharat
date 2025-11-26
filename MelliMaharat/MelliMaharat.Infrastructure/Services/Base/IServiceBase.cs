using MelliMaharat.Dal.Repos;
using System;
using System.Collections.Generic;
using System.Text;

namespace MelliMaharat.Infrastructure.Services.Base
{
    public interface IServiceBase
    {
        StudentRepo Students { get; init; }
        PresentationRepo Presentations { get; init; }
        SelectionRepo Selections { get; init; }
        MasterRepo Masters { get; init; }
        LessonRepo Lessons { get; init; }
    }
}
