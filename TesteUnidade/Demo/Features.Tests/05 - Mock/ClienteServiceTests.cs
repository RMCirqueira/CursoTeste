using Features.Clientes;
using Features.Tests._02___Fixtures;
using MediatR;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Xunit;

namespace Features.Tests._05___Mock
{
    [Collection(nameof(ClienteCollection))]
    public class ClienteServiceTests
    {
        private readonly ClienteTestsFixture _clienteTestsFixture;

        public ClienteServiceTests(ClienteTestsFixture clienteTestsFixture)
        {
            _clienteTestsFixture = clienteTestsFixture;
        }

        [Fact]
        public void ClienteService_Adicionar_DeveExecutarComSucesso()
        {
            var cliente = _clienteTestsFixture.GerarClienteValido();
            var clienteRepo = new Mock<IClienteRepository>();
            var mediator = new Mock<IMediator>();
            var clienteService = new ClienteService(clienteRepo.Object, mediator.Object);

            clienteService.Adicionar(cliente);

            Assert.True(cliente.EhValido());
            clienteRepo.Verify(r => r.Adicionar(cliente), Times.Once());
            mediator.Verify(v => v.Publish(It.IsAny<INotification>(), CancellationToken.None), Times.Once());
        }

        [Fact]
        public void ClienteService_Adicionar_DeveFalharClienteInvalido()
        {
            var cliente = _clienteTestsFixture.GerarClienteInvalido();
            var clienteRepo = new Mock<IClienteRepository>();
            var mediator = new Mock<IMediator>();
            var clienteService = new ClienteService(clienteRepo.Object, mediator.Object);

            clienteService.Adicionar(cliente);

            Assert.False(cliente.EhValido());
            clienteRepo.Verify(r => r.Adicionar(cliente), Times.Never);
            mediator.Verify(v => v.Publish(It.IsAny<INotification>(), CancellationToken.None), Times.Never);
        }

        [Fact]
        public void ClienteService_ObterTodosAtivos_DeveRetornarApenasClientesAtivos()
        {
            // Arrange
            var clienteRepo = new Mock<IClienteRepository>();
            var mediator = new Mock<IMediator>();

            clienteRepo.Setup(c => c.ObterTodos())
                .Returns(_clienteTestsFixture.GerarClientesVariados());

            var clienteService = new ClienteService(clienteRepo.Object, mediator.Object);

            // Act
            var clientes = clienteService.ObterTodosAtivos();

            // Assert 
            clienteRepo.Verify(r => r.ObterTodos(), Times.Once);
            Assert.True(clientes.Any());
            Assert.False(clientes.Count(c => !c.Ativo) > 0);
        }
    }
}