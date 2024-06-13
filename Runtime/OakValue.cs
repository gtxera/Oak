using System;

namespace Oak
{
    public class OakValue
    {
        private object _value;

        private bool _isSet;

        internal OakValue()
        {
            _value = null;
        }

        internal OakValue(object value)
        {
            _value = value;
        }

        public bool IsSet()
        {
            return _isSet;
        }

        public void Set(object value)
        {
            _isSet = true;
            _value = value;
        }

        public void Unset()
        {
            _isSet = false;
        }

        public bool IsOfType<T>()
        {
            return _value.GetType() is T;
        }

        public T Get<T>()
        {
            if (!IsOfType<T>())
            {
                throw new MismatchedValueTypeException($"Value has a type {_value.GetType()}, tried to access it as {typeof(T)}");
            }
            else if (!IsSet())
            {
                throw new UnsetValueException("Value is unset");
            }

            return (T)_value;
        }

        public bool TryGet<T>(out T value)
        {
            if (!IsOfType<T>() || !IsSet())
            {
                value = default(T);
                return false;
            }

            value = (T)_value;
            return true;
        }

        [Serializable]
        public class MismatchedValueTypeException : Exception
        {
            public MismatchedValueTypeException(string messsage) : base(messsage)
            {
            }
        }

        [Serializable]
        public class UnsetValueException : Exception
        {
            public UnsetValueException(string messsage) : base(messsage)
            {
            }
        }
    }
}
