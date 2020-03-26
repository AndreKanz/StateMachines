using System.Collections.Generic;

namespace SchemalessStateMachineEngine.Data
{
    public class DynamicStateMachineConfiguration
    {
        #region Properties

        public IDictionary<string, int> States { get; set; }
        public IDictionary<string, int> Events { get; set; }
        public IDictionary<string, int> Actions { get; set; }
        public IList<DynamicStateTransition> TransitionTable { get; set; }
        public int StartState { get; set; }

        #endregion
    }
}