using E_Vision.Core.Interfaces.UnitOfWork;
using E_Vision.Infrastructure.Context;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace E_Vision.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        public AppDbContext AppDbContext { get; set; } 
        /// <summary>
        /// Initializes a new instance of the <see cref="UnitOfWork"/> class.
        /// </summary>
        /// <param name="context">The context<see cref="Context"/></param>
        public UnitOfWork(AppDbContext appContext )
        {
            AppDbContext = appContext; 
        }
        public async Task<bool> Commit()
        { 
            try
            {
                return await AppDbContext.SaveChangesAsync() > default(byte);
            }
            catch (Exception exception)
            {
                AppDbContext.ChangeTracker.Entries().ToList().ForEach(x => x.Reload());
                return default; 
            } 
        }
         
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
