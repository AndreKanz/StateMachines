using System;

namespace Contracts.Engine
{
    public interface ITransducer<TState, TInput, TOutput>
        where TState  : struct, IConvertible, IComparable, IFormattable
        where TInput  : struct, IConvertible, IComparable, IFormattable
        where TOutput : struct, IConvertible, IComparable, IFormattable
    {
        #region Properties

        TState State { get; }

        #endregion

        #region Methods

        TOutput? ExecuteTransition(TInput input);

        #endregion
    }

    public interface ITransducer
    {
        #region Properties

        string State { get; }

        #endregion

        #region Methods

        string ExecuteTransition(string input);

        #endregion
    }
}
