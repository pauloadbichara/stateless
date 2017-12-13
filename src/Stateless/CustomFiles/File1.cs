using System;
using System.Collections.Generic;
using System.Linq;
using Stateless.Reflection;

namespace Stateless.NameSpace1
{
    /// <summary>
    /// Describes a transition that can be initiated from a trigger, but whose result is non-deterministic.
    /// </summary>
    public class DynamicTransitionInfo : TransitionInfo
    {
        internal InvocationInfo DestinationStateSelectorDescription { get; private set; }
        internal DynamicStateInfos PossibleDestinationStates { get; private set; }

        internal class Creator
        {
            internal static DynamicTransitionInfo Create<TTrigger>(TTrigger trigger, IEnumerable<InvocationInfo> guards,
    InvocationInfo selector, DynamicStateInfos possibleStates)
            {
                var transition = new DynamicTransitionInfo
                {
                    Trigger = new TriggerInfo(trigger),
                    GuardConditionsMethodDescriptions = guards ?? new List<InvocationInfo>(),
                    DestinationStateSelectorDescription = selector,
                    PossibleDestinationStates = possibleStates // behaviour.PossibleDestinationStates?.Select(x => x.ToString()).ToArray()
                };

                return transition;
            }
        }
        internal static DynamicTransitionInfo Create<TTrigger>(TTrigger trigger, IEnumerable<InvocationInfo> guards,
            InvocationInfo selector, DynamicStateInfos possibleStates)
        {
            return Creator.Create(trigger, guards, selector, possibleStates);
        }

        private DynamicTransitionInfo() { }
    }
}
