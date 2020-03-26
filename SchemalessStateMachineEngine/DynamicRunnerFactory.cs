using Contracts.Connectors;
using Contracts.Engine;
using Contracts.Logging;
using Contracts.Pattern;
using Contracts.Serialization;
using SchemalessStateMachineEngine.Compiler;
using SchemalessStateMachineEngine.Data;
using SchemalessStateMachineEngine.Engine;
using System.IO;

namespace SchemalessStateMachineEngine
{
    public class DynamicRunnerFactory
    {
        #region Fields

        private readonly IAbstractFactory<string, ISerializer> serializerFactory;

        #endregion

        #region Constructors

        public DynamicRunnerFactory(IAbstractFactory<string, ISerializer> serializerFactory)
        {
            this.serializerFactory = serializerFactory;
        }

        #endregion

        #region Methods

        public ITransducer CreateRunner(string path, IActionRunner actionRunner = null, ILogger logger = null)
        {
            if (string.IsNullOrEmpty(path))
                return null;

            var configuration = GetConfiguration(path);
            return configuration != null
                ?  new DynamicStateMachineRunner(GetConfiguration(path), actionRunner, logger)
                :  null;
        }

        private DynamicStateMachineConfiguration GetConfiguration(string path)
        {
            var extension = Path.GetExtension(path);
            if (extension == FileSettings.SourceExtension)
                return new DynamicStateMachineCompiler(null).CompileFile(path);

            var serializer = serializerFactory.CreateInstance(extension);
            if (serializer != null)
                return serializer.Deserialize<DynamicStateMachineConfiguration>(path);

            return null;
        }

        #endregion
    }
}