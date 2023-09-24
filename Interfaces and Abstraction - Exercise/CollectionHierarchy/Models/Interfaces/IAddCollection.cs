using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace CollectionHierarchy.Models.Interfaces
{
    
    public interface IAddCollection<T>
    {
        public int AddElement(T element);
    }
}
