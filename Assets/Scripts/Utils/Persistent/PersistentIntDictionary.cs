using UnityEngine;
using Utils.Persistent.Base;

namespace Utils.Persistent
{
    public class PersistentIntDictionary : PersistentDictionary<int>
    {
        public PersistentIntDictionary(string uniquePrefixKey) : base(uniquePrefixKey) { }

        protected override int GetPrefValue(string key, int defaultValue = default)
        {
            return PlayerPrefs.GetInt(key, defaultValue);
        }

        protected override void SetPrefValue(string key, int value)
        {
            PlayerPrefs.SetInt(key,value);
        }
    }
}