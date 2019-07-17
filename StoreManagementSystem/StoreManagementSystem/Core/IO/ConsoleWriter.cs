using System;
using System.Collections.Generic;
using System.Text;

namespace StoreManagementSystem.Core.IO
{
    public class ConsoleWriter:IWriter
    {
        public void WriteLine(object output) => Console.WriteLine(output);

    }
}
