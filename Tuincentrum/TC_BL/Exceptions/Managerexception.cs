using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TC_BL.Exceptions
{
    
    
        [Serializable]
        internal class ManagerException : Exception
        {
            public ManagerException()
            {
            }

            public ManagerException(string? message) : base(message)
            {
            }

            public ManagerException(string? message, Exception? innerException) : base(message, innerException)
            {
            }
        }
    
}
