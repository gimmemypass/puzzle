using System;
using Utils.Persistent.Base;

namespace Utils.Persistent
{
    public class PersistentDoubleDictionary : PersistentSpecialTypeDictionary<double>
    {
        public PersistentDoubleDictionary(string uniquePrefixKey) : base(uniquePrefixKey) { }
        
        protected override double ConvertFromString(string stringValue)
        {
            try
            {
                return Convert.ToDouble(stringValue);
            }
            catch (Exception)
            {
                // ignored
            }

            return 0;
        }
    }
}