using Jugueteria_Prueba3.Azure;
using Jugueteria_Prueba3.Models;
using System;
using System.Linq;
using Xunit;

namespace XUnitTestJuguete
{
    public class XUnitTestJuguete
    {
        [Fact]
        public void TestObtenerJuguete()
        {
            //Arrange
            bool vieneConDatos = false;

            //Act
            var resultado = JugueteAzure.ObtenerJuguete();
            vieneConDatos = resultado.Any();

            //Assert 
            Assert.True(vieneConDatos);
        }

        [Fact]
        public void TestObtenerJuguetePorId()
        {
            //Arrange
            int idProbar = 1;
            Juguete JugueteRetornado;
            int resultadoEsperado = 1;
            //Act
            JugueteRetornado = JugueteAzure.ObtenerJugueteporID(idProbar);

            //Assert 
            Assert.Equal(resultadoEsperado, JugueteRetornado.id_juguete);
        }

        [Fact]
        public void TestAgregarPlanta()
        {
            //Arrange
            int resultadoEsperado = 1;
            int resultadoObtenido = 0;

            Juguete juguete = new Juguete();
            juguete.nombre = "barbi";
            juguete.marca = "matel";
            juguete.precioUnit = 5000;

            //Act
            resultadoObtenido = JugueteAzure.AgregarJuguete(juguete);

            //Assert 
            Assert.Equal(resultadoEsperado, resultadoObtenido);

        }

        [Fact]
        public void TestEliminarJuguetePorID()
        {
            //Arrange         
            Juguete juguete = new Juguete();
            juguete.nombre = "ddd";
            juguete.marca = "doo";
            juguete.precioUnit = 52000;

            int IdJugueteEliminar = 2;

            int resultadoEsperado = 1;
            int resultadoObtenido = 0;

            JugueteAzure.AgregarJuguete(juguete);

            //Act
            resultadoObtenido = JugueteAzure.EliminarJugueteporId(IdJugueteEliminar);

            //Assert 
            Assert.Equal(resultadoEsperado, resultadoObtenido);

        }

        [Fact]
        public void TestActualizarPlantaPorId()
        {
            //Arrange
            int resultadoEsperado = 1;
            int resultadoObtenido = 0;

            Juguete juguete = new Juguete();
            juguete.id_juguete = 1;
            juguete.nombre = "Melissa officinalis";
            juguete.marca = "Toronjil";
            juguete.precioUnit = 500;

            //Act
            resultadoObtenido = JugueteAzure.ActualizarJugueteId(juguete);

            juguete.nombre = "Melisa";
            JugueteAzure.ActualizarJugueteId(juguete);

            //Assert
            Assert.Equal(resultadoEsperado, resultadoObtenido);
        }
    }
}
