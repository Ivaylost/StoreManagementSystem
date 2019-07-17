using StoreManagementSystem.Core.IO;
using System;
using System.Collections.Generic;
using System.Text;

namespace StoreManagementSystem.Core.Providers
{
    public class PramaterValidator:IPramaterValidator
    {
        private readonly IReader reader;
        private readonly IWriter writer;

        public PramaterValidator(IReader reader, IWriter writer)
        {
            this.reader = reader;
            this.writer = writer;
        }
        public string Validate()
        {
            var arguments = "";
            do
            {
                arguments = this.reader.ReadLine();
                if (string.IsNullOrEmpty(arguments))
                {
                    this.writer.WriteLine("Arguments cannot be null or empty! Please enter valid parameters!");
                }

                if (arguments.Length > 20)
                {
                    this.writer.WriteLine("Arguments cannot be longer then 20 chars! Please enter valid parameters!");
                }

            } while (string.IsNullOrEmpty(arguments) || (arguments.Length>20));
            return arguments;
        }
    }
}
