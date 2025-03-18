using Microsoft.AspNetCore.Http;
using System;

namespace Fahrenheit451API.SessionExtensions {
    public static class SessionExtensions
    {
        // Get a boolean value from the session, returns null if not found
        public static bool? GetBoolean(this ISession session, string key)
        {
            var data = session.Get(key);
            if (data == null)
            {
                return null;  // Return null if there's no data
            }
            return BitConverter.ToBoolean(data, 0);  // Convert byte array back to boolean
        }

        // Set a boolean value in the session
        public static void SetBoolean(this ISession session, string key, bool value)
        {
            session.Set(key, BitConverter.GetBytes(value));  // Store the boolean as byte array
        }
    }
}
