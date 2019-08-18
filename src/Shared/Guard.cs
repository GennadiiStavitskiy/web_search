using System;
using System.Collections.Generic;
using System.Linq;

namespace Shared
{
    /// <summary>
    ///     Guard is static class which represents set of methods
    ///     to check input parameters by different type of conditions
    /// </summary>
    public static class Guard
    {
        /// <summary>
        ///     Check if integer parameter is more than zero.
        /// </summary>
        /// <param name="argumentIntValue">The argument int value.</param>
        /// <param name="argumentName">Name of the argument.</param>
        /// <exception cref="System.ArgumentOutOfRangeException">The argument must be greater than 0.</exception>
        public static void IntMoreThanZero(int argumentIntValue, string argumentName)
        {
            if (argumentIntValue <= 0)
                throw new ArgumentOutOfRangeException(argumentName, "The argument must be greater than 0.");
        }

        /// <summary>
        ///     Check if collection is not null or empty.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="argumentListValue">The argument list value.</param>
        /// <param name="argumentName">Name of the argument.</param>
        /// <exception cref="System.ArgumentNullException"></exception>
        /// <exception cref="System.ArgumentException">The argument cannot be empty.</exception>
        public static void ListNotNullOrEmpty<T>(IEnumerable<T> argumentListValue, string argumentName)
        {
            if (argumentListValue == null)
                throw new ArgumentNullException(argumentName);
            if (!argumentListValue.Any())
                throw new ArgumentException("The argument cannot be empty.", argumentName);
        }

        /// <summary>
        ///     Check if argument is not null.
        /// </summary>
        /// <param name="argumentValue">The argument value.</param>
        /// <param name="argumentName">Name of the argument.</param>
        /// <exception cref="System.ArgumentNullException"></exception>
        public static void NotNull(object argumentValue, string argumentName)
        {
            if (argumentValue == null)
                throw new ArgumentNullException(argumentName);
        }

        /// <summary>
        ///     Check if argument is not null or empty.
        /// </summary>
        /// <param name="argumentValue">The argument value.</param>
        /// <param name="argumentName">Name of the argument.</param>
        /// <exception cref="System.ArgumentNullException"></exception>
        /// <exception cref="System.ArgumentException">The argument cannot be empty.</exception>
        public static void NotNullOrEmpty(string argumentValue, string argumentName)
        {
            if (argumentValue == null)
                throw new ArgumentNullException(argumentName);
            if (argumentValue.Length == 0)
                throw new ArgumentException("The argument cannot be empty.", argumentName);
        }
    }
}
