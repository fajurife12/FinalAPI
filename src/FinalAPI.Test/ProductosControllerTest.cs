using Xunit;
using MiProyectoAPI.Api.Controllers;
using MiProyectoAPI.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace MiProyectoAPI.Tests
{
    public class ProductosControllerTests
    {
        [Fact]
        public void GetProductos_ReturnsOkResult()
        {
            var controller = new ProductosController();
            var result = controller.GetProductos();
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.NotNull(okResult.Value);
        }

        [Fact]
        public void GetProducto_WithValidId_ReturnsOkResult()
        {
            var controller = new ProductosController();
            var result = controller.GetProducto(1);
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<Producto>(okResult.Value);
            Assert.Equal(1, returnValue.Id);
        }

        [Fact]
        public void GetProducto_WithInvalidId_ReturnsNotFound()
        {
            var controller = new ProductosController();
            var result = controller.GetProducto(999);
            Assert.IsType<NotFoundResult>(result.Result);
        }
    }
}