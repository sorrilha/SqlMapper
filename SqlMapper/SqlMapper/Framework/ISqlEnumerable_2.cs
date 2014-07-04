using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SqlMapperTest.Framework
{
    public interface ISqlEnumerable : IEnumerable
    {
        ISqlEnumerable Where(string clause);
    }
}
