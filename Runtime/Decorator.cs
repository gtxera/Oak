using System;
using System.Collections.Generic;

namespace Oak
{
    public abstract class Decorator : AuxiliaryNode
    {
        public event EventHandler DecoratorStateChanged;

        protected List<string> observedProperties = new();

        public AbortMode Mode { get; private set; }

        public bool ShouldExecute
        {
            get
            {
                if (!_isCached)
                {
                    _cachedConditionResult = GetShouldExecute();
                    _isCached = true;
                }
                
                return _cachedConditionResult;
            }
        }

        private bool _cachedConditionResult;
        private bool _isCached;

        public override void Init(OakContext context)
        {
            base.Init(context);

            foreach (var property in observedProperties)
            {
                context[property].ValueChanged += (sender, status) =>
                {
                    DecoratorStateChanged?.Invoke(this, new EventArgs());
                };
            }
        }

        public abstract bool GetShouldExecute();

        public enum AbortMode
        {
            None,
            Self,
            LowerPriority,
            Both,
        }
    }
}
