using System;
using System.Threading.Tasks;

namespace E_Vision.Core.Interfaces.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Commit changes
        /// </summary>
        Task<bool> Commit(); 
    }
} 
