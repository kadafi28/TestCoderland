using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Interfaces
{
    public interface IRepository<T>
        where T : class
    {
        /// <summary>
        ///     Obtener registro por Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<T> GetByIdAsync(int id);
        /// <summary>
        ///     Obtener todos los registros
        /// </summary>
        /// <returns></returns>
        IQueryable<T> GetAll();
        /// <summary>
        ///     Buscar registros usando firs or default
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> expression);
        /// <summary>
        ///     Agregar un registro
        /// </summary>
        /// <param name="entity"></param>
        Task AddAsync(T entity);
        /// <summary>
        ///     Agregar varios registros
        /// </summary>
        /// <param name="entities"></param>
        Task AddRangeAsync(IEnumerable<T> entities);
        /// <summary>
        ///     Eliminar registro
        /// </summary>
        /// <param name="entity"></param>
        Task RemoveAsync(T entity);
        /// <summary>
        ///     Eliminar registro por id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task RemoveByIdAsync(int id);
        /// <summary>
        ///     Eliminar lista de registro
        /// </summary>
        /// <param name="entities"></param>
        Task RemoveRangeAsync(IEnumerable<T> entities);
        /// <summary>
        ///     Eliminar registro masivo segun una condicion
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        Task BulkRemove(Expression<Func<T, bool>> expression);
        /// <summary>
        ///     Actualizar registro
        /// </summary>
        /// <param name="entity"></param>
        Task UpdateAsync(T entity);
        /// <summary>
        ///     Actualizar multiples registro
        /// </summary>
        /// <param name="entities"></param>
        Task UpdateRangeAsync(IEnumerable<T> entities);
    }
}
