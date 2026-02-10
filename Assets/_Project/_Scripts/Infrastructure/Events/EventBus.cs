using System;
using System.Collections.Generic;

namespace ACT.Scripts
{
    public sealed class EventBus : IEventBus
    {
        private readonly Dictionary<Type, Delegate> _handlers = new();

        public void Subscribe<T>(Action<T> handler)
        {
            var type = typeof(T);
            _handlers[type] = _handlers.TryGetValue(type, out var del)
                ? Delegate.Combine(del, handler)
                : handler;
        }

        public void Unsubscribe<T>(Action<T> handler)
        {
            var type = typeof(T);
            if (!_handlers.TryGetValue(type, out var del)) 
                return;

            var current = Delegate.Remove(del, handler);
            if (current == null) 
                _handlers.Remove(type);
            else 
                _handlers[type] = current;
        }

        public void Publish<T>(T evt)
        {
            if (_handlers.TryGetValue(typeof(T), out var del))
                ((Action<T>)del)?.Invoke(evt);
        }
    }
}
