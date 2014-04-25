using System.Collections.Generic;
using System.Linq;

namespace SirajTech.Helpers.Core
{
    public abstract class BaseValidatableClass : IValidatable
    {
        #region Fields and Constructor

        protected ViolationHandler violationHandler;
        private readonly List<Violation> violations;



        protected IList<Violation> getViolations(bool registeredInClassViolations = true)
        {
            if (registeredInClassViolations)
            {
                violations.AddRange(violationHandler.GetAllViolations());
            }
            var list = violationHandler.GetAllViolations();
            violationHandler = new ViolationHandler();
            return list;
        }



        protected BaseValidatableClass()
        {
            violationHandler = new ViolationHandler();
            violations = new List<Violation>();
        }

        #endregion



        #region Implementation Of IValidatableService<T>

        public virtual IList<Violation> GetViolations()
        {
            return violations;
        }



        public virtual bool IsValid()
        {
            return !(getViolations().Any() || violations.Any());
        }

        #endregion
    }
}