using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Stateless.Graph;
using Stateless.NameSpace7;
using Stateless.Reflection;

namespace Stateless
{
    public partial class StateMachine<TState, TTrigger>
    {
        internal class StateReference
        {
            public TState State { get; set; }
        }
    }
}

namespace Stateless.NameSpace6
{
    /// <summary>
    /// Generate DOT graphs in basic UML style
    /// </summary>
    public class UmlDotGraphStyle : IGraphStyle
    {
        /// <summary>Get the text that starts a new graph</summary>
        /// <returns></returns>
        override internal string GetPrefix()
        {
            return "digraph {\n"
                      + "compound=true;\n"
                      + "node [shape=Mrecord]\n"
                      + "rankdir=\"LR\"\n";
        }

        internal class StringFormatter
        {
            internal static string FormatOneLine(string fromNodeName, string toNodeName, string label)
            {
                return fromNodeName + " -> " + toNodeName + " " + "[style=\"solid\", label=\"" + label + "\"];";
            }
        }

         internal override string FormatOneCluster(SuperState stateInfo)
        {
            string stateRepresentationString = "";
            var sourceName = stateInfo.StateName;

            StringBuilder label = new StringBuilder(sourceName);

            if ((stateInfo.EntryActions.Count > 0) || (stateInfo.ExitActions.Count > 0))
            {
                label.Append("\\n----------");
                label.Append(string.Concat(stateInfo.EntryActions.Select(act => "\\nentry / " + act)));
                label.Append(string.Concat(stateInfo.ExitActions.Select(act => "\\nexit / " + act)));
            }

            stateRepresentationString = "\n"
                + $"subgraph cluster{stateInfo.NodeName}" + "\n"
                + "\t{" + "\n"
                + $"\tlabel = \"{label.ToString()}\"" + "\n";

            foreach (var subState in stateInfo.SubStates)
            {
                stateRepresentationString += FormatOneState(subState);
            }

            stateRepresentationString += "}\n";

            return stateRepresentationString;
        }

        /// <summary>
        /// Generate the text for a single state
        /// </summary>
        /// <param name="state">The state to generate text for</param>
        /// <returns></returns>
        override internal string FormatOneState(State state)
        {
            if ((state.EntryActions.Count == 0) && (state.ExitActions.Count == 0))
                return state.StateName + " [label=\"" + state.StateName + "\"];\n";

            string f = state.StateName + " [label=\"" + state.StateName + "|";

            List<string> es = new List<string>();
            es.AddRange(state.EntryActions.Select(act => "entry / " + act));
            es.AddRange(state.ExitActions.Select(act => "exit / " + act));

            f += String.Join("\\n", es);

            f += "\"];\n";

            return f;
        }

        /// <summary>
        /// Generate text for a single transition
        /// </summary>
        /// <param name="sourceNodeName"></param>
        /// <param name="trigger"></param>
        /// <param name="actions"></param>
        /// <param name="destinationNodeName"></param>
        /// <param name="guards"></param>
        /// <returns></returns>
        override internal string FormatOneTransition(string sourceNodeName, string trigger, IEnumerable<string> actions, string destinationNodeName, IEnumerable<string> guards)
        {
            string label = trigger ?? "";

            if (actions?.Count() > 0)
                label += " / " + string.Join(", ", actions);

            if (guards.Count() > 0)
            {
                foreach (var info in guards)
                {
                    if (label.Length > 0)
                        label += " ";
                    label += "[" + info + "]";
                }
            }

            return StringFormatter.FormatOneLine(sourceNodeName, destinationNodeName, label);
        }

        /// <summary>
        /// Generate the text for a single decision node
        /// </summary>
        /// <param name="nodeName">Name of the node</param>
        /// <param name="label">Label for the node</param>
        /// <returns></returns>
        override internal string FormatOneDecisionNode(string nodeName, string label)
        {
            return nodeName + " [shape = \"diamond\", label = \"" + label + "\"];\n";
        }
    }

    /// <summary>
    /// Used to keep track of transitions between states
    /// </summary>
    public class Transition
    {
        /// <summary>
        /// The trigger that causes this transition
        /// </summary>
        public TriggerInfo Trigger { get; private set; }

        /// <summary>
        /// List of actions to be performed by the destination state (the one being entered)
        /// </summary>
        public List<ActionInfo> DestinationEntryActions = new List<ActionInfo>();

        /// <summary>
        /// Should the entry and exit actions be executed when this transition takes place
        /// </summary>
        public bool ExecuteEntryExitActions { get; protected set; } = true;

        /// <summary>
        /// The state where this transition starts
        /// </summary>
        public State SourceState { get; private set; }

        /// <summary>
        /// Base class of transitions
        /// </summary>
        /// <param name="sourceState"></param>
        /// <param name="trigger"></param>
        public Transition(State sourceState, TriggerInfo trigger)
        {
            SourceState = sourceState;
            Trigger = trigger;
        }
    }
}
