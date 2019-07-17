using Autofac;
using StoreManagementSystem.Commands;
using StoreManagementSystem.Core;
using StoreManagementSystem.Core.IO;
using StoreManagementSystem.Core.Providers;
using StoreManagementSystem.Data.Context;
using StoreManagementSystem.Services;
using StoreManagementSystem.Services.Interfaces;
using System;
using System.Linq;
using System.Reflection;

namespace StoreManagementSystem.Composition
{
    public class StoreManagementSystemModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ShopContext>().AsSelf()
                .InstancePerLifetimeScope();
            builder.RegisterType<CategoryService>().As<ICategoryService>();
            builder.RegisterType<CustomerService>().As<ICustomerService>();
            builder.RegisterType<OrderService>().As<IOrderService>();
            builder.RegisterType<ProductService>().As<IProductService>();
            builder.RegisterType<SupplierService>().As<ISupplierService>();
            builder.RegisterType<PramaterValidator>().As<IPramaterValidator>();
            builder.RegisterType<PrintService>().As<IPrintService>();


            builder.RegisterType<Engine>().AsSelf();
            builder.RegisterType<InputProcessor>().As<IInputProcessor>();
            builder.RegisterType<ConsoleReader>().As<IReader>().SingleInstance();
            builder.RegisterType<ConsoleWriter>().As<IWriter>().SingleInstance();

            RegisterCommands(builder);
        }

        private void RegisterCommands(ContainerBuilder builder)
        {
            var commands = Assembly.GetExecutingAssembly()
                .DefinedTypes
                .Where(t => t.ImplementedInterfaces.Contains(typeof(ICommand)));

            foreach (var command in commands)
            {
                builder.RegisterType(command.AsType())
                    .Named<ICommand>(command.Name.ToLower().Replace("command", ""));
            }
        }
    }
}
