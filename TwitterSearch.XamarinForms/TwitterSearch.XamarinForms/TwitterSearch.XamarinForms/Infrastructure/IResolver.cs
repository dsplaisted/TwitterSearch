using System;

namespace TwitterSearch.Infrastructure
{
    public interface IResolver
    {
        object Resolve(Type type);
    }
}
