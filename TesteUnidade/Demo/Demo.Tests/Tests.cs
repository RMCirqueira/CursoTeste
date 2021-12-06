using Xunit;

namespace Demo.Tests
{
    public class Tests
    {
        [Fact]
        public void Calculadora_Somar_RetornarValorSoma()
        {
            // Arrange
            var calculadora = new Calculadora();
            // Act
            var resultado = calculadora.Somar(2,2);
            // Assert
            Assert.Equal(4, resultado);
        }

        [Theory]
        [InlineData(1,1,2)]
        [InlineData(2,2,4)]
        [InlineData(3,3,6)]
        public void Calculadora_Somar_RetornarValoresSomaCorretos(double v1, double v2, double total)
        {
            // Arrange
            var calculadora = new Calculadora();
            // Act
            var resultado = calculadora.Somar(v1, v2);
            // Assert
            Assert.Equal(total, resultado);
        }

        [Fact]
        public void StringTools_UnirNomes_RetornarNomeCompleto()
        {
            var sut = new StringsTools();

            var nomeCompleto = sut.Unit("Rodrigo", "Martins");

            Assert.Equal("Rodrigo Martins", nomeCompleto);
        }

        [Fact]
        public void StringTools_UnirNomes_DeveIgnorarCase()
        {
            var sut = new StringsTools();

            var nomeCompleto = sut.Unit("Rodrigo", "Martins");

            Assert.Equal("RODRIGO Martins", nomeCompleto, ignoreCase: true);
        }

        [Fact]
        public void StringTools_UnirNomes_DeveConterTrecho()
        {
            var sut = new StringsTools();

            var nomeCompleto = sut.Unit("Rodrigo", "Martins");
             
            Assert.Contains("rtins", nomeCompleto);
        }

        [Fact]
        public void StringTools_UnirNomes_DeveComecarCom()
        {
            var sut = new StringsTools();

            var nomeCompleto = sut.Unit("Rodrigo", "Martins");

            Assert.StartsWith("Ro", nomeCompleto);
        }

        [Fact]
        public void StringTools_UnirNomes_DeveTerminarCom()
        {
            var sut = new StringsTools();

            var nomeCompleto = sut.Unit("Rodrigo", "Martins");

            Assert.EndsWith("ins", nomeCompleto);
        }

        [Fact]
        public void Calculadora_Somar_DeveSerIgual()
        {
            var calculadora = new Calculadora();

            var nomeCompleto = calculadora.Somar(1, 2);

            Assert.Equal(3, nomeCompleto);
        }

        [Fact]
        public void Calculadora_Somar_DeveSerDiferente()
        {
            var calculadora = new Calculadora();

            var nomeCompleto = calculadora.Somar(2, 2);

            Assert.NotEqual(3, nomeCompleto);
        }
    }
}
