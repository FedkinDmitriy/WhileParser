using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabWhile_6
{
    internal class Token
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public Token(string name, string description)
        {
            this.Name = name;
            this.Description = description;
        }

        public override string ToString()
        {
            string temp = Name +"\n"+ Description;
            return temp;
        }
    }
}
