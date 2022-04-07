using System;
using UnityEngine;
using Utils.Persistent.Base;

namespace Utils.Persistent
{
    public class PersistentStringDictionary : PersistentDictionary<string>
    {
        public PersistentStringDictionary(string uniquePrefixKey) : base(uniquePrefixKey) { }

        protected override string GetPrefValue(string key, string defaultValue = default)
        {
            return PlayerPrefs.GetString(key, defaultValue);
        }

        protected override void SetPrefValue(string key, string value)
        {
            PlayerPrefs.SetString(key,value);
        }
    }
}