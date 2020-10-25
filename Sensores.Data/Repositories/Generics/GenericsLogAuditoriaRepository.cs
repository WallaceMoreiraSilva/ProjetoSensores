using Domain.Interfaces.Generics;
using Infra.Configuration;
using Microsoft.Win32.SafeHandles;
using System;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace Infra.Repository.Generics
{
    public class GenericsLogAuditoriaRepository<T> : IGenericsLogAuditoriaRepository<T>, IDisposable where T : class
    {
        protected readonly ContextBase _context;

        public GenericsLogAuditoriaRepository(ContextBase context)
        {
            _context = context;
        }

        public async Task Add(T Objeto)
        {
            try
            {
                await _context.Set<T>().AddAsync(Objeto);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #region Disposed

        // Flag: Has Dispose already been called?
        bool disposed = false;

        // Instantiate a SafeHandle instance.
        SafeHandle handle = new SafeFileHandle(IntPtr.Zero, true);

        // Public implementation of Dispose pattern callable by consumers.
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        // Protected implementation of Dispose pattern.
        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                handle.Dispose();

                // Free any other managed objects here.               
            }

            disposed = true;
        }
        #endregion
    }
}
