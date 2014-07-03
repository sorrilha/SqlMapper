using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlMapperTest.Framework
{
    public interface ISqlEnumerable<T> :IEnumerable<T>
    {
        ISqlEnumerable<T> Where(string clause);
    }
}
