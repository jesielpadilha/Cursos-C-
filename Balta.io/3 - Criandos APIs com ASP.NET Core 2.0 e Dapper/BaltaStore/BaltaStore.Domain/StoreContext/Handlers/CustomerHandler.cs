using System;
using System.Collections.Generic;
using BaltaStore.Domain.StoreContext.Commands.CustomerCommands.Inputs;
using BaltaStore.Domain.StoreContext.Commands.CustomerCommands.Outputs;
using BaltaStore.Domain.StoreContext.Entites;
using BaltaStore.Domain.StoreContext.Repositories;
using BaltaStore.Domain.StoreContext.Services;
using BaltaStore.Domain.StoreContext.VelueObjects;
using BaltaStore.Shared.Commands;
using FluentValidator;

namespace BaltaStore.Domain.StoreContext.Handlers
{
    public class CustomerHandler :
        Notifiable,
        ICommandHandler<CreateCustomerCommand>,
        ICommandHandler<AddAddressCommand>
    {

        private readonly ICustomerRepository _repository;
        private readonly IEmailService _emailService;

        public CustomerHandler(ICustomerRepository repository, IEmailService emailService)
        {
            _repository = repository;
            _emailService = emailService;
        }

        public ICommandResult Handle(CreateCustomerCommand command)
        {
            //Verificar se o CPF já existe
            if (_repository.CheckDocument(command.Document))
                AddNotification("Document", "Este CPF já está em uso");

            //Verificar se o Email já existe
            if (_repository.CheckEmail(command.Document))
                AddNotification("Email", "Este Email já está em uso");

            //Criar os VOs
            var name = new Name(command.FirstName, command.LastName);
            var document = new Document(command.Document);
            var email = new Email(command.Email);

            //Criar a entidade
            var customer = new Customer(name, document, email, command.Phone);

            //Validar as entidades e VO
            AddNotifications(name.Notifications);
            AddNotifications(document.Notifications);
            AddNotifications(email.Notifications);
            AddNotifications(customer.Notifications);

            if (Invalid)
            {
                return new CommandResult(
                    false,
                    "Por favor, corrija os campos abaixo",
                    Notifications
                );
            }

            //Inserir o cliente no banco
            _repository.Save(customer);

            //Email um email de boas vindas
            _emailService.Send(email.Address, "hello@company.com", "Bem vindo!", "Seja bem vindop ao Balta Store");

            //Retornar o resultado para tela
            return new CommandResult(true, "Bem vindo ao Balta Store", new
            {
                Id = customer.Id,
                Name = name.ToString(),
                Email = email.Address
            });
        }

        public ICommandResult Handle(AddAddressCommand command)
        {
            throw new System.NotImplementedException();
        }
    }
}