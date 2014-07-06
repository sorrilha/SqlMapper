using SqlMapperTest.Framework;

namespace SqlMapper.Framework
{
    public interface IDataMapper<T> : IDataMapper
    {
        new ISqlEnumerable<T> GetAll();
        void Update(T val);
        void Delete(T val);
        void Insert(T val);
    }

    public interface IDataMapper
    {
        ISqlEnumerable GetAll();
        void Update(object val);
        void Delete(object val);
        void Insert(object val);
    }
}
