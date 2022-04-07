using System;
using UnityEngine;

namespace Utils.Persistent.Base
{
    public abstract class PersistentSpecialTypeDictionary<T> : PersistentDictionary<T>
    {
        protected PersistentSpecialTypeDictionary(string uniquePrefixKey) : base(uniquePrefixKey) { }

        protected override T GetPrefValue(string key, T defaultValue = default)
        {
            string stringValue = PlayerPrefs.GetString(key, String.Empty);
            if (string.IsNullOrEmpty(stringValue)) return defaultValue;
            return ConvertFromString(stringValue);
        }

        protected override void SetPrefValue(string key, T value)
        {
            PlayerPrefs.SetString(key, ConvertToString(value));
        }

        protected abstract T ConvertFromString(string stringValue);

        protected virtual string ConvertToString(T value)
        {
            return value.ToString();
        }
    }
}