using Core.Logging;
using Contracts.Engine;
using StateMachineApp.Template.Enums;
using TemplateStateMachineEngine.Engine;

namespace StateMachineApp.Template
{
    public static class TemplateGameAiRunnerFactory
    {
        #region Methods

        public static ITransducer<NpcState, GameEvent, NpcAction> CreateRunner()
        {
            return new StateMachineRunner<NpcState, GameEvent, NpcAction>(
                new NpcAiStateMachine(NpcState.Wait),
                new NpcActionRunner(),
                new TraceLogger());
        }

        #endregion
    }
}