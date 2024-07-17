using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Common.WebApi.Application.Controllers.Generic.Interfaces;
using Common.WebApi.Infrastructure.Models.Response;
using Common.Core.CustomExceptions;
using Common.Core.Data.Identity.Enums;
using Common.Core.Data.Interfaces;
using Common.Core.Generic.DynamicQueryFilter.Interfaces;
using System.Net;

namespace Common.WebApi.Application.Controllers.Generic
{
    /// <summary>
    /// Base controller which encapsulates and centralizes common logic for CRUD operations (Create, Read, Update, Delete). This avoids code duplication
    /// in each specific domain controller. If a case arises that cannot be implemented generically, any functionality can be inherited and overridden.
    /// By using generic types, BaseController can handle any type of entity without needing to know specific details about the entity.
    /// Additionally, it manages the type of IActionResult returned and handles errors consistently across different controllers.
    /// </summary>
    /// <typeparam name="T">The entity type.</typeparam>
    /// <typeparam name="TKey">The type of the entity's key.</typeparam>
    public class BaseController<T, TKey> : ControllerBase, IBaseController<T, TKey>
        where T : class, IEntity
        where TKey : struct
    {
        protected readonly ILogger<BaseController<T, TKey>> _logger;
        protected readonly IBaseService<T, TKey> _baseService;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="log">Logger.</param>
        public BaseController(ILogger<BaseController<T, TKey>> log) 
        { 
            _logger = log;
        }
        public BaseController(ILogger<BaseController<T, TKey>> log, IBaseService<T, TKey> baseService) : this(log)
        {
            _baseService = baseService;
        }

        /// <summary>
        /// Get an entity by its PK
        /// </summary>
        /// <typeparam name="TDto"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        public async virtual Task<IActionResult> Get<TDto>(TKey id)
        {
            TDto dto = await _baseService.GetByPKAsync<TDto>(id);

            if (dto != null)
            {
                return Ok(new Response<TDto>(dto));
            }

            return Ko(new BaseResponse((int)HttpStatusCode.NotFound));
        }

        /// <summary>
        /// Get a filtered list of entities
        /// </summary>
        /// <typeparam name="TDto"></typeparam>
        /// <typeparam name="TQueryFilter"></typeparam>
        /// <param name="filter"></param>
        /// <returns></returns>
        public async virtual Task<IActionResult> Get<TDto, TQueryFilter>(TQueryFilter filter)
            where TQueryFilter : class, IDynamicQueryFilter, IPagination, new()
        {
            IPaginatedResult<TDto> result = await _baseService.Get<TDto, TQueryFilter>(filter);

            if (result != null)
            {
                return result.Items.Count() > 0
                    ? Ok(new Response<TDto>(result.Items.ToList(), result.Page, result.PageSize, result.TotalCount))
                    : NoContent();
            }

            return NoContent();
        }

        /// <summary>
        /// Validates and creates an entity
        /// </summary>
        /// <typeparam name="TDto"></typeparam>
        /// <typeparam name="TValidator"></typeparam>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async virtual Task<IActionResult> Create<TDto, TValidator>(TDto dto)
            where TDto : class, IEntity
            where TValidator : AbstractValidator<TDto>
        {
            _baseService.ValidateDto<TDto, TValidator>(CrudAction.CREATE, dto);

            T result = await _baseService.AddAsync(dto);

            if (result != null)
            {
                TDto resultDto = _baseService.MapEntityToDto<TDto>(result);
                var controllerName = ControllerContext.ActionDescriptor.ControllerName;
                return CreatedAtRoute(
                    $"{controllerName}/Get",
                    new { id = result.Id },
                    new Response<TDto>(resultDto));
            }

            return Ko(new BaseResponse((int)HttpStatusCode.InternalServerError));
        }

        /// <summary>
        /// Updates an entity
        /// </summary>
        /// <typeparam name="TDto"></typeparam>
        /// <typeparam name="TValidator"></typeparam>
        /// <param name="crudAction"></param>
        /// <param name="id"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async virtual Task<IActionResult> Update<TDto, TValidator>(CrudAction crudAction, TKey id, TDto dto)
        where TDto : class, IEntity
        where TValidator : AbstractValidator<TDto>
        {
            return await ExecuteCrudOperation<TDto, TValidator>(crudAction, id, dto);
        }

        /// <summary>
        /// Deletes an entity
        /// </summary>
        /// <typeparam name="TDto"></typeparam>
        /// <typeparam name="TValidator"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        public async virtual Task<IActionResult> Delete<TDto, TValidator>(TKey id)
        where TDto : class, IEntity
        where TValidator : AbstractValidator<TDto>
        {
            return await ExecuteCrudOperation<TDto, TValidator>(CrudAction.DELETE, id);
        }

        /// <summary>
        /// Validates and update or delete an entity
        /// </summary>
        /// <typeparam name="TDto"></typeparam>
        /// <typeparam name="TValidator"></typeparam>
        /// <param name="crudAction"></param>
        /// <param name="id"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        private async Task<IActionResult> ExecuteCrudOperation<TDto, TValidator>(CrudAction crudAction, TKey id, TDto dto = null)
            where TDto : class, IEntity
            where TValidator : AbstractValidator<TDto>
        {
            if (dto != null)
                _baseService.ValidateDto<TDto, TValidator>(crudAction, dto);

            try
            {
                switch (crudAction)
                {
                    case CrudAction.UPDATE:
                    case CrudAction.UPDATE_PATCH:
                        await _baseService.UpdateAsync(dto);
                        break;
                    case CrudAction.DELETE:
                        await _baseService.DeleteAsync(id);
                        break;
                    default:
                        break;
                }

                return NoContent();
            }
            catch (NoDbRecordException)
            {
                return Ko(new BaseResponse((int)HttpStatusCode.NotFound));
            }
            catch (Exception ex)
            {
                return Ko(new BaseResponse((int)HttpStatusCode.InternalServerError, ex.Message));
            }
        }

        /// <summary>
        /// Handle response error
        /// </summary>
        /// <param name="response">Response.</param>
        /// <returns>ObjectResult</returns>
        protected IActionResult Ko(BaseResponse response)
        {
            _logger.LogError(response.Error.ToString());

            int statusCode = response.StatusCode ?? (int)HttpStatusCode.InternalServerError;

            return new ObjectResult(response)
            {
                StatusCode = statusCode
            };
        }
    }
}