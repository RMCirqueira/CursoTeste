using Features.Clientes;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Features.Tests._02___Fixtures
{
    [CollectionDefinition(nameof(ClienteCollection))]
    public class ClienteCollection : ICollectionFixture<ClienteTestsFixture>
    { }

    public class ClienteTestsFixture : IDisposable
    {
        public Cliente GerarClienteValido()
        {
            var cliente = new Cliente(
                Guid.NewGuid(),
                "Rodrigo",
                "Martins",
                DateTime.Now.AddYears(-30),
                "rodrigo@rod.com",
                true,
                DateTime.Now);
            return cliente;
        }

        public Cliente GerarClienteInvalido()
        {
            var cliente = new Cliente(
                Guid.NewGuid(),
                "",
                "",
                DateTime.Now,
                "edu2edu.com",
                true,
                DateTime.Now);
            return cliente;
        }

        public void Dispose()
        {
        }
    }
}
