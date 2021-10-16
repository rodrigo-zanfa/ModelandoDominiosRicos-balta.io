using Flunt.Notifications;
using PaymentContext.Domain.Commands;
using PaymentContext.Domain.Entities;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.Repositories;
using PaymentContext.Domain.Services;
using PaymentContext.Domain.ValueObjects;
using PaymentContext.Shared.Commands;
using PaymentContext.Shared.Handlers;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentContext.Domain.Handlers
{
    public class SubscriptionHandler : Notifiable<Notification>, IHandler<CreateBoletoSubscriptionCommand>, IHandler<CreateCreditCardSubscriptionCommand>, IHandler<CreatePayPalSubscriptionCommand>
    {
        private readonly IStudentRepository _repository;
        private readonly IEmailService _emailService;

        public SubscriptionHandler(IStudentRepository repository, IEmailService emailService)
        {
            _repository = repository;
            _emailService = emailService;
        }

        public ICommandResult Handle(CreateBoletoSubscriptionCommand command)
        {
            // Fail Fast Validations
            command.Validate();
            if (!command.IsValid)
            {
                AddNotifications(command);
                return new CommandResult(false, "Não foi possível realizar sua Assinatura.");
            }

            // verificar se Documento já está cadastrado
            if (_repository.DocumentExists(command.Document))
            {
                AddNotification("Document", "Este CPF já está em uso.");
            }

            // verificar se E-mail já está cadastrado
            if (_repository.EmailExists(command.Email))
            {
                AddNotification("Document", "Este E-mail já está em uso.");
            }

            // gerar os VOs
            var name = new Name(command.FirstName, command.LastName);
            var document = new Document(command.Document, EDocumentType.CPF);
            var email = new Email(command.Email);
            var address = new Address(command.Street, command.Number, command.Neighborhood, command.City, command.State, command.Country, command.ZipCode);

            // gerar as Entidades
            var student = new Student(name, document, email);
            var subscription = new Subscription(DateTime.Now.AddMonths(1));
            var payment = new BoletoPayment(command.BarCode, command.BoletoNumber, command.PaidDate, command.ExpireDate, command.Total, command.TotalPaid, command.Payer, new Document(command.PayerDocument, command.PayerDocumentType), email, address);

            // relacionamentos
            subscription.AddPayment(payment);
            student.AddSubscription(subscription);

            // agrupar as validações
            AddNotifications(name, document, email, address, student, subscription, payment);

            // checar as notificações
            if (!IsValid)
                return new CommandResult(false, "Não foi possível realizar sua Assinatura.");

            // salvar as informações
            _repository.CreateSubscription(student);

            // enviar e-mail de boas vindas
            _emailService.Send(student.Name.ToString(), student.Email.Address, "Seja bem vindo", "Sua Assinatura foi realizada com sucesso!");

            // retornar informações
            return new CommandResult(true, "Assinatura realizada com sucesso!");
        }

        public ICommandResult Handle(CreateCreditCardSubscriptionCommand command)
        {
            // Fail Fast Validations
            command.Validate();
            if (!command.IsValid)
            {
                AddNotifications(command);
                return new CommandResult(false, "Não foi possível realizar sua Assinatura.");
            }

            // verificar se Documento já está cadastrado
            if (_repository.DocumentExists(command.Document))
            {
                AddNotification("Document", "Este CPF já está em uso.");
            }

            // verificar se E-mail já está cadastrado
            if (_repository.EmailExists(command.Email))
            {
                AddNotification("Document", "Este E-mail já está em uso.");
            }

            // gerar os VOs
            var name = new Name(command.FirstName, command.LastName);
            var document = new Document(command.Document, EDocumentType.CPF);
            var email = new Email(command.Email);
            var address = new Address(command.Street, command.Number, command.Neighborhood, command.City, command.State, command.Country, command.ZipCode);

            // gerar as Entidades
            var student = new Student(name, document, email);
            var subscription = new Subscription(DateTime.Now.AddMonths(1));
            var payment = new CreditCardPayment(command.CardHolderName, command.CardNumber, command.LastTransactionNumber, command.PaidDate, command.ExpireDate, command.Total, command.TotalPaid, command.Payer, new Document(command.PayerDocument, command.PayerDocumentType), email, address);

            // relacionamentos
            subscription.AddPayment(payment);
            student.AddSubscription(subscription);

            // agrupar as validações
            AddNotifications(name, document, email, address, student, subscription, payment);

            // checar as notificações
            if (!IsValid)
                return new CommandResult(false, "Não foi possível realizar sua Assinatura.");

            // salvar as informações
            _repository.CreateSubscription(student);

            // enviar e-mail de boas vindas
            _emailService.Send(student.Name.ToString(), student.Email.Address, "Seja bem vindo", "Sua Assinatura foi realizada com sucesso!");

            // retornar informações
            return new CommandResult(true, "Assinatura realizada com sucesso!");
        }

        public ICommandResult Handle(CreatePayPalSubscriptionCommand command)
        {
            // Fail Fast Validations
            command.Validate();
            if (!command.IsValid)
            {
                AddNotifications(command);
                return new CommandResult(false, "Não foi possível realizar sua Assinatura.");
            }

            // verificar se Documento já está cadastrado
            if (_repository.DocumentExists(command.Document))
            {
                AddNotification("Document", "Este CPF já está em uso.");
            }

            // verificar se E-mail já está cadastrado
            if (_repository.EmailExists(command.Email))
            {
                AddNotification("Document", "Este E-mail já está em uso.");
            }

            // gerar os VOs
            var name = new Name(command.FirstName, command.LastName);
            var document = new Document(command.Document, EDocumentType.CPF);
            var email = new Email(command.Email);
            var address = new Address(command.Street, command.Number, command.Neighborhood, command.City, command.State, command.Country, command.ZipCode);

            // gerar as Entidades
            var student = new Student(name, document, email);
            var subscription = new Subscription(DateTime.Now.AddMonths(1));
            var payment = new PayPalPayment(command.TransactionCode, command.PaidDate, command.ExpireDate, command.Total, command.TotalPaid, command.Payer, new Document(command.PayerDocument, command.PayerDocumentType), email, address);

            // relacionamentos
            subscription.AddPayment(payment);
            student.AddSubscription(subscription);

            // agrupar as validações
            AddNotifications(name, document, email, address, student, subscription, payment);

            // checar as notificações
            if (!IsValid)
                return new CommandResult(false, "Não foi possível realizar sua Assinatura.");

            // salvar as informações
            _repository.CreateSubscription(student);

            // enviar e-mail de boas vindas
            _emailService.Send(student.Name.ToString(), student.Email.Address, "Seja bem vindo", "Sua Assinatura foi realizada com sucesso!");

            // retornar informações
            return new CommandResult(true, "Assinatura realizada com sucesso!");
        }
    }
}
