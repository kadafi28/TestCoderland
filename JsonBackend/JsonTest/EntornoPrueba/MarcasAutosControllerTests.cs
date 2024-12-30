using DataEF;
using DataEF.Repositories;
using DataEF.Repositories.CatalogoRepo;
using Dominio.Interfaces;
using Dominio.Interfaces.IModelos;
using Dominio.Modelos.Catalogos;
using JsonBackend.Controllers.Catalogos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonTest.EntornoPrueba
{
    public class MarcasAutosControllerTests
    {
        private readonly Contexto _dbContext;
        private readonly UnitOfWork _unitOfWork;
        private readonly MarcasAutosController _controller;

        public MarcasAutosControllerTests()
        {
            // Configurar el contexto de memoria
            var options = new DbContextOptionsBuilder<Contexto>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            _dbContext = new Contexto(options);

            // Crear un repositorio específico para Marcas de Autos con el contexto en memoria
            var marcaAutosRepository = new MarcaAutosRepository(_dbContext);

            // Crear una instancia real de UnitOfWork
            _unitOfWork = new UnitOfWork(_dbContext, marcaAutosRepository);

            // Crear el controlador con el UnitOfWork real
            _controller = new MarcasAutosController(_unitOfWork);
        }

        [Fact]
        public async Task GetAllMarcasAutosConRegistro()
        {
            var marcasAutos = new List<MarcasAutos>
        {
            new MarcasAutos { Id = 1, Descripcion = "Marca A" },
            new MarcasAutos { Id = 2, Descripcion = "Marca B" },
            new MarcasAutos { Id = 3, Descripcion = "Marca C" }
        };

            await _dbContext.MarcasAutos.AddRangeAsync(marcasAutos);
            await _dbContext.SaveChangesAsync();

            var result = await _controller.GetAllMarcasAutos();

            // Verifica que el resultado es un OkObjectResult
            var actionResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsAssignableFrom<IEnumerable<MarcasAutos>>(actionResult.Value);

            // Verifica que la cantidad de elementos sea correcta
            Assert.Equal(3, returnValue.Count());
        }

        [Fact]
        public async Task GetAllMarcasAutosSinDatos()
        {
            var result = await _controller.GetAllMarcasAutos();
            var okResult = Assert.IsType<OkObjectResult>(result);
            var marcas = Assert.IsType<List<MarcasAutos>>(okResult.Value);
            Assert.NotNull(marcas);
            Assert.Empty(marcas);
        }

        [Fact]
        public async Task PostMarcasAutos()
        {
            var newMarca = new MarcasAutos { Descripcion = "Marca C" }; // Datos de ejemplo para el nuevo recurso
            
            var result = await _controller.Post(newMarca);

            var actionResult = Assert.IsType<OkObjectResult>(result);
            var createdMarca = Assert.IsType<MarcasAutos>(actionResult.Value);
            Assert.NotEqual(0, createdMarca.Id); // El Id debe ser generado
            Assert.Equal(newMarca.Descripcion, createdMarca.Descripcion); // Verifica que el nombre coincida
            // Verifica que esté guardado en la base de datos
            var savedMarca = await _dbContext.MarcasAutos.FindAsync(createdMarca.Id);
            Assert.NotNull(savedMarca);
            Assert.Equal(newMarca.Descripcion, savedMarca.Descripcion);
        }

        [Fact]
        public async Task PutMarcasAutos()
        {
            var existingMarca = new MarcasAutos { Descripcion = "Marca Antigua" };
            await _dbContext.MarcasAutos.AddAsync(existingMarca);
            await _dbContext.SaveChangesAsync();

            // Detach la entidad original para evitar conflictos de rastreo
            _dbContext.Entry(existingMarca).State = EntityState.Detached;

            var updatedMarca = new MarcasAutos { Id = existingMarca.Id, Descripcion = "Marca Actualizada" };

            var result = await _controller.Put(existingMarca.Id, updatedMarca);

            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal(nameof(_controller.Get), redirectResult.ActionName);

            // Verifica que los datos se hayan actualizado en la base de datos
            var updatedEntity = await _dbContext.MarcasAutos.FindAsync(existingMarca.Id);
            Assert.NotNull(updatedEntity);
            Assert.Equal(updatedMarca.Descripcion, updatedEntity.Descripcion);
        }

        [Fact]
        public async Task DeleteMarcasAutos()
        {
            var marcaToDelete = new MarcasAutos { Descripcion = "Marca a Eliminar" };
            await _dbContext.MarcasAutos.AddAsync(marcaToDelete);
            await _dbContext.SaveChangesAsync();

            var result = await _controller.Delete(marcaToDelete.Id);

            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal(nameof(_controller.Get), redirectResult.ActionName);

            // Verifica que el registro haya sido eliminado de la base de datos
            var deletedEntity = await _dbContext.MarcasAutos.FindAsync(marcaToDelete.Id);
            Assert.Null(deletedEntity);
        }

    }
}
