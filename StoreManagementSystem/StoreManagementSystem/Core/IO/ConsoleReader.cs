using System;
using System.Collections.Generic;
using System.Text;

namespace StoreManagementSystem.Core.IO
{
    public class ConsoleReader: IReader
    {
        public string ReadLine() => Console.ReadLine();
    }
}
