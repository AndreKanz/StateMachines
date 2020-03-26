using System;
using System.Collections.Generic;

namespace TemplateStateMachineEngine.Data
{
    public class StateMachineConfiguration<TState, TInput, TOutput>
        where TState  : struct, IConvertible, IComparable, IFormattable
        where TInput  : struct, IConvertible, IComparable, IFormattable
        where TOutput : struct, IConvertible, IComparable, IFormattable
    {
        #region Properties

        public IList<StateTransition<TState, TInput,TOutput>> TransitionChart { get; set; }

        public TState StartState { get; set; }

        #endregion
    }
}