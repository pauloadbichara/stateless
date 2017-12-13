using System;
using System.Collections.Generic;

using Stateless.Graph;
using Stateless.NameSpace1;
using Stateless.NameSpace6;
using Stateless.Reflection;

namespace Stateless.NameSpace3
{
    class DynamicTransition : Transition
    {
        /// <summary>
        /// The state where this transition finishes
        /// </summary>
        public State DestinationState { get; private set; }

        /// <summary>
        /// When is this transition followed
        /// </summary>
        public string Criterion { get; private set; }

        public DynamicTransition(State sourceState, State destinationState, TriggerInfo trigger, string criterion)
            : base(sourceState, trigger)
        {
            DestinationState = destinationState;
            Criterion = criterion;
        }
    }

    class StayTransition : Transition
    {
        public IEnumerable<InvocationInfo> Guards { get; private set; }

        public StayTransition(State sourceState, TriggerInfo trigger, IEnumerable<InvocationInfo> guards, bool executeEntryExitActions)
            : base(sourceState, trigger)
        {
            ExecuteEntryExitActions = executeEntryExitActions;
            Guards = guards;
        }
    }
}

namespace Stateless.NameSpace4
{
    class FixedTransition : Transition
    {
        /// <summary>
        /// The state where this transition finishes
        /// </summary>
        public State DestinationState { get; private set; }

        /// <summary>
        /// Guard functions for this transition (null if none)
        /// </summary>
        public IEnumerable<InvocationInfo> Guards { get; private set; }

        public FixedTransition(State sourceState, State destinationState, TriggerInfo trigger, IEnumerable<InvocationInfo> guards)
            : base(sourceState, trigger)
        {
            DestinationState = destinationState;
            Guards = guards;
        }
    }
}