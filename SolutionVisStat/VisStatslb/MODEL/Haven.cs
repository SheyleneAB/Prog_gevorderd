using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisStatsBL.Exceptions;

namespace VisStatsBL.MODEL
{
    public class Haven
    {
        private string stad;

        public Haven ( string stad)
        {
            Stad = stad;
        }
        public Haven (int? id, string stad)
        {
            Id = id;
            Stad = stad;
        }
        public int? Id;
        public string Stad
        {
            get { return stad; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new DomeinException("Haven_stad");
                stad = value;
            }
        }
        public override string ToString()
        {
            return stad;
        }

    }
}
