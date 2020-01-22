using System;

namespace Onset.Converter
{
    /// <summary>
    /// Converts are meant to convert objects into a specific type. This mostly defined
    /// by a parameter.
    /// Converts are used in the Command section as well as the Remote Event section to convert
    /// the given object array into its specified typed array so it can be used by the
    /// registry to trigger the specified trigger.
    /// </summary>
    public interface IConvert
    {
        /// <summary>
        /// Checks whether this convert can convert the given object in to the wanted type.
        /// </summary>
        /// <param name="wantedType">The wanted type</param>
        /// <returns>True if the convert can do the convert</returns>
        bool CanConvert(Type wantedType);

        /// <summary>
        /// Converts the given object into the wanted type.
        /// </summary>
        /// <param name="givenObject">The given object</param>
        /// <param name="wantedType">The wanted type</param>
        /// <returns>The converted object</returns>
        object Convert(string givenObject, Type wantedType);
    }
}
