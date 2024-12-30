using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Modelos
{
    /// <summary>
    ///     Clase con los campos basico de una entidad
    /// </summary>
    public class ModelBase
    {
        /// <summary>
        ///     Llave primaria de la entidad
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        ///     Fecha de ingreso del registro, por defecto la fecha y hora actual
        /// </summary>
        public DateTime FIngreso { get; set; } = DateTime.Now;
    }
}
