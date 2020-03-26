using System;
using System.Collections.Generic;

namespace Contracts.Engine
{
    public interface IAcceptor<TState, TRaw>
        where TState : struct, IConvertible, IComparable, IFormattable
    {
        #region Properties

        TState State { get; }

        #endregion

        #region Methods

        void Run(IEnumerable<TRaw> input);

        #endregion
    }

    public interface IAcceptor<TRaw>
    {
        #region Properties

        string State { get; }

        #endregion

        #region Methods

        void Run(IEnumerable<TRaw> input);

        #endregion
    }
}