using System.ComponentModel;
using System.Reflection;

namespace DishNetwork.Constants
{
    public class Enums
    {
        
        /// <summary>
        /// Request Status Code Enum
        /// </summary>
        public enum StatusCode
        {
            Ok = 200,
            BadRequest = 400,
            NotFound = 404, // also use for data not found
            ServerError = 500,
            AccessDenied = 403,
            NotAllowed = 405,
            Conflict = 409
        }

    }
}
