using System;

namespace SqlMapper.Framework.CustomAttributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field , AllowMultiple = true)]
    public class Pk : Attribute
    {
        private readonly String _pkName;
        private bool _identity;
        public Pk(String pkName, bool identity)
        {
            _pkName = pkName;
            _identity = identity;
        }

        public string getPkName()
        {
            return _pkName;
        }

        public bool isIdentity()
        {
            return _identity;
        }

    }
}
