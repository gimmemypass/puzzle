using System;
using Services.Input.Data;

namespace Services.Input.EventsProviders
{
    public interface IInputEventsProvider
    {
        public void SetPointerDownEventHandler(Action<InputPointer> handler);
        public void SetPointerMoveEventHandler(Action<InputPointer> handler);
        public void SetPointerUpEventHandler(Action<InputPointer> handler);
    }
}