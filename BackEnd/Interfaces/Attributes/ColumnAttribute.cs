using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, AllowMultiple = false)]
    public sealed class ColumnAttribute : Attribute
    {
        public string ColumnName { get; set; }

        public ColumnAttribute(string name)
        {
            ColumnName = name;
        }

        public override string ToString()
        {
            return ColumnName;
        }
    }
}
