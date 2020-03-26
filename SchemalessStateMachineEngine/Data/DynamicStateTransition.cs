using System.Runtime.InteropServices;

namespace SchemalessStateMachineEngine.Data
{
    [StructLayout(LayoutKind.Sequential)]
    public struct DynamicStateTransition
    {
        #region Fields

        public readonly int CurrentState;
        public readonly int FSMEvent;
        public readonly int NextState;
        public readonly int FSMAction;

        #endregion

        #region Constructors

        public DynamicStateTransition(int currentState, int fsmEvent, int nextState, int fsmAction)
        {
            CurrentState = currentState;
            FSMEvent = fsmEvent;
            NextState = nextState;
            FSMAction = fsmAction;
        }

        #endregion
    }
}