using System;
using System.Threading.Tasks;

namespace Stateless
{
    public partial class StateMachine<TState, TTrigger>
    {
        internal abstract class ActivateActionBehaviour
        {
            readonly TState _state;
            readonly NameSpace1.InvocationInfo _actionDescription;

            protected ActivateActionBehaviour(TState state, NameSpace1.InvocationInfo actionDescription)
            {
                _state = state;
                _actionDescription = actionDescription ?? throw new ArgumentNullException(nameof(actionDescription));
            }

            internal NameSpace1.InvocationInfo Description => _actionDescription;

            public abstract void Execute();
            public abstract Task ExecuteAsync();

            public class Sync : ActivateActionBehaviour
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

            public class Async : ActivateActionBehaviour
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
                        $"Cannot execute asynchronous action specified in OnActivateAsync for '{_state}' state. " +
                         "Use asynchronous version of Activate [ActivateAsync]");
                }

                public override Task ExecuteAsync()
                {
                    return _action();
                }
            }
        }
    }
}
