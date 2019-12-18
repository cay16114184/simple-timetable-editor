﻿using System;
using System.Runtime.Serialization;

namespace Timetabler.CoreData.Exceptions
{
    /// <summary>
    /// Exceptions that occur during file loading.
    /// </summary>
    [Serializable]
    public class TimetableLoaderException : Exception
    {
        /// <summary>
        /// Construct a TimetableLoaderException
        /// </summary>
        public TimetableLoaderException()
        {

        }

        /// <summary>
        /// Construct a TimetableLoaderException containing an error message.
        /// </summary>
        /// <param name="message">The error message</param>
        public TimetableLoaderException(string message) : base(message)
        {

        }

        /// <summary>
        /// Construct a TimetableLoaderException containing an error message and an inner exception.
        /// </summary>
        /// <param name="message">The error message.</param>
        /// <param name="innerException">The underlying exception.</param>
        public TimetableLoaderException(string message, Exception innerException) : base(message, innerException)
        {

        }

        /// <summary>
        /// Constructor that takes serialization infp
        /// </summary>
        /// <param name="info">The serialization info</param>
        /// <param name="context">The streaming context</param>
        protected TimetableLoaderException(SerializationInfo info, StreamingContext context) : base(info, context)
        {

        }
    }
}
