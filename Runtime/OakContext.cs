using System;
using System.Collections.Generic;
using UnityEngine;

namespace Oak
{
    public class OakContext
    {
        private Dictionary<string, OakValue> _values = new();
        
        public bool ContainsKey(string key)
        {
            return _values.ContainsKey(key);
        }

        public void OnDecoratorStateChange(Decorator decorator, bool shouldExecute)
        {

        }

        public OakValue this[string key]
        {
            get
            {
                if (!ContainsKey(key))
                {
                    var newValue = new OakValue();
                    _values[key] = newValue;
                }

                return _values[key];
            }

            set
            {
                if (!ContainsKey(key))
                {
                    var newValue = new OakValue();
                    _values[key] = newValue;
                }

                if (value == null)
                {
                    _values[key].Unset();
                }
                else
                {
                    _values[key].Set(value);
                }
            }
        }
    }
}


