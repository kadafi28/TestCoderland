using Comun.Extensiones;
using Dominio.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace JsonBackend.Controllers._Base
{
    /// <summary>
    ///     Controlador base genérico que proporciona operaciones CRUD comunes para las entidades. 
    ///     Los controladores derivados pueden heredar de esta clase para manejar entidades específicas.
    /// </summary>
    /// <typeparam name="TEntity">El tipo de entidad que maneja este controlador.</typeparam>
    [Route("[controller]")]
    [ApiController]
    public class CrudControllerBase<TEntity> : ControllerBase where TEntity : class
    {
        protected readonly IUnitOfWork _unitOfWork;
        protected readonly IRepository<TEntity> _repository;

        /// <summary>
        ///     Constructor que inicializa el controlador con una instancia de IUnitOfWork y repositorio genérico.
        /// </summary>
        /// <param name="unitOfWork">Unidad de trabajo utilizada para acceder a los repositorios.</param>
        public CrudControllerBase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _repository = unitOfWork.GetRepository<TEntity>();
        }

        /// <summary>
        ///     Obtiene todos los registros de la entidad.
        /// </summary>
        /// <returns>Una respuesta HTTP con la lista de entidades.</returns>
        [HttpGet]
        public virtual async Task<IActionResult> Get()
        {
            var entities = await _repository.GetAll().ToListAsync();
            return Ok(entities);
        }

        /// <summary>
        ///     Obtiene un registro específico por su ID.
        /// </summary>
        /// <param name="id">ID del registro.</param>
        /// <returns>La entidad si se encuentra, o NotFound si no existe.</returns>
        [HttpGet("{id:int}")]
        public virtual async Task<IActionResult> Get(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
            {
                return NotFound();
            }
            return Ok(entity);
        }

        /// <summary>
        ///     Obtiene registros de la entidad de forma paginada.
        /// </summary>
        /// <param name="page">Número de página.</param>
        /// <param name="pageSize">Tamaño de página.</param>
        /// <returns>Una respuesta HTTP con los registros paginados.</returns>
        [HttpGet("{page:int}/{pageSize:int}")]
        public virtual async Task<IActionResult> Get(int page, int pageSize)
        {
            var entities = _repository.GetAll();
            var orderedQuery = GetOrdered(entities);

            return Ok(await orderedQuery.Skip(pageSize * (page - 1)).Take(pageSize).ToListAsync());
        }

        /// <summary>
        ///     Crea un nuevo registro.
        /// </summary>
        /// <param name="entity">Entidad a crear.</param>
        /// <returns>La entidad creada o un error si el modelo no es válido.</returns>
        [HttpPost]
        public virtual async Task<IActionResult> Post([FromBody] TEntity entity)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            await _repository.AddAsync(entity);
            await _unitOfWork.SaveAsync();
            return Ok(entity);
        }

        /// <summary>
        ///     Actualiza un registro existente.
        /// </summary>
        /// <param name="id">ID del registro a actualizar.</param>
        /// <param name="entity">Entidad con los nuevos datos.</param>
        /// <returns>La acción redirigida a la lista actualizada o NotFound si no existe el registro.</returns>
        [HttpPut("{id}")]
        public virtual async Task<IActionResult> Put(int id, [FromBody] TEntity entity)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            if (id != entity.GetValue<int>("Id")) return NotFound();

            try
            {
                await _repository.UpdateAsync(entity);
                await _unitOfWork.SaveAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EntityExists(entity.GetValue<int>("id")))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Get));
        }

        /// <summary>
        ///     Elimina un registro por su ID.
        /// </summary>
        /// <param name="id">ID del registro a eliminar.</param>
        /// <returns>La acción redirigida a la lista actualizada o NotFound si no existe el registro.</returns>
        [HttpDelete("{id}"), ActionName("Delete")]
        public virtual async Task<IActionResult> Delete(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null) return NotFound();

            await _repository.RemoveAsync(entity);
            await _unitOfWork.SaveAsync();
            return RedirectToAction(nameof(Get));
        }

        /// <summary>
        ///     Verifica si una entidad existe por su ID.
        /// </summary>
        /// <param name="id">ID de la entidad.</param>
        /// <returns>True si la entidad existe; de lo contrario, false.</returns>
        private bool EntityExists(int id)
        {
            var entity = _repository.GetByIdAsync(id);
            return entity != null;
        }

        /// <summary>
        ///     Ordena una consulta de entidades de forma descendente por la propiedad "Id".
        /// </summary>
        /// <param name="query">Consulta de entidades.</param>
        /// <returns>Consulta ordenada.</returns>
        /// <exception cref="InvalidOperationException"></exception>
        public IQueryable<TEntity> GetOrdered<TEntity>(IQueryable<TEntity> query)
        {
            // Obtener la primera propiedad de la clase TEntity
            var firstProperty = typeof(TEntity).GetProperties().FirstOrDefault(x => x.Name == "Id");
            if (firstProperty == null) throw new InvalidOperationException("No se encontró ninguna propiedad en la entidad.");

            // Crear una expresión para acceder a la primera propiedad
            var parameter = Expression.Parameter(typeof(TEntity), "x");
            var propertyAccess = Expression.MakeMemberAccess(parameter, firstProperty);
            var orderByExpression = Expression.Lambda(propertyAccess, parameter);

            // Usar el método OrderByDescending genérico con el tipo dinámico
            var resultExpression = Expression.Call(
                typeof(Queryable),
                "OrderByDescending",
                new Type[] { typeof(TEntity), firstProperty.PropertyType },
                query.Expression,
                Expression.Quote(orderByExpression)
            );

            return query.Provider.CreateQuery<TEntity>(resultExpression);
        }
    }
}
