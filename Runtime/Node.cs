using System;
using System.Collections;
using System.Collections.Generic;

namespace Oak
{
    public abstract class Node :  IEnumerable<Node>
    {
        public int Depth { get; private set; }
        public int Priority { get; private set; }

        protected OakContext context;

        protected bool InvertResult { get; private set; }

        public virtual void Init(OakContext context)
        {
            this.context = context;
        }

        public abstract void Reset();

        public IEnumerator<Node> GetEnumerator()
        {
            foreach (var node in Enumerate())
            {
                yield return node;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        protected abstract IEnumerable<Node> Enumerate();

        public enum Status
        {
            Running,
            Success,
            Failure,
        }
    }
}
