using System;
using System.Collections.Generic;
using System.Text;

namespace DotNet.Testing.TestData
{
    public abstract class Builder<T>
    {
        private readonly List<Action<T>> _mutations = new();

        public Builder<T> With(Action<T> mutation)
        {
            _mutations.Add(mutation);
            return this;
        }

        public virtual T Build()
        {
            var instance = Create();
            foreach (var mutate in _mutations)
                mutate(instance);

            return instance;
        }

        public IEnumerable<T> BuildMany(int count)
        {
            if (count <= 0) throw new ArgumentOutOfRangeException(nameof(count));
            for (var i = 0; i < count; i++)
                yield return Build();
        }

        protected abstract T Create();
    }
}