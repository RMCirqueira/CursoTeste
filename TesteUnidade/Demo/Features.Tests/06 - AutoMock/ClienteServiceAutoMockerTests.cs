using Features.Clientes;
using Features.Tests._02___Fixtures;
using MediatR;
using Moq;
using Moq.AutoMock;
using System.Linq;
using System.Threading;
using Xunit;

namespace Features.Tests._06___AutoMock
{
    [Collection(nameof(ClienteCollection))]
    public class ClienteServiceAutoMockerTests
    {
        private readonly ClienteTestsFixture _clienteTestsFixture;

        public ClienteServiceAutoMockerTests(ClienteTestsFixture clienteTestsFixture)
        {
            _clienteTestsFixture = clienteTestsFixture;
        }

        [Fact]
        public void ClienteService_Adicionar_DeveExecutarComSucesso()
        {
            var cliente = _clienteTestsFixture.GerarClienteValido();
            var mocker = new AutoMocker();
            var clienteService = mocker.CreateInstance<ClienteService>();

            clienteService.Adicionar(cliente);

            Assert.True(cliente.EhValido());
            mocker.GetMock<IClienteRepository>().Verify(r => r.Adicionar(cliente), Times.Once());
            mocker.GetMock<IMediator>().Verify(v => v.Publish(It.IsAny<INotification>(), CancellationToken.None), Times.Once());
        }

        [Fact]
        public void ClienteService_Adicionar_DeveFalharClienteInvalido()
        {
            var cliente = _clienteTestsFixture.GerarClienteInvalido();
            var mocker = new AutoMocker();
            var clienteService = mocker.CreateInstance<ClienteService>();

            clienteService.Adicionar(cliente);

            Assert.False(cliente.EhValido());
            mocker.GetMock<IClienteRepository>().Verify(r => r.Adicionar(cliente), Times.Never);
            mocker.GetMock<IMediator>().Verify(v => v.Publish(It.IsAny<INotification>(), CancellationToken.None), Times.Never);
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
        }
    }
}
