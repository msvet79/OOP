using CollectionHierarchy.Models.Interfaces;
using System.Collections.ObjectModel;
using System.Collections.Generic;


namespace CollectionHierarchy.Models
{
    public class AddCollection<T> : Collection<T>, IAddCollection<T>
    {
        private ICollection<T> addColl;

        public AddCollection()
        {
            this.addColl = new List<T>();
        }

       public virtual int AddElement(T element)
        {

            this.addColl.Add(element);
            return addColl.Count - 1;
        }

        

        public IReadOnlyCollection<T> AddColl => (IReadOnlyCollection<T>) addColl;
    }
}

