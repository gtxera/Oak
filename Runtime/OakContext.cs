using System;
using System.Collections.Generic;
using UnityEngine;

namespace Oak
{
    public class OakContext
    {
        public event Action<Node.Status> BehaviourTreeFinished;

        private GameObject _ownerObject;

        private bool _isPaused;

        private Task _currentTask;
        private Composite _treeRoot;

        private Dictionary<string, OakValue> _values = new();

        public OakContext(GameObject ownerObject, Composite treeRoot, bool startImmediate)
        {
            _ownerObject = ownerObject;

            _treeRoot = treeRoot;
            _treeRoot.NodeFinished += (status) => BehaviourTreeFinished?.Invoke(status);

            _isPaused = true;

            if (startImmediate)
            {
                Start();
            }
        }

        public void Execute()
        {
            if (!ShouldExecute())
            {
                return;
            }

            _currentTask.Update();
        }

        public bool OwnerObjectExists()
        {
            return _ownerObject != null;
        }

        public void Start()
        {
            _treeRoot.Init(this);

            _currentTask = _treeRoot.Start();

            _isPaused = false;
        }

        public void Stop()
        {
            _treeRoot.Reset();

            _isPaused = true;
        }

        public void Pause()
        {
            _isPaused = true;
        }

        public void Unpause()
        {
            _isPaused = false;
        }

        public void SetCurrentTask(Task task)
        {
            _currentTask = task;
        }

        public bool ContainsKey(string key)
        {
            return _values.ContainsKey(key);
        }

        private bool ShouldExecute()
        {
            return !_isPaused && _ownerObject.activeInHierarchy;
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
                _values[key] = value;
            }
        }
    }
}


