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
        //private readonly DbContextOptions<ContextBase> _OptionsBuilder;        

        //public GenericsRepository()
        //{
        //    _OptionsBuilder = new DbContextOptions<ContextBase>();
        //}       

        //public async Task Add(T Objeto)
        //{
        //    using (var data = new ContextBase(_context))
        //    {
        //        await data.Set<T>().AddAsync(Objeto);
        //        await data.SaveChangesAsync();
        //    }
        //}

        //public async Task Delete(T Objeto)
        //{
        //    using (var data = new ContextBase(_OptionsBuilder))
        //    {
        //        data.Set<T>().Remove(Objeto);
        //        await data.SaveChangesAsync();
        //    }
        //}

        //public async Task<T> GetEntityById(int Id)
        //{
        //    using (var data = new ContextBase(_OptionsBuilder))
        //    {
        //        return await data.Set<T>().FindAsync(Id);
        //    }
        //}

        //public async Task<List<T>> List()
        //{
        //    using (var data = new ContextBase(_OptionsBuilder))
        //    {
        //        return await data.Set<T>().AsNoTracking().ToListAsync();
        //    }
        //}

        //public async Task Update(T Objeto)
        //{
        //    using (var data = new ContextBase(_OptionsBuilder))
        //    {
        //        data.Set<T>().Update(Objeto);
        //        await data.SaveChangesAsync();
        //    }
        //}

        //#region Properties

        //protected readonly ContextBase _context;

        //protected DbSet<T> DbSet
        //{
        //    get
        //    {
        //        return _context.Set<T>();
        //    }
        //}

        //#endregion

        protected readonly ContextBase _context;

        public GenericsRepository(ContextBase context)
        {
            _context = context;
        }

        public async Task Add(T Objeto)
        {            
            await _context.Set<T>().AddAsync(Objeto);
            await _context.SaveChangesAsync();            
        }

        public async Task Delete(T Objeto)
        {
            _context.Set<T>().Remove(Objeto);
            await _context.SaveChangesAsync();            
        }

        public async Task<T> GetEntityById(int Id)
        {           
            return await _context.Set<T>().FindAsync(Id);            
        }

        public async Task<List<T>> List()
        {            
            return await _context.Set<T>().AsNoTracking().ToListAsync();           
        }

        public async Task Update(T Objeto)
        {
            _context.Set<T>().Update(Objeto);
            await _context.SaveChangesAsync();
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
