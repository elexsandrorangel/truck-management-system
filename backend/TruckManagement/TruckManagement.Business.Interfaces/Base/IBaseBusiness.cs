﻿using System.Collections.Generic;
using System.Threading.Tasks;
using TruckManagement.Models.Entities.Base;
using TruckManagement.ViewModels.Base;

namespace TruckManagement.Business.Interfaces.Base
{
    public interface IBaseBusiness<TEntity, TModel>
        where TEntity : BaseEntity
        where TModel : BaseViewModel
    {
        #region Add

        /// <summary>
        /// Inserts a single object to the database and commits the change
        /// </summary>
        /// <remarks>Asynchronous</remarks>
        /// <param name="t">The object to insert</param>
        /// <returns>The resulting object including its primary key after the insert</returns>
        Task<TModel> AddAsync(TModel t);

        /// <summary>
        /// Inserts a collection of objects into the database and commits the changes
        /// </summary>
        /// <remarks>Asynchronous</remarks>
        /// <param name="tList">An IEnumerable list of objects to insert</param>
        /// <returns>The IEnumerable resulting list of inserted objects including the primary keys</returns>
        Task<IEnumerable<TModel>> AddAsync(IEnumerable<TModel> tList);

        #endregion Add

        #region Count

        /// <summary>
        /// Gets the count of the number of objects in the database
        /// </summary>
        /// <remarks>Asynchronous</remarks>
        /// <returns>The count of the number of objects</returns>
        Task<long> CountAsync();

        #endregion Count

        #region Delete

        /// <summary>
        /// Deletes a single object from the database and commits the change
        /// </summary>
        /// <remarks>Asynchronous</remarks>
        /// <param name="id">The object identifier</param>
        Task DeleteAsync(int id);

        /// <summary>
        /// Deletes a single object from the database and commits the change
        /// </summary>
        /// <remarks>Asynchronous</remarks>
        /// <param name="t">The object to delete</param>
        Task DeleteAsync(TModel t);

        #endregion Delete

        #region Get

        /// <summary>
        /// Returns a single object with a primary key of the provided id
        /// </summary>
        /// <remarks>Asynchronous</remarks>
        /// <param name="id">The primary key of the object to fetch</param>
        /// <returns>A single object with the provided primary key or null</returns>
        Task<TModel> GetAsync(int id, bool track = false);

        /// <summary>
        /// Gets a collection of all objects in the database
        /// </summary>
        /// <remarks>Asynchronous</remarks>
        /// <returns>An IEnumerable of every object in the database</returns>
        Task<IEnumerable<TModel>> GetAsync(int page = 1, int qty = int.MaxValue);

        #endregion Get

        #region Update

        /// <summary>
        /// Updates a single object based on the provided primary key and commits the change
        /// </summary>
        /// <remarks>Asynchronous</remarks>
        /// <param name="updated">The updated object to apply to the database</param>
        /// <returns>The resulting updated object</returns>
        Task<TModel> UpdateAsync(TModel updated);

        #endregion Update
    }
}
