using CollectionHierarchy.Models.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace CollectionHierarchy.Models
{
    public class AddRemoveCollection<T> : AddCollection<T>, IAddRemoveCollection<T>
    {
        private IList<T> addRemove;

        public AddRemoveCollection()
        {
            this.addRemove = new List<T>();
        }
        
        public T Remove(string element)
        {
            var elementRemoved = addRemove[addRemove.Count - 1];
            addRemove.RemoveAt(addRemove.Count - 1);
            return elementRemoved;
        }

        public IReadOnlyCollection<T> AddRemove => (IReadOnlyCollection<T>)this.addRemove;
    }
}
