using Domain.Interfaces.Generics;
using Infra.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace Infra.Repository.Generics
{
    public class GenericsRepository<T> : IGenericsRepository<T>, IDisposable where T : class
    {
        protected readonly ContextBase _context;

        public GenericsRepository(ContextBase context)
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

        public async Task Delete(T Objeto)
        {
            try
            {
                _context.Set<T>().Remove(Objeto);
                await _context.SaveChangesAsync();               
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }                   
        }

        public async Task<T> GetEntityById(int Id)
        {           
            return await _context.Set<T>().FindAsync(Id);            
        }

        public async Task<List<T>> List()
        {
            List<T> lista = new List<T>();

            try
            {
                lista = await _context.Set<T>().AsNoTracking().ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }            

            return lista;                    
        }

        public async Task Update(T Objeto)
        {     
            try
            {
                _context.Set<T>().Update(Objeto);
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
