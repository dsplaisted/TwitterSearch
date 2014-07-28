using System;

namespace MvvmPortable.Composition
{
    public interface IResolver
    {
        object Resolve(Type type);
    }
}
