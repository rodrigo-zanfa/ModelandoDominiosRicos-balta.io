using Flunt.Validations;
using PaymentContext.Domain.ValueObjects;
using PaymentContext.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PaymentContext.Domain.Entities
{
    public class Student : Entity
    {
        private IList<Subscription> _subscriptions;

        public Student(Name name, Document document, Email email/*, Address address*/)
        {
            Name = name;
            Document = document;
            Email = email;
            //Address = address;

            _subscriptions = new List<Subscription>();

            AddNotifications(name, document, email);
        }

        // Colocando as propriedades como "private set" fazemos com que nenhuma delas possa ser alterada sem um método específico para isso (a não ser no próprio construtor), evitando assim não passar por eventuais validações (regras de negócios específicas).
        // Exemplo:
        //     student.Email = "hello@world.com";
        // Deverá ser alterado pelo método que faz a alteração do e-mail:
        //     student.ChangeEmail("hello@world.com");

        // Já a propriedade lista de subscriptions foi alterada de "List<Subscription>" para "IReadOnlyCollection<Subscription>" SEM o "set / private set", e a adição de um novo item somente poderá ser feito com um método específico para isso.
        // Onde antes a chamada era:
        //     student.Subscriptions.Add(subscription);
        // Agora deverá ser alterado pelo método que faz a adição de uma nova subscription:
        //     student.AddSubscription(subscription);
        // A lista (que é privada) somente será retornada pelo "get" da propriedade, e somente será modificada pelo método específico (AddSubscription).

        public Name Name { get; private set; }
        public Document Document { get; private set; }
        public Email Email { get; private set; }
        public Address Address { get; private set; }
        public IReadOnlyCollection<Subscription> Subscriptions { get { return _subscriptions.ToArray(); } }

        public void AddSubscription(Subscription subscription)
        {
            //// cancela todas as assinaturas ativas e coloca a atual como principal
            //foreach (var sub in Subscriptions)
            //{
            //    //sub.Active = false;
            //    sub.Inactivate();
            //}

            //_subscriptions.Add(subscription);

            var hasActiveSubscription = false;
            foreach (var sub in Subscriptions)
            {
                if (sub.Active)
                    hasActiveSubscription = true;
            }

            AddNotifications(new Contract<Student>()
                .Requires()
                .IsFalse(hasActiveSubscription, "Student.Subscriptions", "Você já tem uma Assinatura ativa.")
                .AreNotEquals(0, subscription.Payments.Count, "Student.Subscriptions.Payments", "Esta Assinatura não possui Pagamentos."));

            if (IsValid)  // só adiciona se for válido
                _subscriptions.Add(subscription);
        }
    }
}
