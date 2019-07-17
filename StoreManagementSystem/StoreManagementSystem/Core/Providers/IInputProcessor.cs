using System;
using System.Collections.Generic;
using System.Text;

namespace StoreManagementSystem.Core.Providers
{
    public interface IInputProcessor
    {
        string Process(string arguments);
    }
}
