using Contracts.Engine;
using SchemalessStateMachineEngine.Data;
using System;
using System.Linq;

namespace SchemalessStateMachineEngine.Engine
{
    public class DynamicFiniteStateMachine : IFiniteStateMachine
    {
        #region Fields

        private readonly int backupState;

        #endregion

        #region Properties

        private DynamicStateTransition[] TransitionTable { get; }
        public int CurrentState { get; private set; }

        #endregion

        #region Constructors

        public DynamicFiniteStateMachine(DynamicStateMachineConfiguration configuration)
        {
            TransitionTable = configuration.TransitionTable.ToArray();
            CurrentState = configuration.StartState;
            backupState = configuration.StartState;
        }

        public DynamicFiniteStateMachine(DynamicStateTransition[] transitionTable, int startState)
        {
            TransitionTable = transitionTable;
            CurrentState = startState;
            backupState = startState;
        }

        #endregion

        #region Methods

        public void Reset()
        {
            CurrentState = backupState;
        }

        public int ExecuteTransition(int fsmEvent)
        {
            var transition = GetTransition(fsmEvent);
            CurrentState = transition.NextState;
            return transition.FSMAction;
        }

        private DynamicStateTransition GetTransition(int fsmEvent)
        {
            for (var i = 0; i < TransitionTable.Length; ++i)
                if (TransitionTable[i].CurrentState == CurrentState && TransitionTable[i].FSMEvent == fsmEvent)
                    return TransitionTable[i];

            throw new InvalidOperationException("No matching transition found.");
        }

        #endregion
    }
}