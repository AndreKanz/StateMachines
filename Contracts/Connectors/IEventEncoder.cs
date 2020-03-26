using System;

namespace Contracts.Connectors
{
    public interface IEventEncoder<TInput, TRaw>
        where TInput : struct, IConvertible, IComparable, IFormattable
    {
        #region Methods

        TInput Encode(TRaw input);

        #endregion
    }

    public interface IEventEncoder<TRaw>
    {
        #region Methods

        string Encode(TRaw input);

        #endregion
    }
}