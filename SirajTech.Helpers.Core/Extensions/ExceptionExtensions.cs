using System;

namespace SirajTech.Helpers.Core
{
    public static class ExceptionExtensions
    {
        /// <summary>
        ///     Dig throw an exception to get its final error message from InnerExceptions if found.
        /// </summary>
        /// <param name="exception"> main Exception to dig in. </param>
        /// <returns> Error message as "String" </returns>
        public static string GetErrorMessage(this Exception exception)
        {
            while (true)
            {
                if (exception.InnerException == null) return exception.Message;
                exception = exception.InnerException;
            }
        }
    }
}
