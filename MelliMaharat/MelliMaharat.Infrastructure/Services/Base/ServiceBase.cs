using MelliMaharat.Dal.DbContexts;
using MelliMaharat.Dal.Repos;
using MelliMaharat.Dal.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace MelliMaharat.Infrastructure.Services.Base
{
    public class ServiceBase
    {
        protected readonly IUnitOfWork? unitOfWork;
        protected readonly ApplicationDbContext? _context;
        public ServiceBase(ApplicationDbContext context)
        {
            _context = context;
        }
        public ServiceBase(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
    }
}
