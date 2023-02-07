using GameReview.Domain.Interfaces.Commom;
using GameReview.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameReview.Infrastructure.UnitOfWork
{
    public class UnitOfWork: IUnitOfWork
    {
        public ApplicationContext _context { get; set; }

        public UnitOfWork(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<bool> CommitAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            var result = await _context.SaveChangesAsync();
            return result > 0;
        }

        public async Task DisponseAsync()
        {
            await _context.DisposeAsync();
        }
    }
}
