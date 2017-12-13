﻿using System;

namespace Stateless
{
    public partial class StateMachine<TState, TTrigger>
    {
        internal class GuardCondition
        {
            NameSpace1.InvocationInfo _methodDescription;

            internal GuardCondition(Func<bool> guard, NameSpace1.InvocationInfo description)
            {
                Guard = guard ?? throw new ArgumentNullException(nameof(guard));
                _methodDescription = description ?? throw new ArgumentNullException(nameof(description));
            }
            internal Func<bool> Guard { get; }

            // Return the description of the guard method: the caller-defined description if one
            // was provided, else the name of the method itself
            internal string Description => _methodDescription.Description;

            // Return a more complete description of the guard method
            internal NameSpace1.InvocationInfo MethodDescription => _methodDescription;
        }
    }
}