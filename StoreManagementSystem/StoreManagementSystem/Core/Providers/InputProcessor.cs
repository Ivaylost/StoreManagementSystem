using Autofac;
using Autofac.Core.Registration;
using Microsoft.EntityFrameworkCore;
using StoreManagementSystem.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StoreManagementSystem.Core.Providers
{
    public class InputProcessor:IInputProcessor
    {
        private readonly ILifetimeScope scope;

        public InputProcessor(ILifetimeScope scope)
        {
            this.scope = scope;
        }

        public string Process(string input)
        {
            using (var childScope = this.scope.BeginLifetimeScope())
            {
                var name = input.Split(' ')[0];

                try
                {
                    var command = childScope.ResolveNamed<ICommand>(name);
                    var commandPramaters = command.ProvideParameters();
                    return command.Execute(commandPramaters);
                }
                catch (ArgumentException ex)
                {
                    return $"Tried to activate command {name} but got {ex.Message}";
                }
                catch (ComponentNotRegisteredException ex)
                {
                    return ex.Message;
                }
                catch (Exception ex) when (ex is DbUpdateException)
                {
                    while (ex.InnerException != null)
                        ex = ex.InnerException;

                    return $"Sql error occurred: {ex.Message}";
                }
            }
        }

       
    }
}
