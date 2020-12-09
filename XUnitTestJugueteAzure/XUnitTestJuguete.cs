using System;
using Xunit;
using System.Linq;
using Jugueteria_Prueba3.Azure;
using Jugueteria_Prueba3.Models;

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
            vieneConDatos = !resultado.Any();

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
            Assert.Equal(resultadoEsperado,JugueteRetornado.id_juguete);
        }
    }
}
