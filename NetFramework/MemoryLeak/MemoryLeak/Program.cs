using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoryLeak
{
    class Program
    {
        static void Main(string[] args)
        {
            Document documentTemplate = new Document()
            {
                Text = "Template",
                Color = "White"
            };

            for (int i = 0; i < 1000; i++)
            {
                Document clonedDocument = documentTemplate.Clone();
                clonedDocument.Text = $"Clone {i}.";

                clonedDocument.Print();
            }
        }
    }
}
