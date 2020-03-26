using System;

namespace Contracts.Engine
{
    public interface IFiniteStateMachine<TState, TInput, TOutput> : IResettable
        where TState  : struct, IConvertible, IComparable, IFormattable
        where TInput  : struct, IConvertible, IComparable, IFormattable
        where TOutput : struct, IConvertible, IComparable, IFormattable
    {
        #region Properties

        TState CurrentState { get; }

        #endregion

        #region Methods

        TOutput ExecuteTransition(TInput fsmEvent);

        #endregion
    }

    public interface IFiniteStateMachine : IResettable
    {
        #region Properties 

        int CurrentState { get; }

        #endregion

        #region Methods

        int ExecuteTransition(int input);

        #endregion
    }
}