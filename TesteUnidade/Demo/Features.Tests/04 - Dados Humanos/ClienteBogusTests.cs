using Bogus;
using Bogus.DataSets;
using Features.Clientes;
using Features.Tests._02___Fixtures;
using System;
using Xunit;

namespace Features.Tests._04___Dados_Humanos
{
    [Collection(nameof(ClienteCollection))]
    public class ClienteBogusTests
    {
        private readonly ClienteTestsFixture _clienteTestsFixture;

        public ClienteBogusTests(ClienteTestsFixture clienteTestsFixture)
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

        [Fact]
        [Trait("Categoria", "Cliente Trait Testes")]
        public void Cliente_NovoCliente_DeveEstarInvalido()
        {
            // Arrange
            var cliente = _clienteTestsFixture.GerarClienteInvalido();

            // Act
            var result = cliente.EhValido();

            // Assert 
            Assert.False(result);
            Assert.NotEmpty(cliente.ValidationResult.Errors);
        }

        // BOGUS 
        [Fact]
        [Trait("Categoria", "Cliente Trait Testes")]
        public void Teste_Bogus()
        {
            var genero = new Faker().PickRandom<Name.Gender>();

            //var email = new Faker().Internet.Email("eduardo","pires","gmail");
            //var clientefaker = new Faker<Cliente>();
            //clientefaker.RuleFor(c => c.Nome, (f, c) => f.Name.FirstName());

            var clientes = new Faker<Cliente>("pt_BR")
                .CustomInstantiator(f => new Cliente(
                    Guid.NewGuid(),
                    f.Name.FirstName(genero),
                    f.Name.LastName(genero),
                    f.Date.Past(80, DateTime.Now.AddYears(-18)),
                    "",
                    true,
                    DateTime.Now))
                .RuleFor(c => c.Email, (f, c) =>
                      f.Internet.Email(c.Nome.ToLower(), c.Sobrenome.ToLower()));

            Cliente t = clientes;
        }
    }
}
