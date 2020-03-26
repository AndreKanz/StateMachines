using Contracts.Engine;
using System;
using TemplateStateMachineEngine.Data;

namespace TemplateStateMachineEngine.Engine
{
    public abstract class FiniteStateMachine<TState, TInput, TOutput> : IFiniteStateMachine<TState, TInput, TOutput>
        where TState  : struct, IConvertible, IComparable, IFormattable
        where TInput  : struct, IConvertible, IComparable, IFormattable
        where TOutput : struct, IConvertible, IComparable, IFormattable
    {
        #region Fields

        private readonly TState backupState;

        #endregion

        #region Properties

        protected abstract StateTransition<TState, TInput, TOutput>[] TransitionTable { get; }
        public TState CurrentState { get; private set; }

        #endregion

        #region Constructors
        
        protected FiniteStateMachine(TState startState)
        {
            CurrentState = startState;
            backupState = startState;
        }

        #endregion

        #region Methods

        public void Reset()
        {
            CurrentState = backupState;
        }

        protected static StateTransition<TState, TInput, TOutput> CreateTransition(TState currentState, TInput fsmEvent, TState nextState, TOutput fsmAction)
        {
            return new StateTransition<TState, TInput, TOutput>(currentState, fsmEvent, nextState, fsmAction);
        }

        public TOutput ExecuteTransition(TInput fsmEvent)
        {
            var transition = GetTransition(fsmEvent);
            CurrentState = transition.NextState;
            return transition.FSMAction;
        }

        private StateTransition<TState, TInput, TOutput> GetTransition(TInput fsmEvent)
        {
            for (var i = 0; i < TransitionTable.Length; ++i)
                if (TransitionTable[i].CurrentState.Equals(CurrentState) && TransitionTable[i].FSMEvent.Equals(fsmEvent))
                    return TransitionTable[i];

            throw new InvalidOperationException("No matching transition found.");
        }

        #endregion
    }
}