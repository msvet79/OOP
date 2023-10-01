﻿using System;
using System.Collections.Generic;
using System.Text;

namespace CollectionHierarchy.Models.Interfaces
{
    public interface IAddRemoveCollection<T> : IAddCollection<T>
    {
        public T Remove();
    }
}
