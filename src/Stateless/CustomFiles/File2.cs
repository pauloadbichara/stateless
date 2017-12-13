using System;
using System.Collections.Generic;

using Stateless.NameSpace6;
using Stateless.Reflection;

using Stateless.Graph;

namespace Stateless.NameSpace2
{
    /// <summary>
    /// Used to keep track of the decision point of a dynamic transition
    /// </summary>
    class Decision : State
    {
        public InvocationInfo Method { get; private set; }

        internal Decision(InvocationInfo method, int num)
            : base("Decision" + num.ToString())
        {
            Method = method;
        }
    }

    /// <summary>
    /// Class to generate a DOT grah in UML format
    /// </summary>
    public static class UmlDotGraph
    {
        /// <summary>
        /// Generate a UML DOT graph from the state machine info
        /// </summary>
        /// <param name="machineInfo"></param>
        /// <returns></returns>
        public static string Format(StateMachineInfo machineInfo)
        {
            var graph = new Graph.StateGraph(machineInfo);

            return graph.ToGraph(new UmlDotGraphStyle());
        }

    }
}
