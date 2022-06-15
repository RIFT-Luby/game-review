using GameReview.Domain.Interfaces.Commom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameReview.Infrastructure.UnitOfWork
{
    public class UnitOfWork: IUnitOfWork
    {

        public async Task<bool> CommitAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return true;
        }

        public async Task DisponseAsync()
        {
        }
    }
}
