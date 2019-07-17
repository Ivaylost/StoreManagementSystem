using System;
using System.Collections.Generic;
using System.Text;

namespace StoreManagementSystem.Commands
{
    public interface ICommand
    {
        string Execute(IReadOnlyList<string> parameters);

        IReadOnlyList<string> ProvideParameters();
    }
}
