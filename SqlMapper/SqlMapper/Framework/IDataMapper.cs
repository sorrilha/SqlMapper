using System;
using System.Collections.Generic;
using System.Reflection;
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
        SqlEnumerable GetAll();
        void Update(object val);
        void Delete(object val);
        void Insert(object val);
    }

    class DataMapper : IDataMapper
    {

        public DataMapper(Object x)
        {
            
        }
        public SqlEnumerable GetAll()
        {
            throw new System.NotImplementedException();
        }

        public void Update(object val)
        {
            throw new System.NotImplementedException();
        }

        public void Delete(object val)
        {
            throw new System.NotImplementedException();
        }

        public void Insert(object val)
        {
            throw new System.NotImplementedException();
        }
    }
}
