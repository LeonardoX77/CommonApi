using Common.Domain.Entities;
using Common.WebApi.Application.Models.Client;
using Microsoft.AspNetCore.Mvc;
using Common.Core.Data.Interfaces;
using Common.WebApi.Application.Controllers.Generic;
using Common.WebApi.Application.Models.Location;

namespace Common.WebApi.Application.Controllers
{
    /// <summary>
    /// Controller for managing clients.
    /// </summary>
    public class ClientController : GenericControllerBase<Client, ClientDto, ClientQueryFilter, ClientDtoValidator>
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="logger">Logger.</param>
        /// <param name="clientService">Client service.</param>
        public ClientController(
            ILogger<ClientController> logger,
            IBaseService<Client, int> clientService) : base(logger, clientService)
        {
        }

    }
}
