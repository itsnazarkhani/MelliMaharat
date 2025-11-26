using MelliMaharat.Dal.Repos;
using System;
using System.Collections.Generic;
using System.Text;

namespace MelliMaharat.Infrastructure.Services.Base
{
    public class ServiceBase : IServiceBase
    {
        public StudentRepo Students { get; init; }
        public PresentationRepo Presentations { get; init; }
        public SelectionRepo Selections { get; init; }
        public MasterRepo Masters { get; init; }
        public LessonRepo Lessons { get; init; }

        public ServiceBase(
            StudentRepo studentRepo,
            PresentationRepo presentationRepo,
            SelectionRepo selectionRepo,
            MasterRepo masterRepo,
            LessonRepo lessonRepo)
        {
            Students = studentRepo;
            Presentations = presentationRepo;
            Selections = selectionRepo;
            Masters = masterRepo;
            Lessons = lessonRepo;
        }
    }
}
