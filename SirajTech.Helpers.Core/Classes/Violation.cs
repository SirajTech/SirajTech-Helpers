using System;
using System.Configuration;
using System.Linq.Expressions;
using System.Reflection.Emit;

namespace SirajTech.Helpers.Core
{
    public class Violation
    {
        private readonly string fullPropertyPath;



        public Violation(string fullPropertyPath)
        {
            this.fullPropertyPath = fullPropertyPath;
        }



        public Violation(Type modelType,string propertyName)
        {
            this.ModelType = modelType;
            this.PropertyName = propertyName;
        }



        public Type ModelType { get; private set; }
        public string PropertyName { get; private set; }
        public string ErrorMessage { get; set; }

        public string FriendlyMessage
        {
            get
            {
                var resourceTypeName = ConfigurationManager.AppSettings["ViolationResourceType"];
                if (string.IsNullOrEmpty(resourceTypeName))
                    return ErrorMessage;

                var resourceType = Type.GetType(resourceTypeName);
                if (resourceType == null)
                    throw new NullReferenceException("Error in initialize resource type. Please recheck the Configuration file");

                var value = HelperUtilities.GetResourceValue(resourceType, ErrorCode);
                return string.IsNullOrEmpty(value)
                        ? ErrorMessage
                        : value;
            }
        }

        public ViolationType ViolationType { get; set; }
        public Exception InnerException { get; set; }

        public string ErrorCode
        {
            get
            {
                var result = fullPropertyPath;
                
                if (!string.IsNullOrEmpty(fullPropertyPath)) 
                    return result + "_" + ViolationType;

                result = ModelType.Name;
                if (!string.IsNullOrEmpty(PropertyName))
                    result += "_" + PropertyName.Replace(".", "_") + "_" + ViolationType;

                else
                    result += "_" + ViolationType;

                return result;
            }
        }
    }
}
