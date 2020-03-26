using Contracts.Engine;

namespace Contracts.Compiler
{
    public interface IStateMachineCompiler
    {
        #region Methods

        void Compile(string sourcePath, string targetPath);
        IFiniteStateMachine Compile(string path);
        IFiniteStateMachine Load(string path);

        #endregion
    }
}