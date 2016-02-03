using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Business.Interfaces;
using Integrations;
using Ninject;
using RabbitMQ.Client.Events;
using Integrations.ReceivingEvents;

namespace SharedMaster
{
    public class Program
    {
        private static IKernel _kernel;

        private static void Main(string[] args)
        {
            InitializeDependencies();
            InitializeMessenging();
            Console.WriteLine("----------SHARED----------");
            Console.WriteLine("Waiting for messages...");
            Console.ReadKey();
        }

        private static void InitializeMessenging()
        {
            var messagingManager = new MessagingManager("localhost");
            var events = _kernel.Get<FinanceEvents>();
            var financeCustomerAdded = events.GetCustomerAddedHandler();
            var financeCustomerDeleted = events.GetCustomerDeletedHandler();
            var financeCustomerModified = events.GetCustomerModifiedHandler();

            messagingManager.Subscribe("Finance.Customer.Added", financeCustomerAdded);
            messagingManager.Subscribe("Finance.Customer.Deleted", financeCustomerDeleted);
            messagingManager.Subscribe("Finance.Customer.Modified", financeCustomerModified);
        }

        private static void InitializeDependencies()
        {
            _kernel = new StandardKernel();
            _kernel.Load(Assembly.GetExecutingAssembly());

            _kernel.Get<ICustomerManager>().GetAllCustomers();
        }
    }
}
