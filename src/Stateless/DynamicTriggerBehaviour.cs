using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Stateless
{
    public partial class StateMachine<TState, TTrigger>
    {
        internal class DynamicTriggerBehaviour : TriggerBehaviour
        {
            readonly Func<object[], TState> _destination;
            internal NameSpace1.DynamicTransitionInfo TransitionInfo { get; private set; }

            public DynamicTriggerBehaviour(TTrigger trigger, Func<object[], TState> destination, 
                TransitionGuard transitionGuard, NameSpace1.DynamicTransitionInfo info)
                : base(trigger, transitionGuard)
            {
                _destination = destination ?? throw new ArgumentNullException(nameof(destination));
                TransitionInfo = info ?? throw new ArgumentNullException(nameof(info));
            }

            public override bool ResultsInTransitionFrom(TState source, object[] args, out TState destination)
            {
                destination = _destination(args);
                return true;
            }
        }
    }
}
