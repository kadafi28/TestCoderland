using Dominio.Interfaces.IModelos;
using Dominio.Modelos.Catalogos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataEF.Repositories.CatalogoRepo
{
    /// <summary>
    ///     Repositorio específico para la entidad MarcasAutos, que hereda de la clase genérica Repository. 
    ///     Implementa la interfaz IMarcaAutosRepository y proporciona métodos adicionales específicos para gestionar las marcas de autos.
    /// </summary>
    public class MarcaAutosRepository : Repository<MarcasAutos>, IMarcaAutosRepository
    {
        /// <summary>
        ///     Constructor que inicializa el repositorio con el contexto de base de datos.
        /// </summary>
        /// <param name="context">El contexto de base de datos utilizado para las operaciones.</param>
        public MarcaAutosRepository(Contexto context) : base(context)
        {
        }

        /// <summary>
        ///     Obtiene todas las marcas de autos de forma asíncrona.
        /// </summary>
        /// <returns>Una lista de objetos MarcasAutos.</returns>
        public async Task<List<MarcasAutos>> GetAllMarcasAutos()
            => await GetAll().ToListAsync();
    }
}
