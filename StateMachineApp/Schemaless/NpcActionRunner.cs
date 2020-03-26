using Contracts.Connectors;
using Core;
using System;
using System.Collections.Generic;

namespace StateMachineApp.Schemaless
{
    internal class NpcActionRunner : IActionRunner
    {
        #region Properties

        private static readonly IDictionary<string, Action> Commands =
            new Dictionary<string, Action>()
            {
                //register commands
                { "AttackEnenmy",   () => {   } },
                { "FillUpResource", () => {   } },
                { "Escape",         () => {   } },
                { "HealLife",       () => {   } },
                { "Patrol",         () => {   } },
                { "Ignore",         () => {   } },
                { "Die",            () => {   } }
            };

        #endregion

        #region Methods

        public void Execute(string action)
        {
            Commands.Execute(action);
        }

        #endregion
    }
}
