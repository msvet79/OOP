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
        
        public virtual T Remove()
        {
            var elementRemoved = addRemove[addRemove.Count - 1];
            addRemove.RemoveAt(addRemove.Count - 1);
            return elementRemoved;
        }

        public override int AddElement(T element)
        {
            this.addRemove.Insert(0, element);
            return 0;
        }

        public IReadOnlyCollection<T> AddRemove => (IReadOnlyCollection<T>)this.addRemove;


    }
}
