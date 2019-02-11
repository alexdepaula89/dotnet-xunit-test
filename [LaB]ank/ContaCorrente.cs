using System;

namespace LaBank
{
    public class ContaCorrente
    {
        private string _nomeCorrentista;

        private double _saldo;

        private bool _isContaBloqueada = false;

        private readonly HistoricoContaCorrente _historicoContaCorrente;

        private ContaCorrente()
        {
        }

        public ContaCorrente(string nomeCorrentista, double saldo)
        {
            _nomeCorrentista = nomeCorrentista;
            _saldo = saldo;
        }

        public ContaCorrente(string nomeCorrentista, double saldo, HistoricoContaCorrente historicoContaCorrente)
        {
            _nomeCorrentista = nomeCorrentista;
            _saldo = saldo;
            _historicoContaCorrente = historicoContaCorrente;
        }

        public string NomeCorrentista
        {
            get { return _nomeCorrentista; }
        }

        public double Saldo
        {
            get { return _saldo; }
        }

        public void Debito(double valor)
        {
            if (_isContaBloqueada)
            {
                throw new Exception("Conta corrente bloqueada.");
            }

            if (valor > _saldo)
            {
                throw new ArgumentOutOfRangeException("valor", "Conta corrente sem saldo.");
            }

            if (valor < 0)
            {
                throw new ArgumentOutOfRangeException("valor", "Valor de debito da conta corrente é menor que zero.");
            }

            _saldo -= valor;
        }

        public void Credito(double valor)
        {
            if (_isContaBloqueada)
            {
                throw new Exception("Conta corrente bloqueada.");
            }
            
            if (valor < 0)
            {
                throw new ArgumentOutOfRangeException("valor", "Valor de credito da conta corrente é menor que zero.");
            }
            
            _historicoContaCorrente.EnviarParaTesouraria(_nomeCorrentista, valor);

            _saldo += valor;
        }

        public void BloquearContaCorrente()
        {
            _isContaBloqueada = true;
        }

        public void DesbloquearContaCorrente()
        {
            _isContaBloqueada = false;
        }
    }
}

