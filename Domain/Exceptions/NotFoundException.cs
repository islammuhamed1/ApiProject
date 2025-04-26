using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public abstract class NotFoundException : Exception
    {
        protected NotFoundException(string message) : base(message)
        {
        }
        protected NotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
