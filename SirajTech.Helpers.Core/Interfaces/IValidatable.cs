using System.Collections.Generic;

namespace SirajTech.Helpers.Core
{
    public interface IValidatable
    {

        /// <summary>
        /// 
        /// </summary>
        IList<Violation> GetViolations();



        /// <summary>
        /// 
        /// </summary>
        /// <returns> </returns>
        bool IsValid();
    }
}