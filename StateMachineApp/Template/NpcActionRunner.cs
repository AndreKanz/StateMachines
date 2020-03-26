using System;
using System.Collections.Generic;
using Contracts.Connectors;
using Core;
using StateMachineApp.Template.Enums;

namespace StateMachineApp.Template
{
    internal class NpcActionRunner : IActionRunner<NpcAction>
    {
        #region Properties

        private static readonly IDictionary<NpcAction, Action> Commands =
            new Dictionary<NpcAction, Action>()
            {
                //register commands
                { NpcAction.AttackEnenmy,   () => {   } },
                { NpcAction.FillUpResource, () => {   } },
                { NpcAction.Escape,         () => {   } },
                { NpcAction.HealLife,       () => {   } },
                { NpcAction.Patrol,         () => {   } },
                { NpcAction.Ignore,         () => {   } },
                { NpcAction.Die,            () => {   } }
            };

        #endregion

        #region Methods

        public void Execute(NpcAction action)
        {
            Commands.Execute(action);
        }

        #endregion
    }
}