using System;
using UnityEngine;
using Utils.Persistent.Base;

namespace Utils.Persistent
{
    public class PersistentBoolDictionary : PersistentDictionary<bool>
    {
        public PersistentBoolDictionary(string uniquePrefixKey) : base(uniquePrefixKey) { }

        protected override bool GetPrefValue(string key, bool defaultValue = default)
        {
            return Convert.ToBoolean(PlayerPrefs.GetInt(key, Convert.ToInt32(defaultValue)));
        }

        protected override void SetPrefValue(string key, bool value)
        {
            var v = Convert.ToInt32(value);
            PlayerPrefs.SetInt(key, v);
        }
    }
}
