using System;
using System.Collections.Generic;

namespace Oak
{
    public abstract class Decorator : Node
    {
        protected List<string> observedProperties = new();

        public override void Init(OakContext context)
        {
            base.Init(context);


        }
    }
}
