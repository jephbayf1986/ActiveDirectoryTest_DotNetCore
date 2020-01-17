using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace testDotNetCoreAdApp.ErrorHandling
{
    public class ErrorHandler
    {
        public int StatusCode;

        public string Message;

        public ErrorHandler(Exception Exception, bool ShowFullDetails)
        {
            //Default:
            StatusCode = 500;
            Message = "An Unknown Error Occured: " + (ShowFullDetails ? Exception.Message : "");

            if (Exception.Message.Contains("was not found"))
            {
                StatusCode = 404;
                Message = "Error: No Identity of this Type matches the one provided";
            }

            if (Exception.Message == "Object reference not set to an instance of an object.")
            {
                StatusCode = 500;
                Message = "Error: A Null Exception was Raised";
            }

            if (Exception.Message.StartsWith("Database operation expected to affect") && Exception.Message.Contains("but actually affected 0"))
            {
                StatusCode = 500;
                Message = "Error: A Concurrency Error Occurred - Check the Task has not already completed";
            }

            if (Exception.InnerException != null)
            {
                if (Exception.InnerException.Message.Contains("Cannot insert duplicate key row in object"))
                {
                    StatusCode = 500;
                    Message = "Error: A Duplicate Constraint was violated. Check the Data does not already exist.";
                }
            }
        }
    }
}