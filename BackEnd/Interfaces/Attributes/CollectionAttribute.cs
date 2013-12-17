using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, AllowMultiple = false)]
    public sealed class CollectionAttribute : Attribute
    {
        public string CollectionName { get; set; }

        public CollectionAttribute(string name)
        {
            CollectionName = name;
        }

        public override string ToString()
        {
            return CollectionName;
        }
    }
}
