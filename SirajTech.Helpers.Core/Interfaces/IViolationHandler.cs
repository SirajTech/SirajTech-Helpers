using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace SirajTech.Helpers.Core
{
    public interface IViolationHandler
    {
        List<Violation> GetAllViolations();
        void AddViolation(params Violation[] violations);
        void AddViolation(Type modelType, Exception innerException = null, ViolationType violationType = ViolationType.General, string propertyPath = null, string errorMessage = null);
        void AddViolation<T>(Expression<Func<T, object>> expression, ViolationType violationType, string errorMessage = null, Exception innerException = null);
        void AddManualViolation(string fullPropertyPath, ViolationType violationType = ViolationType.General, string errorMessage = null, Exception innerException = null);
    }
}
