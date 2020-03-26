using System;

namespace Contracts.Connectors
{
    public interface IActionRunner<TOutput> 
        where TOutput : struct, IConvertible, IComparable, IFormattable
    {
        #region Methods

        void Execute(TOutput action);

        #endregion
    }

    public interface IActionRunner
    {
        #region Methods

        void Execute(string action);

        #endregion
    }
}