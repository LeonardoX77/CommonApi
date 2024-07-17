using Common.Domain.Entities;
using Common.WebApi.Application.Models.Session;
using Microsoft.AspNetCore.Mvc;
using Common.Core.Data.Interfaces;
using Common.WebApi.Application.Controllers.Generic;
using Common.WebApi.Application.Models.Photographer;

namespace Common.WebApi.Application.Controllers
{
    /// <summary>
    /// Controller for managing Sessions.
    /// </summary>
    public class SessionController : GenericControllerBase<Session, SessionDto, SessionQueryFilter, SessionDtoValidator>
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="logger">Logger.</param>
        /// <param name="SessionService">Session service.</param>
        public SessionController(
            ILogger<SessionController> logger,
            IBaseService<Session, int> SessionService) : base(logger, SessionService)
        {
        }
    }
}
