using System;
using System.Collections.Generic;
using Contracts.Engine;
using Contracts.Connectors;
using Contracts.Logging;

namespace TemplateStateMachineEngine.Engine
{
    public class StateMachineRunner<TState, TInput, TOutput> : ITransducer<TState, TInput, TOutput>
        where TState  : struct, IConvertible, IComparable, IFormattable
        where TInput  : struct, IConvertible, IComparable, IFormattable
        where TOutput : struct, IConvertible, IComparable, IFormattable
    {
        #region Fields

        private readonly IFiniteStateMachine<TState, TInput, TOutput> stateMachine;
        private readonly IActionRunner<TOutput> actions;
        private readonly ILogger logger;

        #endregion

        #region Properties

        public TState State => stateMachine.CurrentState;

        #endregion

        #region Constructors

        public StateMachineRunner(IFiniteStateMachine<TState, TInput, TOutput> stateMachine)
        {
            this.stateMachine = stateMachine;
        }

        public StateMachineRunner(IFiniteStateMachine<TState, TInput, TOutput> stateMachine, IActionRunner<TOutput> actions)
        {
            this.stateMachine = stateMachine;
            this.actions = actions;
        }

        public StateMachineRunner(IFiniteStateMachine<TState, TInput, TOutput> stateMachine, ILogger logger)
        {
            this.stateMachine = stateMachine;
            this.logger = logger;
        }

        public StateMachineRunner(IFiniteStateMachine<TState, TInput, TOutput> stateMachine, IActionRunner<TOutput> actions, ILogger logger)
        {
            this.stateMachine = stateMachine;
            this.actions = actions;
            this.logger = logger;
        }

        #endregion

        #region Methods

        public TOutput? ExecuteTransition(TInput input)
        {
            try
            {
                var output = stateMachine.ExecuteTransition(input);
                actions?.Execute(output);
                return output;
            }
            catch (InvalidOperationException ex)
            {
                logger?.Log(ex.Message);
                stateMachine.Reset();
                return null;
            }
        }

        #endregion
    }

    public class StateMachineRunner<TState, TInput, TOutput, TRaw> : IAcceptor<TState, TRaw>
        where TState  : struct, IConvertible, IComparable, IFormattable
        where TInput  : struct, IConvertible, IComparable, IFormattable
        where TOutput : struct, IConvertible, IComparable, IFormattable
    {
        #region Fields

        private readonly IFiniteStateMachine<TState, TInput, TOutput> stateMachine;
        private readonly IEventEncoder<TInput, TRaw> encoder;
        private readonly IActionRunner<TOutput> actions;
        private readonly ILogger logger;

        #endregion

        #region Properties

        public TState State => stateMachine.CurrentState;

        #endregion

        #region Constructors

        public StateMachineRunner(IFiniteStateMachine<TState, TInput, TOutput> stateMachine, IEventEncoder<TInput, TRaw> encoder)
        {
            this.stateMachine = stateMachine;
            this.encoder = encoder;
        }

        public StateMachineRunner(IFiniteStateMachine<TState, TInput, TOutput> stateMachine, IEventEncoder<TInput, TRaw> encoder, IActionRunner<TOutput> actions)
        {
            this.stateMachine = stateMachine;
            this.encoder = encoder;
            this.actions = actions;
        }

        public StateMachineRunner(IFiniteStateMachine<TState, TInput, TOutput> stateMachine, IEventEncoder<TInput, TRaw> encoder, ILogger logger)
        {
            this.stateMachine = stateMachine;
            this.encoder = encoder;
            this.logger = logger;
        }

        public StateMachineRunner(IFiniteStateMachine<TState, TInput, TOutput> stateMachine, IEventEncoder<TInput, TRaw> encoder, IActionRunner<TOutput> actions, ILogger logger)
        {
            this.stateMachine = stateMachine;
            this.encoder = encoder;
            this.actions = actions;
            this.logger = logger;
        }

        #endregion

        #region Methods

        public virtual void Run(IEnumerable<TRaw> input)
        {
            foreach (var item in input)
                ExecuteTransition(item);
        }

        private void ExecuteTransition(TRaw item)
        {
            try
            {
                var input = encoder.Encode(item);
                var output = stateMachine.ExecuteTransition(input);
                actions?.Execute(output);
            }
            catch(InvalidOperationException ex)
            {
                logger?.Log(ex.Message);
                stateMachine.Reset();
            }
        }

        #endregion
    }
}