using Dominio.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataEF.Repositories
{
    /// <summary>
    ///     Clase genérica Repository que implementa la interfaz IRepository.
    ///     Proporciona métodos estándar para interactuar con la base de datos, como agregar, actualizar, eliminar y consultar entidades.
    /// </summary>
    /// <typeparam name="T">El tipo de entidad con el que opera el repositorio. Debe ser una clase.</typeparam>
    public class Repository<T> : IRepository<T>
        where T : class
    {
        private readonly Contexto _context;
        private readonly DbSet<T> _dbSet;
        private bool disposed = false;

        /// <summary>
        ///     Constructor que inicializa el contexto y el conjunto de datos para la entidad especificada.
        /// </summary>
        /// <param name="context">El contexto de base de datos.</param>
        public Repository(Contexto context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        /// <summary>
        ///     Agrega una entidad de forma asíncrona al conjunto.
        /// </summary>
        /// <param name="entity">La entidad a agregar.</param>
        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        /// <summary>
        ///     Agrega un rango de entidades de forma asíncrona al conjunto.
        /// </summary>
        /// <param name="entities">Las entidades a agregar.</param>
        public async Task AddRangeAsync(IEnumerable<T> entities)
        {
            await _dbSet.AddRangeAsync(entities);
        }

        /// <summary>
        ///     Obtiene la primera entidad que coincide con la expresión especificada.
        /// </summary>
        /// <param name="expression">La expresión para filtrar los resultados.</param>
        /// <returns>La primera entidad que coincide o null si no se encuentra ninguna.</returns>
        public async Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> expression)
        => await _dbSet.FirstOrDefaultAsync(expression);

        /// <summary>
        ///     Devuelve un IQueryable que representa todas las entidades del conjunto.
        /// </summary>
        /// <returns>Un IQueryable de todas las entidades.</returns>
        public IQueryable<T> GetAll()
            => _dbSet;

        /// <summary>
        ///     Obtiene una entidad por su identificador de forma asíncrona.
        /// </summary>
        /// <param name="id">El identificador de la entidad.</param>
        /// <returns>La entidad encontrada o null si no existe.</returns>
        public async Task<T> GetByIdAsync(int id)
        => await _dbSet.FindAsync(id);

        /// <summary>
        ///     Elimina una entidad del conjunto de forma asíncrona.
        /// </summary>
        /// <param name="entity">La entidad a eliminar.</param>
        public async Task RemoveAsync(T entity)
        {
            _context.Remove(entity);
        }

        /// <summary>
        ///     Elimina una entidad por su identificador de forma asíncrona.
        /// </summary>
        /// <param name="id">El identificador de la entidad a eliminar.</param>
        public async Task RemoveByIdAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            if (entity != null)
                _context.Remove(entity);
        }

        /// <summary>
        ///     Elimina un rango de entidades del conjunto de forma asíncrona.
        /// </summary>
        /// <param name="entities">Las entidades a eliminar.</param>
        public async Task RemoveRangeAsync(IEnumerable<T> entities)
        {
            _context.RemoveRange(entities);
        }

        /// <summary>
        ///     Elimina en bloque todas las entidades que cumplen con una expresión especificada.
        /// </summary>
        /// <param name="expression">La expresión para filtrar las entidades a eliminar.</param>
        public async Task BulkRemove(Expression<Func<T, bool>> expression)
        {
            await _dbSet.Where(expression).ExecuteDeleteAsync();
        }

        /// <summary>
        ///     Actualiza una entidad en el conjunto de forma asíncrona.
        /// </summary>
        /// <param name="entity">La entidad a actualizar.</param>
        public async Task UpdateAsync(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }

        /// <summary>
        ///     Actualiza un rango de entidades en el conjunto de forma asíncrona.
        /// </summary>
        /// <param name="entities">Las entidades a actualizar.</param>
        public async Task UpdateRangeAsync(IEnumerable<T> entities)
        {
            foreach (T entity in entities)
                await UpdateAsync(entity);
        }

        /// <summary>
        ///     Libera los recursos gestionados y no gestionados utilizados por el repositorio.
        /// </summary>
        /// <param name="disposing">Indica si se deben liberar los recursos gestionados.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                    _context.Dispose();
            }
            this.disposed = true;
        }

        /// <summary>
        ///     Libera los recursos del repositorio.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
