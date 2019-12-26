using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Newtonsoft.Json;
using Onset.Convertation.BuildIn;
using Onset.Entities;
using Onset.Helper;
using Onset.Runtime;

namespace Onset.Convertation
{
    /// <summary>
    /// This is the convert registry. With it you can request a convertation
    /// or register new converts.
    /// </summary>
    public class Converts
    {
        private static readonly List<IConvert> Converters = new List<IConvert>
        {
        };

        private static readonly IConvert EndConvert = new BasicConvert();

        /// <summary>
        /// Registers a new convert in the registry.
        /// </summary>
        /// <param name="convert">The convert to be registered</param>
        public static void Register(IConvert convert)
        {
            Converters.Add(convert);
        }

        /// <summary>
        /// Converts the given objects into it wanted types.
        /// </summary>
        /// <param name="objects">The given objects</param>
        /// <param name="wantedTypes">The wanted types</param>
        public static object[] Convert(string[] objects, Type[] wantedTypes)
        {
            object[] arr = new object[objects.Length];
            for (int i = 0; i < objects.Length; i++)
            {
                Type wantedType = wantedTypes[i];
                string obj = objects[i];
                IConvert convert = FindConvert(wantedType);
                if(convert != null)
                {
                    try
                    {
                        arr[i + 1] = convert.Convert(obj, wantedType);
                    }
                    catch (Exception e)
                    {
                        Wrapper.Server.Logger.Error("An error occurred while converting " + obj + " to " + wantedType.FullName + "!", e);
                    }
                }
            }

            return arr;
        }

        internal static object[] Convert(string[] objects, ParameterInfo[] wantedTypes, IPlayer invoker, bool withOptional = false)
        {
            try
            {
                object[] arr = new object[objects.Length + 1];
                arr[0] = invoker;
                int idx = 1;
                foreach (string obj in objects)
                {
                    ParameterInfo wantedType = wantedTypes[idx];
                    if (wantedType.IsOptional)
                    {
                        arr[idx] = Type.Missing;
                        continue;
                    }
                    IConvert convert = FindConvert(wantedType.ParameterType);
                    if (convert != null)
                    {
                        arr[idx] = convert.Convert(obj, wantedType.ParameterType);
                    }

                    idx++;
                }

                return arr;
            }
            catch (Exception e)
            {
                Wrapper.Server.Logger.Error("An error occurred in converting process!", e);
                return null;
            }
        }

        private static IConvert FindConvert(Type wantedType)
        {
            return Converters.SelectFirst(convert => convert.CanConvert(wantedType), EndConvert);
        }
    }
}
