using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MEIZU.Model
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property)]
    public class AttClass : Attribute
    {
        public string Name { get; set; }
        public AttClass(string name)
        {
            this.Name = name + ".";
        }
    }
}
