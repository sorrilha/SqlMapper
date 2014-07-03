using System;

namespace SqlMapper.Framework.CustomAttributes
{
    [AttributeUsage(AttributeTargets.Property)]
    class Pk : Attribute
    {
        private readonly String _pkName;

        public Pk(String pkName)
        {
            _pkName = pkName;
        }

        public string getPkName()
        {
            return _pkName;
        }

    }
}
