using System;
using System.Collections.Generic;
using System.Text;

namespace MelliMaharat.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
