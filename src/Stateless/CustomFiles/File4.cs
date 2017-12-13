using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


// We are keeping the original namespace instead of using something like NameSpaceN
// because of the partial class
namespace Stateless
{
    public partial class StateMachine<TState, TTrigger>
    {
        /// <summary>
        /// Describes a state transition.
        /// </summary>
        public class Transition
        {
            readonly TState _source;
            readonly TState _destination;
            readonly TTrigger _trigger;

            /// <summary>
            /// Construct a transition.
            /// </summary>
            /// <param name="source">The state transitioned from.</param>
            /// <param name="destination">The state transitioned to.</param>
            /// <param name="trigger">The trigger that caused the transition.</param>
            public Transition(TState source, TState destination, TTrigger trigger)
            {
                _source = source;
                _destination = destination;
                _trigger = trigger;
            }

            /// <summary>
            /// The state transitioned from.
            /// </summary>
            public TState Source { get { return _source; } }

            /// <summary>
            /// The state transitioned to.
            /// </summary>
            public TState Destination { get { return _destination; } }

            /// <summary>
            /// The trigger that caused the transition.
            /// </summary>
            public TTrigger Trigger { get { return _trigger; } }

            /// <summary>
            /// True if the transition is a re-entry, i.e. the identity transition.
            /// </summary>
            public bool IsReentry { get { return Source.Equals(Destination); } }
        }
    }

    internal static class TaskResult
    {
        internal static readonly Task Done = FromResult(1);

        static Task<T> FromResult<T>(T value)
        {
            var tcs = new TaskCompletionSource<T>();
            tcs.SetResult(value);
            return tcs.Task;
        }
    }
}
