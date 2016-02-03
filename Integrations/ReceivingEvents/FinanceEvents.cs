using System;
using System.Text;
using Business.Interfaces;
using Models;
using Newtonsoft.Json;
using RabbitMQ.Client.Events;

namespace Integrations.ReceivingEvents
{
    public class FinanceEvents
    {
        private readonly ICustomerManager _customerManager;
        private readonly MessagingManager _messenger;

        public FinanceEvents(ICustomerManager customerManager)
        {
            _customerManager = customerManager;
            _messenger = new MessagingManager("localhost");
        }

        public EventHandler<BasicDeliverEventArgs> GetCustomerAddedHandler()
        {
            const string queueName = "Shared.Customer.Added";

            return (model, ea) =>
            {
                var customer = GetHydratedCustomer(ea.Body);
                _customerManager.AddCustomer(customer);

                _messenger.CreateQueue(queueName);

                var message = JsonConvert.SerializeObject(customer);

                _messenger.PublishCommand(queueName, message);

                Console.WriteLine("Finance customer with name {0} added", customer.FullName);
            };
        }

        public EventHandler<BasicDeliverEventArgs> GetCustomerDeletedHandler()
        {
            const string queueName = "Shared.Customer.Deleted";

            return (model, ea) =>
            {
                var jsonCustomer = JsonConvert.DeserializeObject<Customer>(Encoding.UTF8.GetString(ea.Body));
                var customer = _customerManager.GetCustomerById(jsonCustomer.Id);
                _customerManager.DeleteCustomer(customer);

                _messenger.CreateQueue(queueName);

                var message = JsonConvert.SerializeObject(customer);

                _messenger.PublishCommand(queueName, message);

                Console.WriteLine("Finance customer with name {0} deleted", jsonCustomer.FullName);
            };
        }

        public EventHandler<BasicDeliverEventArgs> GetCustomerModifiedHandler()
        {
            const string queueName = "Shared.Customer.Deleted";

            return (model, ea) =>
            {
                var customer = GetHydratedCustomer(ea.Body);
                var customerFromContext = HydrateContextCustomer(customer);

                _customerManager.UpdateCustomer();

                var message = JsonConvert.SerializeObject(customer);
                _messenger.PublishCommand(queueName, message);

                Console.WriteLine("Finance customer with name {0} Updated", customerFromContext.FullName);
            };
        }

        private Customer HydrateContextCustomer(Customer customer)
        {
            var customerToUpdate = _customerManager.GetCustomerById(customer.Id);

            customerToUpdate.BillingAddress = customer.BillingAddress;
            customerToUpdate.FullName = customer.FullName;
            customerToUpdate.ShippingAddress = customer.ShippingAddress;

            return customerToUpdate;
        }

        private static Customer GetHydratedCustomer(byte[] body)
        {
            var jsonCustomer = JsonConvert.DeserializeObject<Customer>(Encoding.UTF8.GetString(body));

            jsonCustomer.BillingAddress = new Address
            {
                City = jsonCustomer.BillingAddress != null ? jsonCustomer.BillingAddress.City : jsonCustomer.ShippingAddress.City,
                State = jsonCustomer.BillingAddress != null ? jsonCustomer.BillingAddress.State : jsonCustomer.ShippingAddress.State,
                Street = jsonCustomer.BillingAddress != null ? jsonCustomer.BillingAddress.Street : jsonCustomer.ShippingAddress.Street,
                ZipCode = jsonCustomer.BillingAddress != null ? jsonCustomer.BillingAddress.ZipCode : jsonCustomer.ShippingAddress.ZipCode
            };

            jsonCustomer.ShippingAddress = new Address
            {
                City = jsonCustomer.ShippingAddress != null ? jsonCustomer.ShippingAddress.City : jsonCustomer.BillingAddress.City,
                State = jsonCustomer.ShippingAddress != null ? jsonCustomer.ShippingAddress.State : jsonCustomer.BillingAddress.State,
                Street = jsonCustomer.ShippingAddress != null ? jsonCustomer.ShippingAddress.Street : jsonCustomer.BillingAddress.Street,
                ZipCode = jsonCustomer.ShippingAddress != null ? jsonCustomer.ShippingAddress.ZipCode : jsonCustomer.BillingAddress.ZipCode
            };

            return jsonCustomer;
        }

    }
}