using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace SirajTech.Helpers.Core
{
    public class ViolationHandler : IViolationHandler
    {
        private readonly List<Violation> _violations;



        public ViolationHandler()
        {
            _violations = new List<Violation>();
        }



        public List<Violation> GetAllViolations()
        {
            return _violations;
        }



        public void AddViolation(params Violation[] violations)
        {
            _violations.AddRange(violations);
        }



        public void AddViolation(Type modelType, Exception innerException = null, ViolationType violationType = ViolationType.General, string propertyPath = null, string errorMessage = null)
        {
            _violations.Add(new Violation(modelType, propertyPath)
            {
                ViolationType = violationType,
                InnerException = innerException,
                ErrorMessage = errorMessage,
            });
        }



        public void AddViolation<T>(Expression<Func<T, object>> expression, ViolationType violationType, string errorMessage = null, Exception innerException = null)
        {
            _violations.Add(new Violation(typeof (T), HelperUtilities.GetPropertyPath(expression))
            {
                ViolationType = violationType,
                InnerException = innerException,
                ErrorMessage = errorMessage,
            });
        }



        public void AddManualViolation(string fullPropertyPath, ViolationType violationType = ViolationType.General, string errorMessage = null, Exception innerException = null)
        {
            _violations.Add(new Violation(fullPropertyPath)
            {
                ViolationType = violationType,
                InnerException = innerException,
                ErrorMessage = errorMessage,
            });
        }



        private string getAutomateErrorMessage(string propertyPath, ViolationType violationType, Type entityType = null)
        {
            var messageBuilder = new StringBuilder();
            switch (violationType)
            {
                case ViolationType.Required:
                    if (propertyPath.Length > 0)
                    {
                        messageBuilder.Append("The Field ");
                        messageBuilder.Append(propertyPath.Split('.').LastOrDefault());
                        messageBuilder.Append(" is required");
                    }
                    break;
                case ViolationType.Duplicated:
                    if (propertyPath.Length > 0)
                    {
                        messageBuilder.Append("There is already entity with the same ");
                        messageBuilder.Append(propertyPath.Split('.').LastOrDefault());
                    }
                    messageBuilder.Append(" value. This field should be unique");
                    break;
                case ViolationType.Invalid:
                    if (propertyPath.Length > 0)
                    {
                        messageBuilder.Append("The value of the ");
                        messageBuilder.Append(propertyPath.Split('.').LastOrDefault());
                    }
                    if (entityType != null)
                        messageBuilder.Append(entityType.Name);
                    messageBuilder.Append(" is invalid");
                    break;
                case ViolationType.MaxLength:
                    if (propertyPath.Length > 0)
                    {
                        messageBuilder.Append("The length of the ");
                        messageBuilder.Append(propertyPath.Split('.').LastOrDefault() + " in ");
                        if (entityType != null) 
                            messageBuilder.Append(entityType.Name);
                        messageBuilder.Append(" is too long");
                    }
                    break;
                case ViolationType.MinLength:
                    if (propertyPath.Length > 0)
                    {
                        messageBuilder.Append("The length of the ");
                        messageBuilder.Append(propertyPath.Split('.').LastOrDefault() + " in ");
                        if (entityType != null) messageBuilder.Append(entityType.Name);
                        messageBuilder.Append(" is too small");
                    }
                    break;
                default:
                    messageBuilder.Append("Error has happened during this operation. ");
                    if (entityType != null) 
                        messageBuilder.Append(entityType.Name);
                    messageBuilder.Append(" " + violationType.ToString());
                    break;
            }
            messageBuilder.Append(" !.");
            return messageBuilder.ToString();
        }
    }
}