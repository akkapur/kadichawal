using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces.Attributes
{
    /// <summary>
    /// To be used by all properties that need to be text indexed for full text searching.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public sealed class IsTextIndexedAttribute : Attribute
    {
        public IsTextIndexedAttribute()
        {

        }
    }
}
