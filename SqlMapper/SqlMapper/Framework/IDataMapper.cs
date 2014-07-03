using System.Collections.Generic;

namespace SqlMapper.Framework
{
    public interface IDataMapper<T>
    {
        IEnumerable<T> GetAll();
        void Update(T val);
        void Delete(T val);
        void Insert(T val);
    }
}
