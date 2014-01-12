using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MvvmPortable.Presentation.Input
{
    public class ActionCommand : Command
    {
        private readonly Action _action;

        public ActionCommand(Action action)
        {
            Requires.NotNull(action, "action");

            _action = action;
        }

        public override void Execute()
        {
            _action();
        }
    }
}
