namespace Onset.Runtime.Garbage
{
    internal interface IGarbageHandler<in T>
    {
        void Handle(T obj);
    }
}
