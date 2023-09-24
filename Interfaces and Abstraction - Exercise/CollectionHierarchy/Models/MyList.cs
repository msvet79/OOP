using CollectionHierarchy.Models.Interfaces;
using System;
using System.Collections.Generic;

namespace CollectionHierarchy.Models
{
    public class MyList<T> : AddRemoveCollection<T>, IMyList
    {
        private IList<T> myListOnly;
        public MyList()
        {
            this.myListOnly = new List<T>();
        }
        
        public override T Remove()
            {
            var elementRemoved = myListOnly[0];
            myListOnly.RemoveAt(0);
            return elementRemoved;


        }

        public int Used => myListOnly.Count;

        public IReadOnlyCollection<T> MyListOnly => (IReadOnlyCollection<T>)this.myListOnly;

        public override int AddElement(T element)
        {
            this.myListOnly.Insert(0, element);
            return 0;
        }

    }

   


}

