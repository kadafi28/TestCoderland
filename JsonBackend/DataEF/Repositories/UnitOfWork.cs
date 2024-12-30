using Dominio.Interfaces;
using Dominio.Interfaces.IModelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataEF.Repositories
{
    /// <summary>
    ///     Clase UnitOfWork que implementa la interfaz IUnitOfWork.
    ///     Maneja la transacción de múltiples operaciones en la base de datos, proporcionando una manera centralizada de gestionar repositorios y 
    ///     asegurar consistencia en las operaciones de datos.
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        private readonly Contexto _context;

        /// <summary>
        ///     Constructor que inicializa el contexto de base de datos y los repositorios específicos.
        /// </summary>
        /// <param name="context">El contexto de base de datos.</param>
        /// <param name="marcaAutosRepository">Repositorio específico para Marcas de Autos.</param>
        public UnitOfWork(Contexto context,
            IMarcaAutosRepository marcaAutosRepository)
        {
            _context = context;
            _marcaAutosRepository = marcaAutosRepository;
        }

        /// <summary>
        ///     Obtiene un repositorio genérico para el tipo de entidad especificado.
        /// </summary>
        /// <typeparam name="TEntity">El tipo de entidad para el repositorio.</typeparam>
        /// <returns>Un nuevo repositorio para la entidad especificada.</returns>
        public IRepository<TEntity> GetRepository<TEntity>() where TEntity : class
            => new Repository<TEntity>(this._context);

        public void Dispose()
        {
            _context.Dispose();
        }

        /// <summary>
        ///     Guarda los cambios realizados en el contexto de forma sincrónica.
        /// </summary>
        /// <returns>El número de registros afectados.</returns>
        public int Save()
         => _context.SaveChanges();

        /// <summary>
        ///     Guarda los cambios realizados en el contexto de forma asíncrona.
        /// </summary>
        /// <returns>Una tarea que representa la operación de guardado.</returns>
        public Task SaveAsync()
        => _context.SaveChangesAsync();

        #region Repositorios

        private readonly IMarcaAutosRepository _marcaAutosRepository;
        /// <summary>
        ///     Obtiene el repositorio específico para Marcas de Autos.
        /// </summary>
        public IMarcaAutosRepository MarcaAutosRepository => _marcaAutosRepository;

        #endregion
    }
}
