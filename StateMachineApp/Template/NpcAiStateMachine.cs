using StateMachineApp.Template.Enums;
using TemplateStateMachineEngine.Data;
using TemplateStateMachineEngine.Engine;

namespace StateMachineApp.Template
{
    internal class NpcAiStateMachine : FiniteStateMachine<NpcState, GameEvent, NpcAction>
    {
        #region Properties

        protected override StateTransition<NpcState, GameEvent, NpcAction>[] TransitionTable { get; }
            = new []
            {
                CreateTransition(NpcState.Attack, GameEvent.BadChances,    NpcState.Flee,   NpcAction.Escape),
                CreateTransition(NpcState.Attack, GameEvent.Cleared,       NpcState.Wait,   NpcAction.Patrol),
                CreateTransition(NpcState.Attack, GameEvent.ResourceEmpty, NpcState.Reload, NpcAction.FillUpResource),
                CreateTransition(NpcState.Attack, GameEvent.EnemyInRange,  NpcState.Attack, NpcAction.Ignore),
                CreateTransition(NpcState.Attack, GameEvent.LowHealth,     NpcState.Heal,   NpcAction.HealLife),
                CreateTransition(NpcState.Attack, GameEvent.CriticalBlow,  NpcState.Dead,   NpcAction.Die),
                CreateTransition(NpcState.Attack, GameEvent.Respawn,       NpcState.Attack, NpcAction.Ignore),

                CreateTransition(NpcState.Flee,   GameEvent.BadChances,    NpcState.Flee,   NpcAction.Ignore),
                CreateTransition(NpcState.Flee,   GameEvent.Cleared,       NpcState.Wait,   NpcAction.Patrol),
                CreateTransition(NpcState.Flee,   GameEvent.ResourceEmpty, NpcState.Flee,   NpcAction.Ignore),
                CreateTransition(NpcState.Flee,   GameEvent.EnemyInRange,  NpcState.Flee,   NpcAction.Ignore),
                CreateTransition(NpcState.Flee,   GameEvent.LowHealth,     NpcState.Heal,   NpcAction.HealLife),
                CreateTransition(NpcState.Flee,   GameEvent.CriticalBlow,  NpcState.Dead,   NpcAction.Die),
                CreateTransition(NpcState.Flee,   GameEvent.Respawn,       NpcState.Flee,   NpcAction.Ignore),

                CreateTransition(NpcState.Heal,   GameEvent.BadChances,    NpcState.Flee,   NpcAction.Escape),
                CreateTransition(NpcState.Heal,   GameEvent.Cleared,       NpcState.Wait,   NpcAction.Patrol),
                CreateTransition(NpcState.Heal,   GameEvent.ResourceEmpty, NpcState.Heal,   NpcAction.Ignore),
                CreateTransition(NpcState.Heal,   GameEvent.EnemyInRange,  NpcState.Heal,   NpcAction.Ignore),
                CreateTransition(NpcState.Heal,   GameEvent.LowHealth,     NpcState.Heal,   NpcAction.Ignore),
                CreateTransition(NpcState.Heal,   GameEvent.CriticalBlow,  NpcState.Dead,   NpcAction.Die),
                CreateTransition(NpcState.Heal,   GameEvent.Respawn,       NpcState.Heal,   NpcAction.Ignore),

                CreateTransition(NpcState.Reload, GameEvent.BadChances,    NpcState.Flee,   NpcAction.Escape),
                CreateTransition(NpcState.Reload, GameEvent.Cleared,       NpcState.Wait,   NpcAction.Patrol),
                CreateTransition(NpcState.Reload, GameEvent.ResourceEmpty, NpcState.Reload, NpcAction.Ignore),
                CreateTransition(NpcState.Reload, GameEvent.EnemyInRange,  NpcState.Reload, NpcAction.Ignore),
                CreateTransition(NpcState.Reload, GameEvent.LowHealth,     NpcState.Heal,   NpcAction.HealLife),
                CreateTransition(NpcState.Reload, GameEvent.CriticalBlow,  NpcState.Dead,   NpcAction.Die),
                CreateTransition(NpcState.Reload, GameEvent.Respawn,       NpcState.Reload, NpcAction.Ignore),

                CreateTransition(NpcState.Wait,   GameEvent.BadChances,    NpcState.Flee,   NpcAction.Escape),
                CreateTransition(NpcState.Wait,   GameEvent.Cleared,       NpcState.Wait,   NpcAction.Ignore),
                CreateTransition(NpcState.Wait,   GameEvent.ResourceEmpty, NpcState.Reload, NpcAction.FillUpResource),
                CreateTransition(NpcState.Wait,   GameEvent.EnemyInRange,  NpcState.Attack, NpcAction.AttackEnenmy),
                CreateTransition(NpcState.Wait,   GameEvent.LowHealth,     NpcState.Heal,   NpcAction.HealLife),
                CreateTransition(NpcState.Wait,   GameEvent.CriticalBlow,  NpcState.Dead,   NpcAction.Die),
                CreateTransition(NpcState.Wait,   GameEvent.Respawn,       NpcState.Wait,   NpcAction.Ignore),

                CreateTransition(NpcState.Dead,   GameEvent.BadChances,    NpcState.Dead,   NpcAction.Ignore),
                CreateTransition(NpcState.Dead,   GameEvent.Cleared,       NpcState.Dead,   NpcAction.Ignore),
                CreateTransition(NpcState.Dead,   GameEvent.ResourceEmpty, NpcState.Dead,   NpcAction.Ignore),
                CreateTransition(NpcState.Dead,   GameEvent.EnemyInRange,  NpcState.Dead,   NpcAction.Ignore),
                CreateTransition(NpcState.Dead,   GameEvent.LowHealth,     NpcState.Dead,   NpcAction.Ignore),
                CreateTransition(NpcState.Dead,   GameEvent.CriticalBlow,  NpcState.Dead,   NpcAction.Ignore),
                CreateTransition(NpcState.Dead,   GameEvent.Respawn,       NpcState.Wait,   NpcAction.Patrol),
            };

        #endregion

        #region Constructors

        internal NpcAiStateMachine(NpcState startState) : base(startState) {   }

        #endregion
    }
}