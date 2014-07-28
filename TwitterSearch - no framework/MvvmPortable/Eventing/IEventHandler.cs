using System;

namespace MvvmPortable.Eventing
{
    /// Handles events of type TEvent
    public interface IEventHandler<TEvent>
        where TEvent : IEvent
    {
        void Handle(TEvent e);
    }
}
