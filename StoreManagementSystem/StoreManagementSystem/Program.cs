using Autofac;
using StoreManagementSystem.Core;
using StoreManagementSystem.Composition;

namespace StoreManagementSystem
{
    class Program
    {
        static void Main()
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule<StoreManagementSystemModule>();
            var container = builder.Build();

            var engine = container.Resolve<Engine>();
            engine.Run();
        }
    }
}
