using Dominio.Interfaces.IModelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        ///     Obtener el repositorio generico de la entidad
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        IRepository<TEntity> GetRepository<TEntity>() where TEntity : class;

        /// <summary>
        ///     Guardar cambios en la base de datos
        /// </summary>
        /// <returns></returns>
        int Save();
        /// <summary>
        ///     Guardar cambios en la base de datos asincrono
        /// </summary>
        /// <returns></returns>
        Task SaveAsync();

        #region Repositorios

        /// <summary>
        ///     Indica el repositorio de marcas autos
        /// </summary>
        IMarcaAutosRepository MarcaAutosRepository { get; }

        #endregion
    }
}
