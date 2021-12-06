using Features.Clientes;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Features.Tests._02___Fixtures
{
    [Collection(nameof(ClienteCollection))]
    public class ClienteTesteValido
    {
        private readonly ClienteTestsFixture _clienteTestsFixture;

        public ClienteTesteValido(ClienteTestsFixture clienteTestsFixture)
        {
            _clienteTestsFixture = clienteTestsFixture;
        }

        [Fact(DisplayName = "Cliente_NovoCliente_DeveEstarValido")]
        [Trait("Categoria", "Cliente Trait Testes")]
        public void Cliente_NovoCliente_DeveEstarValido()
        {
            // Arrange
            var cliente = _clienteTestsFixture.GerarClienteValido();

            // Act
            var result = cliente.EhValido();

            // Assert 
            Assert.True(result);
            Assert.Empty(cliente.ValidationResult.Errors);
        }
    }
}
