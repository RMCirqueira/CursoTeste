using Features.Clientes;
using Features.Tests._02___Fixtures;
using FluentAssertions;
using Moq;
using Moq.AutoMock;
using System.Linq;
using Xunit;

namespace Features.Tests._07___FluentAssertions
{
    [Collection(nameof(ClienteCollection))]
    public class ClientFluentAssertionsTests
    {
        private readonly ClienteTestsFixture _clienteTestsFixture;

        public ClientFluentAssertionsTests(ClienteTestsFixture clienteTestsFixture)
        {
            _clienteTestsFixture = clienteTestsFixture;
        }

        [Fact]
        public void ClienteService_ObterTodosAtivos_DeveRetornarApenasClientesAtivos()
        {
            // Arrange
            var mocker = new AutoMocker();
            var clienteService = mocker.CreateInstance<ClienteService>();

            mocker.GetMock<IClienteRepository>().Setup(c => c.ObterTodos())
                .Returns(_clienteTestsFixture.GerarClientesVariados());

            // Act
            var clientes = clienteService.ObterTodosAtivos();

            // Assert 
            mocker.GetMock<IClienteRepository>().Verify(r => r.ObterTodos(), Times.Once);
            Assert.True(clientes.Any());
            Assert.False(clientes.Count(c => !c.Ativo) > 0);

            // Assert 
            mocker.GetMock<IClienteRepository>().Verify(r => r.ObterTodos(), Times.Once);
            clientes.Any().Should().BeTrue();
            clientes.Where(w => !w.Ativo).Should().HaveCountGreaterThan(0);
        }
    }
}
