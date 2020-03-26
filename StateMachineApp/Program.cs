using Contracts.Engine;
using Core.Serialization;
using SchemalessStateMachineEngine.Compiler;
using StateMachineApp.Schemaless;
using StateMachineApp.Template;
using StateMachineApp.Template.Enums;

namespace StateMachineApp
{
    class Program
    {
        static void Main(string[] args)
        {
            new DynamicStateMachineCompiler(new SerializerFactory()).Compile("cfg.csv", "compiled.bson");
            var tRunner = TemplateGameAiRunnerFactory.CreateRunner();
            var sRunner = SchemalessGameAiRunnerFactory.CreateRunner("cfg.csv");
            tRunner.ExecuteTransition(GameEvent.EnemyInRange);
            sRunner.ExecuteTransition("EnemyInRange");
            var same = IsSameState(tRunner, sRunner);
            tRunner.ExecuteTransition(GameEvent.LowHealth);
            sRunner.ExecuteTransition("LowHealth");
            same = IsSameState(tRunner, sRunner);
            tRunner.ExecuteTransition(GameEvent.CriticalBlow);
            sRunner.ExecuteTransition("CriticalBlow");
            same = IsSameState(tRunner, sRunner);
            tRunner.ExecuteTransition(GameEvent.EnemyInRange);
            sRunner.ExecuteTransition("EnemyInRange");
            same = IsSameState(tRunner, sRunner);
            tRunner.ExecuteTransition(GameEvent.Respawn);
            sRunner.ExecuteTransition("Respawn");
            same = IsSameState(tRunner, sRunner);
        }

        private static bool IsSameState(ITransducer<NpcState, GameEvent, NpcAction> tRunner, ITransducer sRunner)
        {
            return tRunner.State.ToString() == sRunner.State;
        }
    }
}
