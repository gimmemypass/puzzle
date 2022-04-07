using System;
using Utils.Persistent.Base;

namespace Utils.Persistent
{
    public class PersistentLongDictionary : PersistentSpecialTypeDictionary<long>
    {
        public PersistentLongDictionary(string uniquePrefixKey) : base(uniquePrefixKey) { }
        
        protected override long ConvertFromString(string stringValue)
        {
            try
            {
                return Convert.ToInt64(stringValue);
            }
            catch (Exception)
            {
                // ignored
            }

            return 0;
        }
    }
}