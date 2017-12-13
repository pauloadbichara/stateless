using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stateless
{
    public partial class StateMachine<TState, TTrigger>
    {
        internal abstract class DeactivateActionBehaviour
        {
            readonly TState _state;
            readonly NameSpace1.InvocationInfo _actionDescription;

            protected DeactivateActionBehaviour(TState state, NameSpace1.InvocationInfo actionDescription)
            {
                _state = state;
                _actionDescription = actionDescription ?? throw new ArgumentNullException(nameof(actionDescription));
            }

            internal NameSpace1.InvocationInfo Description => _actionDescription;

            public abstract void Execute();
            public abstract Task ExecuteAsync();

            public class Sync : DeactivateActionBehaviour
            {
                readonly Action _action;

                public Sync(TState state, Action action, NameSpace1.InvocationInfo actionDescription)
                    : base(state, actionDescription)
                {
                    _action = action;
                }

                public override void Execute()
                {
                    _action();
                }

                public override Task ExecuteAsync()
                {
                    Execute();
                    return TaskResult.Done;
                }
            }

            public class Async : DeactivateActionBehaviour
            {
                readonly Func<Task> _action;

                public Async(TState state, Func<Task> action, NameSpace1.InvocationInfo actionDescription)
                    : base(state, actionDescription)
                {
                    _action = action;
                }

                public override void Execute()
                {
                    throw new InvalidOperationException(
                        $"Cannot execute asynchronous action specified in OnDeactivateAsync for '{_state}' state. " +
                         "Use asynchronous version of Deactivate [DeactivateAsync]");
                }

                public override Task ExecuteAsync()
                {
                    return _action();
                }
            }
        }
    }
}
