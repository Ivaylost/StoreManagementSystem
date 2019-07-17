using StoreManagementSystem.Core.IO;
using StoreManagementSystem.Core.Providers;
using System;


namespace StoreManagementSystem.Core
{
    public class Engine
    {
        private readonly IReader reader;
        private readonly IWriter writer;
        private readonly IInputProcessor processor;

        public Engine(IReader reader, IWriter writer, IInputProcessor processor)
        {
            this.reader = reader ?? throw new ArgumentNullException(nameof(reader));
            this.writer = writer ?? throw new ArgumentNullException(nameof(writer));
            this.processor = processor ?? throw new ArgumentNullException(nameof(processor));
        }

        public void Run()
        {
            while (true)
            {
                var input = this.reader.ReadLine().ToLower();
                if(input=="end") Environment.Exit(0);
                var output = this.processor.Process(input);
                this.writer.WriteLine(output);
            }
        }
    }
}
