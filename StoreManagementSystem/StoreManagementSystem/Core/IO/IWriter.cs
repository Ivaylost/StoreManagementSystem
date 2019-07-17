using System;
using System.Collections.Generic;
using System.Text;

namespace StoreManagementSystem.Core.IO
{
    public interface IWriter
    {
        void WriteLine(object output);
    }
}
