using System;


namespace CollectionHierarchy.Core
{
    using CollectionHierarchy.IO;
    using CollectionHierarchy.Models;
    using CollectionHierarchy.Models.Interfaces;
    using Interfaces;
    using System.Collections;
    using System.Collections.Generic;

    public class Engine : IEngine
    {
        private readonly IWriter writer;
        private readonly IReader reader;
        private IAddCollection<string> addCollection;
        private IAddRemoveCollection<string> addRemoveCollection;
        private MyList<string> myList;

        private List<int> resultAddCollection;
        private List<int> resultAddRemoveCollection;
        private List<int> resultAddMyList;
        private List<string> resultRemoveAddRemoveCollection;
        private List<string> resultRemoveMyList;


        public Engine(IWriter writer, IReader reader)
            :this()
        {
            this.writer = writer;
            this.reader = reader;
            

        }

        private Engine()
        {
            this.addCollection = new AddCollection<string>();
            this.addRemoveCollection = new AddRemoveCollection<string>();
            this.myList = new MyList<string>();

            this.resultAddCollection = new List<int>();
            this.resultAddRemoveCollection = new List<int>();
            this.resultAddMyList = new List<int>();
            this.resultRemoveAddRemoveCollection = new List<string>();
            this.resultRemoveMyList = new List<string>();
    }
        public void Run()
        {
            string[] cmdArgAdd = reader.Readline()
                .Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries);

            int removeCount = int.Parse(reader.Readline());

            foreach (string item in cmdArgAdd)
            {
                resultAddCollection.Add(addCollection.AddElement(item));

               

                resultAddRemoveCollection.Add(addRemoveCollection.AddElement(item));

                resultAddMyList.Add(myList.AddElement(item));
                
            }

           for (int i = 0; i < removeCount; i++)
           {
                resultRemoveAddRemoveCollection.Add(addRemoveCollection.Remove());
                resultRemoveMyList.Add(myList.Remove());

           }

            writer.WriteLine(string.Join(" ", resultAddCollection));
            writer.WriteLine(string.Join(" ", resultAddRemoveCollection));
            writer.WriteLine(string.Join(" ", resultAddMyList));
            writer.WriteLine(string.Join(" ", resultRemoveAddRemoveCollection));
            writer.WriteLine(string.Join(" ", resultRemoveMyList));
        }
    }
}
