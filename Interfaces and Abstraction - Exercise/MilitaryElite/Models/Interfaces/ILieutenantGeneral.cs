using System;
using System.Collections.Generic;
using System.Text;

namespace MilitaryElite.Core.Models
{
    public interface ILieutenantGeneral : IPrivate
    {
         IReadOnlyCollection<IPrivate> Privates { get; }
    }
}
