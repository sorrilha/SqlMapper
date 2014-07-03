using System;

namespace SqlMapper.Framework.CustomAttributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field , AllowMultiple = true)]
    public class Pk : Attribute
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
