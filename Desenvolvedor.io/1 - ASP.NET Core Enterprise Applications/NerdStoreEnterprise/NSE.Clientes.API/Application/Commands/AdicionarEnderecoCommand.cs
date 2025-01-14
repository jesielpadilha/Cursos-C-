﻿using FluentValidation;
using FluentValidation.Results;
using NSE.Core.Messages;
using System;

namespace NSE.Clientes.API.Application.Commands
{
    public class AdicionarEnderecoCommand : Command
    {
        public AdicionarEnderecoCommand(Guid clienteId, string logradouro, string numero, string complemento,
            string bairro, string cep, string cidade, string estado)
        {
            AggregateId = clienteId;
            ClienteId = clienteId;
            Logradouro = logradouro;
            Numero = numero;
            Complemento = complemento;
            Bairro = bairro;
            Cep = cep;
            Cidade = cidade;
            Estado = estado;
        }

        public Guid ClienteId { get; set; }
        public string Logradouro { get; private set; }
        public string Numero { get; private set; }
        public string Complemento { get; private set; }
        public string Bairro { get; private set; }
        public string Cep { get; private set; }
        public string Cidade { get; private set; }
        public string Estado { get; private set; }

        public override bool IsValid()
        {
            ValidationResult = new EnderecoValidation().Validate(this);
            return ValidationResult.IsValid;
        }

        public class EnderecoValidation : AbstractValidator<AdicionarEnderecoCommand>
        {
            public EnderecoValidation()
            {
                RuleFor(c => c.Logradouro)
                    .NotEmpty()
                    .WithMessage("Informe o Logradouro");

                RuleFor(c => c.Numero)
                  .NotEmpty()
                  .WithMessage("Informe o Número");

                RuleFor(c => c.Cep)
                  .NotEmpty()
                  .WithMessage("Informe o CEP");

                RuleFor(c => c.Bairro)
                  .NotEmpty()
                  .WithMessage("Informe o Bairro");

                RuleFor(c => c.Cidade)
                  .NotEmpty()
                  .WithMessage("Informe a Cidade");

                RuleFor(c => c.Estado)
                  .NotEmpty()
                  .WithMessage("Informe o Estado");
            }
        }
    }
}
