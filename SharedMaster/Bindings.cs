using Business;
using Business.Interfaces;
using Data.Repositories;
using Models.Interfaces;
using Ninject.Modules;

namespace SharedMaster
{
    public class Bindings : NinjectModule
    {
        public override void Load()
        {
            Bind<ICustomerManager>().To<CustomerManager>();
            Bind<ICustomerRepository>().To<CustomerRepository>();
        }
    }
}