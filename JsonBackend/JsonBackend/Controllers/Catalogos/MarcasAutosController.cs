using Dominio.Interfaces;
using Dominio.Modelos.Catalogos;
using JsonBackend.Controllers._Base;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;

namespace JsonBackend.Controllers.Catalogos
{
    /// <summary>
    ///     Controlador para gestionar las operaciones relacionadas con Marcas de Autos.
    ///     Hereda de <see cref="CrudControllerBase{TEntity}"/> para aprovechar funcionalidades CRUD genéricas.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class MarcasAutosController : CrudControllerBase<MarcasAutos>
    {
        /// <summary>
        ///     Constructor que inicializa el controlador con una instancia de <see cref="IUnitOfWork"/>.
        /// </summary>
        /// <param name="unitOfWork">Unidad de trabajo utilizada para acceder a los repositorios.</param>
        public MarcasAutosController(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        /// <summary>
        ///     Endpoint para obtener todas las marcas de autos.
        /// </summary>
        /// <returns>Una respuesta HTTP con la lista de todas las marcas de autos.</returns>
        [Route("GetAllMarcasAutos")]
        [HttpGet]
        public async Task<IActionResult> GetAllMarcasAutos()
        {
            var marcasAutos = await _unitOfWork.MarcaAutosRepository.GetAllMarcasAutos();
            if (marcasAutos == null) return NotFound(); //En caso de que la lista sea nula
            return Ok(marcasAutos);
        }
    }
}
