﻿using System;
using System.Collections.Generic;
using System.Text;

namespace MilitaryElite.Core.Models
{
   public interface ISpecialisedSoldier : IPrivate
    {
        Corps Corps { get; }
    }
}
