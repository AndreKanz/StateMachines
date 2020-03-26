using Contracts.Engine;
using Core.Logging;
using Core.Serialization;
using SchemalessStateMachineEngine;

namespace StateMachineApp.Schemaless
{
    public class SchemalessGameAiRunnerFactory
    {
        #region Methods

        public static ITransducer CreateRunner(string path)
        {
            return new DynamicRunnerFactory(
                new SerializerFactory())
                    .CreateRunner(
                        path, 
                        new NpcActionRunner(),
                        new TraceLogger());
        }

        #endregion
    }
}