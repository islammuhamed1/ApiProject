﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public sealed class ProdcutNotFoundException : NotFoundException
    {
        public ProdcutNotFoundException(int id) : base($"Product with id {id} not found.")
        {

        }
    }

}
