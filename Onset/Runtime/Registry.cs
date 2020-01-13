using System;
using System.Collections.Generic;
using System.Reflection;
using Onset.Helper;

namespace Onset.Runtime
{
    internal class Registry<T> where T : Attribute
    {
        internal event Action<Item> ItemRegistered;
 
        private readonly List<Item> _items;

        internal Registry()
        {
            _items = new List<Item>();
        }

        internal List<Item> GetAll(Predicate<Item> check)
        {
            return _items.SelectAll(check);
        }

        internal object Execute(Predicate<Item> check, params object[] args)
        {
            Item item = GetItem(check);
            return item?.Invoke(args);
        }

        internal Item GetItem(Predicate<Item> check)
        {
            return _items.SelectFirst(check);
        }

        internal bool Exists(Predicate<Item> check)
        {
            return GetItem(check) != null;
        }

        internal void Register(object obj)
        {
            try
            {
                foreach (MethodInfo info in obj.GetType().GetMethods())
                {
                    T attribute = info.GetCustomAttribute<T>(false);
                    if (attribute != null)
                    {
                        Item item = new Item(attribute, obj, info);
                        _items.Add(item);
                        ItemRegistered?.Invoke(item);
                    }
                }
            }
            catch(Exception e)
            {
                Wrapper.Server.Logger.Error("Tried to register an Object from \"" + obj.GetType().FullName + "\" to the Registry for \"" + typeof(T).FullName + "\" but there was an exception!", e);
            }
        }

        internal void Register<TO>(Action<Item> callback = null)
        {
            try
            {
                Register(Activator.CreateInstance<TO>());
            }
            catch (Exception e)
            {
                Wrapper.Server.Logger.Error("Tried to register a Type from \"" + typeof(TO).FullName + "\" to the Registry for \"" + typeof(T).FullName + "\" but the Type has no default constructor or another exception occurred!", e);
            }
        }

        public class Item
        {
            internal T Data { get; }

            private object Handler { get; }

            internal MethodInfo Invoker { get; }

            internal Item(T data, object handler, MethodInfo invoker)
            {
                Data = data;
                Handler = handler;
                Invoker = invoker;
            }

            internal object Invoke(params object[] args) 
            {
                try
                {
                    if(args == null)
                    {
                        Wrapper.Server.Logger.Warn("Could not execute " + Invoker.Name + " for registry " + typeof(T).FullName + " because the arguments were null!");
                        return null;
                    }

                    if (Invoker.GetParameters().Length != args.Length)
                    {
                        Wrapper.Server.Logger.Fatal("Could not invoke " + Invoker.Name + " for registry " + typeof(T).FullName + " because the parameters are mismatching (got: " + args.Length + "; needed: " + Invoker.GetParameters().Length + ")!");
                        return null;
                    }

                    return Invoker.Invoke(Handler, args);
                }
                catch(Exception e)
                {
                    Wrapper.Server.Logger.Error("Could not execute registry item in Registry for \"" + typeof(T).FullName + "\" because of an Error!", e);
                    return null;
                }
            }
        }
    }
}
