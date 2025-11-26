using MelliMaharat.Dal.Repos;
using System;
using System.Collections.Generic;
using System.Text;

namespace MelliMaharat.Infrastructure.Services.Base
{
    public interface IServiceBase
    {
        StudentRepo Students { get; set; }
        PresentationRepo Presentation { get; set; }
        SelectionRepo Selections { get; set; }
        MasterRepo Masters { get; set; }
        LessonRepo Lessons { get; set; }
    }
}
