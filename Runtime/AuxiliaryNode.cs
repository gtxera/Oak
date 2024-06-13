using System.Collections.Generic;

namespace Oak
{
    public abstract class AuxiliaryNode : Node
    {
        protected override IEnumerable<Node> Enumerate()
        {
            yield return this;
        }
    }
}
