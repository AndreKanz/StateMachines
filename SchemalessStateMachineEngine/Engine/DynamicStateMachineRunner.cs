using Contracts.Connectors;
using Contracts.Engine;
using Contracts.Logging;
using Core;
using SchemalessStateMachineEngine.Data;
using System;
using System.Collections.Generic;

namespace SchemalessStateMachineEngine.Engine
{
    public class DynamicStateMachineRunner : ITransducer
    {
        #region Fields

        private readonly IFiniteStateMachine stateMachine;
        private readonly IActionRunner actionRunner;
        private readonly ILogger logger;

        private readonly IDictionary<string, int> states;
        private readonly IDictionary<string, int> events;
        private readonly IDictionary<string, int> actions;

        #endregion

        #region Properties

        public string State => states.GetKey(stateMachine.CurrentState);

        #endregion

        #region Constructors

        public DynamicStateMachineRunner(DynamicStateMachineConfiguration configuration, IActionRunner actionRunner, ILogger logger)
        {
            stateMachine = new DynamicFiniteStateMachine(configuration);
            states = configuration.States;
            events = configuration.Events;
            actions = configuration.Actions;
            this.actionRunner = actionRunner;
            this.logger = logger;
        }

        #endregion

        #region Methods

        public string ExecuteTransition(string input)
        {
            try
            {
                var action = GetAction(input);
                actionRunner?.Execute(action);
                return action;
            }
            catch(InvalidOperationException ex)
            {
                logger?.Log(ex.Message);
                stateMachine.Reset();
                return string.Empty;
            }
        }

        private string GetAction(string input)
        {
            var output = stateMachine.ExecuteTransition(events[input]);
            return actions.GetKey(output);
        }

        #endregion
    }

    public class DynamicStateMachineRunner<TRaw> : IAcceptor<TRaw>
    {
        #region Fields

        private readonly IFiniteStateMachine stateMachine;
        private readonly IEventEncoder<TRaw> eventEncoder;
        private readonly IActionRunner actionRunner;
        private readonly ILogger logger;

        private readonly IDictionary<string, int> states;
        private readonly IDictionary<string, int> events;
        private readonly IDictionary<string, int> actions;

        #endregion

        #region Properties

        public string State => states.GetKey(stateMachine.CurrentState);

        #endregion

        #region Constructors

        public DynamicStateMachineRunner(DynamicStateMachineConfiguration configuration, IEventEncoder<TRaw> eventEncoder, IActionRunner actionRunner, ILogger logger)
        {
            stateMachine = new DynamicFiniteStateMachine(configuration);
            states = configuration.States;
            events = configuration.Events;
            actions = configuration.Actions;
            this.eventEncoder = eventEncoder;
            this.actionRunner = actionRunner;
            this.logger = logger;
        }

        #endregion

        #region Methods

        public void Run(IEnumerable<TRaw> input)
        {
            foreach (var item in input)
            {
                try
                {
                    var encoded = eventEncoder.Encode(item);
                    var output = stateMachine.ExecuteTransition(events[encoded]);
                    actionRunner?.Execute(actions.GetKey(output));
                }
                catch (InvalidOperationException ex)
                {
                    logger?.Log(ex.Message);
                    stateMachine.Reset();
                }
            }
        }

        #endregion
    }
}