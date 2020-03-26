namespace Contracts.Pattern
{
    public interface IAbstractFactory<TKey, out TValue>
    {
        #region Properties

        TKey[] Keys { get; }

        #endregion

        #region Methods

        TValue CreateInstance(TKey key);

        #endregion
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1005:AvoidExcessiveParametersOnGenericTypes")]
    public interface IAbstractFactory<TKey, in TParameter, out TValue>
    {
        #region Properties

        TKey[] Keys { get; }

        #endregion

        #region Methods

        TValue CreateInstance(TKey key, TParameter parameter);

        #endregion
    }
}