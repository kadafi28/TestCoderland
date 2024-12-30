using Dominio.Modelos.Catalogos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataEF
{
    public class Contexto : DbContext
    {
        public Contexto(DbContextOptions<Contexto> options) : base(options) { }

        #region Modelos

        public DbSet<MarcasAutos> MarcasAutos => Set<MarcasAutos>();

        #endregion

        /// <summary>
        ///     Configura opciones adicionales para el contexto de la base de datos.
        ///     En este caso, se ignora la advertencia de cambios pendientes en el modelo para evitar que interfiera con la ejecución de la aplicación.
        /// </summary>
        /// <param name="optionsBuilder"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.ConfigureWarnings(warnings => warnings.Ignore(RelationalEventId.PendingModelChangesWarning));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            Seeder(modelBuilder);
        }

        /// <summary>
        ///     Metodo seeder para preconfigurar datos iniciales en la tabla MarcasAutos
        /// </summary>
        /// <param name="modelBuilder"></param>
        private void Seeder(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MarcasAutos>().HasData(
                            new MarcasAutos { Id = 1, Descripcion = "Toyota", FIngreso = DateTime.UtcNow },
                            new MarcasAutos { Id = 2, Descripcion = "Nissan", FIngreso = DateTime.UtcNow },
                            new MarcasAutos { Id = 3, Descripcion = "Hyundai", FIngreso = DateTime.UtcNow },
                            new MarcasAutos { Id = 4, Descripcion = "Suzuki", FIngreso = DateTime.UtcNow }
                        );
        }
    }
}
