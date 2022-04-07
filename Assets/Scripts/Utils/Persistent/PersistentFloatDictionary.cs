using System;
using Utils.Persistent.Base;

namespace Utils.Persistent
{
    public class PersistentFloatDictionary : PersistentSpecialTypeDictionary<float>
    {
        public PersistentFloatDictionary(string uniquePrefixKey) : base(uniquePrefixKey) { }

        protected override float ConvertFromString(string stringValue)
        {
            try
            {
                return Convert.ToSingle(stringValue);
            }
            catch (Exception)
            {
                // ignored
            }

            return 0;
        }
    }
}