using System;
using System.Runtime.InteropServices;

namespace TemplateStateMachineEngine.Data
{
    [StructLayout(LayoutKind.Sequential)]
    public struct StateTransition<TState, TInput, TOutput>
        where TState  : struct, IConvertible, IComparable, IFormattable
        where TInput  : struct, IConvertible, IComparable, IFormattable
        where TOutput : struct, IConvertible, IComparable, IFormattable
    {
        #region Fields

        public readonly TState  CurrentState;
        public readonly TInput  FSMEvent;
        public readonly TState  NextState;
        public readonly TOutput FSMAction;

        #endregion

        #region Constructors

        public StateTransition(TState currentState, TInput fsmEvent, TState nextState, TOutput fsmAction)
        {
            CurrentState = currentState;
            FSMEvent = fsmEvent;
            NextState = nextState;
            FSMAction = fsmAction;
        }

        #endregion
    }
}