using Jugueteria_Prueba3.Azure;
using Jugueteria_Prueba3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace XUnitTestJuguete
{
    public class XUnitTestProveedor
    {
        [Fact]
        public void TestObtenerProveedor()
        {
            //Arrange
            bool vieneConDatos = false;

            //Act
            var resultado = ProveedorAzure.ObtenerProvedor();
            vieneConDatos = resultado.Any();

            //Assert 
            Assert.True(vieneConDatos);
        }

        [Fact]
        public void TestObtenerProveedorporID()
        {
            //Arrange
            int idProbar = 1;
            Proveedor proveedorRetornado;
            int resultadoEsperado = 1;
            //Act
            proveedorRetornado = ProveedorAzure.ObtenerProveedorporID(idProbar);

            //Assert 
            Assert.Equal(resultadoEsperado, proveedorRetornado.id_proveedor);
        }

        [Fact]
        public void TestAgregarProveedor()
        {
            //Arrange
            int resultadoEsperado = 1;
            int resultadoObtenido = 0;

            Proveedor provedor = new Proveedor();
            provedor.rut = "barbi";
            provedor.nombre = "matel";
            provedor.apellido = "5000";
            provedor.fono = "5000";
            provedor.direccion = "5000";

            //Act
            resultadoObtenido = ProveedorAzure.AgregarProvedor(provedor);

            //Assert 
            Assert.Equal(resultadoEsperado, resultadoObtenido);

        }

        [Fact]
        public void TestEliminarProveedorPorRut()
        {
            //Arrange         
            Proveedor provedor = new Proveedor();
            provedor.rut = "12345";
            provedor.nombre = "matel";
            provedor.apellido = "5000";
            provedor.fono = "5000";
            provedor.direccion = "5000";

            string RutproveedorEliminar = "12345";

            int resultadoEsperado = 1;
            int resultadoObtenido = 0;

            ProveedorAzure.AgregarProvedor(provedor);

            //Act
            resultadoObtenido = ProveedorAzure.EliminarProveedorPorRut(RutproveedorEliminar);

            //Assert 
            Assert.Equal(resultadoEsperado, resultadoObtenido);

        }

        [Fact]
        public void TestActualizarProveedorPorID()
        {
            //Arrange
            int resultadoEsperado = 1;
            int resultadoObtenido = 0;

            Proveedor provedor = new Proveedor();
            provedor.id_proveedor = 1;
            provedor.rut = "1234";
            provedor.nombre = "matel";
            provedor.apellido = "5000";
            provedor.fono = "5000";
            provedor.direccion = "5000";

            //Act
            resultadoObtenido = ProveedorAzure.ActualizarProvedorID(provedor);

            provedor.nombre = "Melisa";
            ProveedorAzure.ActualizarProvedorID(provedor);

            //Assert
            Assert.Equal(resultadoEsperado, resultadoObtenido);
        }


    }
}
