using AutoMapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TruckManagement.Business.Interfaces.Base;
using TruckManagement.Infra.Core.Exceptions;
using TruckManagement.Models.Entities.Base;
using TruckManagement.Repository.Interfaces.Base;
using TruckManagement.ViewModels.Base;

namespace TruckManagement.Business.Base
{
    public abstract class BaseBusiness<TEntity, TModel, TRepo> : IBaseBusiness<TEntity, TModel>
        where TEntity : BaseEntity
        where TModel : BaseViewModel
        where TRepo : IBaseRepository<TEntity>
    {
        #region Fields

        protected readonly TRepo Repository;

        protected readonly IMapper _mapper;

        #endregion Fields

        #region Ctor

        protected BaseBusiness(TRepo repository, IMapper mapper)
        {
            Repository = repository;
            _mapper = mapper;
        }

        #endregion Ctor

        #region Mapper

        /// <summary>
        /// Converts a entity object to value object
        /// </summary>
        /// <param name="t"><paramref name="t"/> Entity class</param>
        /// <returns>Converted model</returns>
        internal TModel ModelFromEntity(TEntity t)
        {
            return t == null ? null : _mapper.Map<TModel>(t);
        }

        /// <summary>
        /// Converts a list of entities object to value object enumerable
        /// </summary>
        /// <param name="entities"><paramref name="entities"/> Entity class</param>
        /// <returns></returns>
        internal IEnumerable<TModel> ModelFromEntity(IEnumerable<TEntity> entities)
        {
            return entities == null ? null : _mapper.Map<IEnumerable<TModel>>(entities);
        }

        internal TEntity EntityFromModel(TModel model)
        {
            return _mapper.Map<TEntity>(model);
        }

        internal IEnumerable<TEntity> EntityFromModel(IEnumerable<TModel> models)
        {
            return _mapper.Map<IEnumerable<TEntity>>(models);
        }

        #endregion Mapper

        #region Add

        public virtual async Task<TModel> AddAsync(TModel t)
        {
            ValidateInsert(t);

            return ModelFromEntity(await Repository.AddAsync(EntityFromModel(t)))!;
        }

        public virtual async Task<IEnumerable<TModel>> AddAsync(IEnumerable<TModel> tList)
        {
            var list = tList;

            ValidateInsert(tList);
            return ModelFromEntity(await Repository.AddAsync(EntityFromModel(list)))!;
        }

        #endregion Add

        #region Count

        public virtual async Task<long> CountAsync()
        {
            return await Repository.CountAsync();
        }

        #endregion Count

        #region Delete

        public virtual async Task DeleteAsync(int id)
        {
            await Repository.DeleteAsync(id);
        }

        public virtual async Task DeleteAsync(TModel t)
        {
            await ValidateDeleteAsync(t);
            await Repository.DeleteAsync(EntityFromModel(t));
        }

        #endregion Delete

        #region Get

        public virtual async Task<IEnumerable<TModel>> GetAsync(int page = 1, int qty = int.MaxValue)
        {
            if (page > 0)
            {
                page -= 1;
            }
            else
            {
                page = 0;
            }
            return ModelFromEntity(await Repository.GetAsync(page, qty, false))!;
        }

        public virtual async Task<TModel> GetAsync(int id, bool track = false)
        {
            return ModelFromEntity(await Repository.GetAsync(id, track));
        }

        #endregion Get

        #region Update

        public virtual async Task<TModel> UpdateAsync(TModel updated)
        {
            await ValidateUpdateAsync(updated);

            TModel record = await GetAsync(updated.Id, false);

            if (record == null)
            {
                throw new AppNotFoundException();
            }

            //updated.TenantId = record.TenantId;
            updated.CreatedAt = record.CreatedAt;

            var data = await Repository.UpdateAsync(EntityFromModel(updated));
            return ModelFromEntity(data)!;
        }

        #endregion Update

        #region Validations

        /// <summary>
        /// Data validation before insert model into database
        /// </summary>
        /// <param name="model">Model to insert</param>
        /// <exception cref="AppException">Business exception</exception>
        protected abstract void ValidateInsert(TModel model);

        /// <summary>
        /// Data validation before insert model into database
        /// </summary>
        /// <param name="models">Models to insert</param>
        /// <exception cref="AppException">Business exception</exception>
        protected virtual void ValidateInsert(IEnumerable<TModel> models)
        {
            foreach (var item in models)
            {
                ValidateInsert(item);
            }
        }

        /// <summary>
        /// Data validation before update model into database
        /// </summary>
        /// <param name="model">Model to update</param>
        /// <remarks>Asynchronous</remarks>
        /// <exception cref="AppException">Business exception</exception>
        protected virtual async Task ValidateUpdateAsync(TModel model)
        {
            if (model == null || model?.Id == 0)
            {
                throw new InvalidOperationException();
            }
            var record = await GetAsync(model.Id, false);
            if (record == null)
            {
                throw new AppNotFoundException();
            }

            ValidateInsert(model);
        }

        /// <summary>
        /// Data validation before remove model from database
        /// </summary>
        /// <param name="model">Model to remove</param>
        /// <remarks>Asynchronous</remarks>
        /// <exception cref="AppException">Business exception</exception>
        protected virtual async Task ValidateDeleteAsync(TModel model)
        {
            if (model == null || model?.Id == 0)
            {
                throw new InvalidOperationException();
            }
            var record = await GetAsync(model.Id, false);
            if (record == null)
            {
                throw new AppNotFoundException();
            }
        }

        protected async Task<TEntity> GetEntityOrThrowAsync(TModel model)
        {
            var data = await Repository.GetAsync(model.Id, false);

            if (data == null)
            {
                throw new AppNotFoundException();
            }

            return data;
        }

        #endregion Validations
    }
}
