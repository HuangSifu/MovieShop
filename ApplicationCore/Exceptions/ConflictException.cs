using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Exceptions
{
    public class ConflictException: Exception//inherited from C# Exception class
    {
        public ConflictException(string message) : base(message)
        {

        }
    }
}
