using Moq;
using System;
using Xunit;

namespace LaBank.Test
{
    public class ContaCorrenteTest
    {
        public ContaCorrenteTest()
        {
        }

        [Fact]
        public void Debito_ContaCorrenteBloqueada()
        {
            ContaCorrente cc = new ContaCorrente("Sr. Alex de Paula", 11.99);

            cc.BloquearContaCorrente();

            // Deve retornar uma "Exception"
            Action actual = () => cc.Debito(1.99);

            // Valida se retornou a "Exception" esperada
            var exception = Assert.Throws<Exception>(actual);

            // Valida a mensagem
            Assert.Equal("Conta corrente bloqueada.", exception.Message);
        }

        [Fact]
        public void Debito_ContaCorrenteSemSaldo()
        {
            ContaCorrente cc = new ContaCorrente("Sr. Alex de Paula", 11.99);

            cc.DesbloquearContaCorrente();

            // Deve retornar uma "Exception"
            Action actual = () => cc.Debito(12.99);

            // Valida se retornou a "Exception" esperada
            var exception = Assert.Throws<ArgumentOutOfRangeException>(actual);

            // Valida a mensagem
            Assert.Contains("Conta corrente sem saldo.", exception.Message);
        }

        [Fact]
        public void Debito_ValorDebitoContaCorrenteMenorQueZero()
        {
            ContaCorrente cc = new ContaCorrente("Sr. Alex de Paula", 11.99);

            cc.DesbloquearContaCorrente();

            // Deve retornar uma "Exception"
            Action actual = () => cc.Debito(-0.99);

            // Valida se retornou a "Exception" esperada
            var exception = Assert.Throws<ArgumentOutOfRangeException>(actual);

            // Valida a mensagem
            Assert.Contains("Valor de debito da conta corrente é menor que zero.", exception.Message);
        }

        [Fact]
        public void Debito_DebitoRealizadoComSucesso()
        {
            ContaCorrente cc = new ContaCorrente("Sr. Alex de Paula", 11.99);

            cc.DesbloquearContaCorrente();

            // Deve debitar da conta corrente
            cc.Debito(11.99);
            
            // Validar saldo disponivel
            Assert.Equal(0.00, cc.Saldo);
        }

        [Fact]
        public void Credito_CreditoRealizadoComSucesso()
        {
            var historicoContaCorrenteMock = new Mock<HistoricoContaCorrente>();
            historicoContaCorrenteMock.Setup(e => e.EnviarParaTesouraria(It.IsAny<string>(), It.IsAny<double>())).Returns(true);

            ContaCorrente cc = new ContaCorrente("Sr. Alex de Paula", 0, historicoContaCorrenteMock.Object);

            cc.DesbloquearContaCorrente();

            // Deve debitar da conta corrente
            cc.Credito(2.50);

            // Validar saldo disponivel
            Assert.Equal(2.50, cc.Saldo);
        }
    }
}
