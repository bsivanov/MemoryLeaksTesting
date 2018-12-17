using System;
using System.Collections.Generic;

namespace MemoryLeak
{
    public class Document
    {
        private readonly List<Document> badReferenceToClones = new List<Document>();

        public string Text { get; set; }

        public string Color { get; set; }

        public Document Clone()
        {
            Document clonedDocument = new Document
            {
                Text = this.Text,
                Color = this.Color
            };

            this.badReferenceToClones.Add(clonedDocument);

            return clonedDocument;
        }

        public void Print()
        {
            Console.Out.WriteLine($"{Color} document with text {Text}.");
        }
    }
}
