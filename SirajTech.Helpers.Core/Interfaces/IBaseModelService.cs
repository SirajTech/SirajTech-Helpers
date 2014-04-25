using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace SirajTech.Helpers.Core
{
    public interface IBaseModelService<T> : IValidatable
    {
        /// <summary>
        ///     Returns a list of objects as IEnumerable for DS after applying the where statement against them.
        /// </summary>
        /// <param name="whereClause"> Where clause to search for </param>
        /// <param name="includeProperties"> Any properties wanted to be included cause LazyLoading is disabled by default </param>
        /// <returns> List of wanted objects as IEnumerable </returns>
        IList<T> Find(Expression<Func<T, bool>> whereClause, params Expression<Func<T, object>>[] includeProperties);
        


        /// <summary>
        ///     Returns a list of objects as IEnumerable for DS after applying the where and paging statements against them.
        /// </summary>
        /// <param name="totalCount"> Total records count as out parameter </param>
        /// <param name="orderBy"> Order By clause to sort the result before do paging </param>
        /// <param name="pageIndex"> Page number starts from '1' </param>
        /// <param name="pageSize"> Page size starts from '1' and default is '10' </param>
        /// <param name="whereClause"> Where clause to search for before do paging </param>
        /// <param name="includeProperties"> Any properties wanted to be included cause LazyLoading is disabled by default </param>
        /// <returns> List of wanted objects as IEnumerable </returns>
        IList<T> GetPage(out int totalCount, Expression<Func<T, IComparable>> orderBy, int pageIndex = 1, int pageSize = 10, Expression<Func<T, bool>> whereClause = null, params Expression<Func<T, object>>[] includeProperties);



        /// <summary>
        ///     Returns a list of objects as IEnumerable for DS after applying the where and paging statements against them.
        /// </summary>
        /// <param name="totalCount"> Total records count as out parameter </param>
        /// <param name="orderBy"> OrderBy PropertyName to sort the result before do paging
        ///     <example>
        ///         <code>"CategoryId desc ThenBy UserId"</code>
        ///     </example>
        /// </param>
        /// <param name="pageIndex"> Page number starts from '1' </param>
        /// <param name="pageSize"> Page size starts from '1' and default is '10' </param>
        /// <param name="whereClause"> Where clause to search for before do paging
        ///     <example>
        ///         This sample shows how to set the whereClause parameter <code>"CategoryID = 3 AND UnitPrice > 3"</code>
        ///     </example>
        /// </param>
        /// <param name="includeProperties"> Any properties wanted to be included cause LazyLoading is disabled by default </param>
        /// <returns> List of wanted objects as IEnumerable </returns>
        IList<T> GetPage(out int totalCount, string orderBy, int pageIndex = 1, int pageSize = 10, string whereClause = null, params Expression<Func<T, object>>[] includeProperties);



        /// <summary>
        ///     Returns a list of ViewModel as IList after applying the where, sort and paging statements against them.
        /// </summary>
        /// <typeparam name="TViewModel"> ViewModel type which list will contain </typeparam>
        /// <param name="totalCount"> Total data store records count as out parameter </param>
        /// <param name="convertMethod"> This method will convert the IQueryable of Model to IQueryable of ViewModel </param>
        /// <param name="orderBy"> Order By clause to sort the result before do paging </param>
        /// <param name="pageIndex"> Page number starts from '1' </param>
        /// <param name="pageSize"> Page size starts from '1' and default is '10' </param>
        /// <param name="whereClause"> Where clause to search for before do paging </param>
        /// <returns> List of wanted ViewModel objects </returns>
        IList<TViewModel> GetViewModelPage<TViewModel>(out int totalCount, Func<IQueryable<T>, IQueryable<TViewModel>> convertMethod, Expression<Func<TViewModel, IComparable>> orderBy, int pageIndex = 1, int pageSize = 10, Expression<Func<TViewModel, bool>> whereClause = null);



        /// <summary>
        ///     Returns a list of ViewModel as IList after applying the where, sort and paging statements against them.
        /// </summary>
        /// <typeparam name="TViewModel"> ViewModel type which list will contain </typeparam>
        /// <param name="totalCount"> Total data store records count as out parameter </param>
        /// <param name="convertMethod"> This method will convert the IQueryable of Model to IQueryable of ViewModel </param>
        /// <param name="orderBy"> OrderBy PropertyName to sort the result before do paging
        ///     <example>
        ///         <code>"CategoryId desc ThenBy UserId"</code>
        ///     </example>
        /// </param>
        /// <param name="pageIndex"> Page number starts from '1' </param>
        /// <param name="pageSize"> Page size starts from '1' and default is '10' </param>
        /// <param name="whereClause"> Where clause to search for before do paging
        ///     <example>
        ///         This sample shows how to set the whereClause parameter <code>"CategoryID = 3 AND UnitPrice > 3"</code>
        ///     </example>
        /// </param>
        /// <returns> List of wanted ViewModel objects </returns>
        IList<TViewModel> GetViewModelPage<TViewModel>(out int totalCount, Func<IQueryable<T>, IQueryable<TViewModel>> convertMethod, string orderBy, int pageIndex = 1, int pageSize = 10, string whereClause = null);



        /// <summary>
        ///     Returns a list of ViewModel as IList after applying the where, sort and paging statements against them.
        /// </summary>
        /// <typeparam name="TViewModel"> ViewModel type which list will contain </typeparam>
        /// <param name="convertMethod"> This method will convert the IQueryable of Model to IQueryable of ViewModel </param>
        /// <param name="whereClause"> Where clause to search for before do paging </param>
        /// <returns> List of wanted ViewModel objects </returns>
        IList<TViewModel> GetViewModelList<TViewModel>(Func<IQueryable<T>, IQueryable<TViewModel>> convertMethod, Expression<Func<TViewModel, bool>> whereClause = null);



        /// <summary>
        ///     Returns a list of ViewModel as IList after applying the where statement against them.
        /// </summary>
        /// <typeparam name="TViewModel"> ViewModel type which list will contain </typeparam>
        /// <param name="convertMethod"> This method will convert the IQueryable of Model to IQueryable of ViewModel </param>
        /// <param name="whereClause"> Where clause to search for before do paging
        ///     <example>
        ///         This sample shows how to set the whereClause parameter <code>"CategoryID = 3 AND UnitPrice > 3"</code>
        ///     </example>
        /// </param>
        /// <returns> List of wanted ViewModel objects </returns>
        IList<TViewModel> GetViewModelList<TViewModel>(Func<IQueryable<T>, IQueryable<TViewModel>> convertMethod, string whereClause = null);



        /// <summary>
        ///     Returns single object by searching for where statement and get the first item.
        /// </summary>
        /// <param name="whereClause"> Where clause to search for </param>        
        /// <param name="includeProperties"> Any properties wanted to be included cause LazyLoading is disabled by default </param>
        /// <returns> First object which where statement returned </returns>
        T First(Expression<Func<T, bool>> whereClause, params Expression<Func<T, object>>[] includeProperties);



        /// <summary>
        ///     Returns single object by applying where statement and get the first item after converting it to ViewModel.
        /// </summary>
        /// <typeparam name="TViewModel"> ViewModel type which list will contain </typeparam>
        /// <param name="convertMethod"> This method will convert the IQueryable of Model to IQueryable of ViewModel </param>
        /// <param name="whereViewModelClause"> Where clause to apply against ViewModel IQueryable and get the FirstOrDefault item </param>
        /// <returns> Single or null ViewModel object </returns>
        TViewModel FirstViewModel<TViewModel>(Func<IQueryable<T>, IQueryable<TViewModel>> convertMethod, Expression<Func<TViewModel, bool>> whereViewModelClause) where TViewModel : class;



        /// <summary>
        ///     Returns single object by applying where statement and get the first item after converting it to ViewModel.
        /// </summary>
        /// <typeparam name="TViewModel"> ViewModel type which list will contain </typeparam>
        /// <param name="convertMethod"> This method will convert the IQueryable of Model to IQueryable of ViewModel </param>
        /// <param name="whereModelClause"> Where clause to apply against Model IQueryable and get the FirstOrDefault item </param>
        /// <returns> Single or null ViewModel object </returns>
        TViewModel FirstViewModelByModel<TViewModel>(Func<IQueryable<T>, IQueryable<TViewModel>> convertMethod, Expression<Func<T, bool>> whereModelClause) where TViewModel : class;
    }
}