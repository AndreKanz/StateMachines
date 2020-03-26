using Contracts.Engine;
using Contracts.Compiler;
using Contracts.Serialization;
using Core;
using System.Collections.Generic;
using System.IO;
using SchemalessStateMachineEngine.Data;
using SchemalessStateMachineEngine.Engine;
using Contracts.Pattern;

namespace SchemalessStateMachineEngine.Compiler
{
    public class DynamicStateMachineCompiler : IStateMachineCompiler
    {
        #region Fields

        private int stateCount;
        private int eventCount;
        private int actionCount;

        private readonly IAbstractFactory<string, ISerializer> serializerFactory;

        #endregion

        #region Constructors

        public DynamicStateMachineCompiler(IAbstractFactory<string, ISerializer> serializerFactory)
        {
            this.serializerFactory = serializerFactory;
        }

        #endregion

        #region Methods

        public void Compile(string sourcePath, string targetPath)
        {
            if (string.IsNullOrEmpty(targetPath))
                return;

            var configuration = CompileFile(sourcePath);
            if (configuration == null)
                return;

            serializerFactory
                .CreateInstance(Path.GetExtension(targetPath))?
                .Serialize(configuration, targetPath);

        }

        public IFiniteStateMachine Compile(string path)
        {
            var configuration = CompileFile(path);
            return configuration != null 
                ?  new DynamicFiniteStateMachine(configuration)
                :  null;
        }

        public IFiniteStateMachine Load(string path)
        {
            if (string.IsNullOrEmpty(path))
                return null;

            var configuration = serializerFactory
                .CreateInstance(Path.GetExtension(path))
                .Deserialize<DynamicStateMachineConfiguration>(path);

            return configuration != null
                ?  new DynamicFiniteStateMachine(configuration)
                :  null;
        }

        internal DynamicStateMachineConfiguration CompileFile(string path)
        {
            if (IsSourceInvalid(path))
                return null;

            var configuration = Initialize();

            File
                .ReadAllLines(path)
                .ForEach(line => ParseLine(line, configuration));

            return configuration;
        }

        private static bool IsSourceInvalid(string sourcePath)
        {
            return string.IsNullOrEmpty(sourcePath) 
                || Path.GetExtension(sourcePath) != FileSettings.SourceExtension 
                || !File.Exists(sourcePath);
        }

        private DynamicStateMachineConfiguration Initialize()
        {
            stateCount  = -1;
            eventCount  = -1;
            actionCount = -1;

            return new DynamicStateMachineConfiguration
            {
                TransitionTable = new List<DynamicStateTransition>(),
                States = new Dictionary<string, int>(),
                Events = new Dictionary<string, int>(),
                Actions = new Dictionary<string, int>()
            };
        }

        private void ParseLine(string line, DynamicStateMachineConfiguration configuration)
        {
            if (IsSkipLine(line))
                return;

            if (line.Contains(FileSettings.Delimiter))
                configuration
                    .TransitionTable
                    .Add(CreateTransition(line, configuration));
            else
                configuration.StartState = EncodeState(line, configuration);
        }

        private static bool IsSkipLine(string line)
        {
            return string.IsNullOrWhiteSpace(line)
                || (line.StartsWith(FileSettings.OpenGroup) 
                && line.EndsWith(FileSettings.CloseGroup));
        }

        private DynamicStateTransition CreateTransition(string line, DynamicStateMachineConfiguration configuration)
        {
            var splitted = line.Split(FileSettings.Delimiter);
            return new DynamicStateTransition(
                EncodeState(splitted[0], configuration),
                EncodeEvent(splitted[1], configuration),
                EncodeState(splitted[2], configuration),
                EncodeAction(splitted[3], configuration));
        }

        private int EncodeState(string value, DynamicStateMachineConfiguration configuration)
        {
            return Encode(value, configuration.States, ref stateCount);
        }

        private int EncodeEvent(string value, DynamicStateMachineConfiguration configuration)
        {
            return Encode(value, configuration.Events, ref eventCount);
        }

        private int EncodeAction(string value, DynamicStateMachineConfiguration configuration)
        {
            return Encode(value, configuration.Actions, ref actionCount);
        }

        private static int Encode(string value, IDictionary<string, int> map, ref int count)
        {
            if (map.ContainsKey(value))
                return map[value];
            else
            {
                map.Add(value, ++count);
                return count;
            }
        }

        #endregion
    }
}