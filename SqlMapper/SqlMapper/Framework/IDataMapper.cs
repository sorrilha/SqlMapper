using System.Collections.Generic;
using SqlMapperTest.Framework;

namespace SqlMapper.Framework
{
    public interface IDataMapper<T>
    {
        ISqlEnumerable<T> GetAll();
        void Update(T val);
        void Delete(T val);
        void Insert(T val);
    }

}
