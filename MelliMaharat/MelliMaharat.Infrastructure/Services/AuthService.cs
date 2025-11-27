using MelliMaharat.Dal.Repos;
using MelliMaharat.Infrastructure.Services.Base;
using MelliMaharat.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace MelliMaharat.Infrastructure.Services
{
    public class AuthService : ServiceBase
    {
        public AuthService(
            StudentRepo studentRepo,
            PresentationRepo presentationRepo,
            SelectionRepo selectionRepo,
            MasterRepo masterRepo,
            LessonRepo lessonRepo) : base(
                studentRepo,
                presentationRepo,
                selectionRepo,
                masterRepo,
                lessonRepo)
        { }

        public (bool, UserRoles) SignIn(string username, string password)
        {
            if (Masters.IsUserExist(username))
            {
                if (Masters.IsPasswordMatch(username, password))
                {
                    if (Masters.IsAdmin(username, password))
                        return (true, UserRoles.Admin);
                    else
                        return (true, UserRoles.Master);
                }
                else
                {
                    return (false, UserRoles.None);
                }
            }
            else if (Students.IsUserExist(username))
            {
                if (Students.IsPasswordMatch(username, password))
                {
                    if (Students.IsAdmin(username, password))
                        return (true, UserRoles.Admin);
                    else
                        return (true, UserRoles.Student);
                }
                else
                {
                    return (false, UserRoles.None);
                }
            }
            else
            {
                return (false, UserRoles.None);
            }
        }
    }
}
