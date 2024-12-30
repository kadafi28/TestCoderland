using Dominio.Modelos.Catalogos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Interfaces.IModelos
{
    public interface IMarcaAutosRepository : IRepository<MarcasAutos>
    {
        /// <summary>
        ///     Obtiene todas las marcas de autos de forma asíncrona.
        /// </summary>
        /// <returns>Una lista de objetos MarcasAutos.</returns>
        Task<List<MarcasAutos>> GetAllMarcasAutos();
    }
}
