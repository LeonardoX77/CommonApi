using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Common.WebApi.Infrastructure.Models.Response;
using Common.Core.Data.Identity.Enums;
using Common.Core.Data.Interfaces;
using System.Net;

using Microsoft.AspNetCore.Authorization;
using Common.Core.Generic.DynamicQueryFilter.Interfaces;

namespace Common.WebApi.Application.Controllers.Generic
{
    /// <summary>
    /// Controller base which inherits from BaseController and provides additional features:
    /// - Marks the controller as an API controller with [ApiController] attribute.
    /// - Requires authorization with [Authorize] attribute.
    /// - Handles CRUD operations using HTTP verbs.
    /// </summary>
    /// <typeparam name="TEntity">The entity type.</typeparam>
    /// <typeparam name="TDto">The DTO type.</typeparam>
    /// <typeparam name="TQueryFilter">The query filter type.</typeparam>
    /// <typeparam name="TValidator">The validator type.</typeparam>
    [ApiController]
    [Authorize]
    [Route("api/v1/[controller]")]
    public class GenericControllerBase<TEntity, TDto, TQueryFilter, TValidator> : BaseController<TEntity, int>
        where TEntity : class, IEntity
        where TDto : class, IEntity
        where TQueryFilter : class, IDynamicQueryFilter, IPagination, new()
        where TValidator : AbstractValidator<TDto>, new()
    {
        public GenericControllerBase(
            ILogger<BaseController<TEntity, int>> logger, 
            IBaseService<TEntity, int> service) : base(logger, service) { }

        /// <summary>
        /// Gets an entity by id.
        /// </summary>
        /// <param name="id"></param>
        [HttpGet("{id}", Name = $"[controller]/{nameof(Get)}")]
        //[HttpGet("{id}", Name = "[controller]/GetById")]
        public async Task<IActionResult> Get(int id)
        {
            return await base.Get<TDto>(id);
        }

        /// <summary>
        /// Gets a list of entities by filter.
        /// </summary>
        /// <param name="filter"></param>
        [HttpPost("query")]
        public async Task<IActionResult> Query(TQueryFilter filter)
        {
            return await base.Get<TDto, TQueryFilter>(filter);
        }

        /// <summary>
        /// Creates a new entity.
        /// </summary>
        /// <param name="dto"></param>
        [HttpPost]
        public virtual async Task<IActionResult> Create([FromBody] TDto dto)
        {
            return await base.Create<TDto, TValidator>(dto);
        }

        /// <summary>
        /// Updates an entity.
        /// </summary>
        /// <param name="dto"></param>
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] TDto dto)
        {
            return await base.Update<TDto, TValidator>(CrudAction.UPDATE, dto.Id, dto);
        }

        /// <summary>
        /// Partially updates an entity.
        /// </summary>
        /// <param name="dto"></param>
        [HttpPatch]
        public async Task<IActionResult> Patch([FromBody] TDto dto)
        {
            return await base.Update<TDto, TValidator>(CrudAction.UPDATE_PATCH, dto.Id, dto);
        }

        /// <summary>
        /// Deletes an entity.
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return await base.Delete<TDto, TValidator>(id);
        }
    }
}
