using Common.Domain.Entities;
using Common.WebApi.Application.Models.Location;
using Microsoft.AspNetCore.Mvc;
using Common.Core.Data.Interfaces;
using Common.WebApi.Application.Controllers.Generic;
using Common.WebApi.Application.Models.Photographer;

namespace Common.WebApi.Application.Controllers
{
    /// <summary>
    /// Controller for managing Locations.
    /// </summary>
    public class LocationController : GenericControllerBase<Location, LocationDto, LocationQueryFilter, LocationDtoValidator>
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="logger">Logger.</param>
        /// <param name="LocationService">Location service.</param>
        public LocationController(
            ILogger<LocationController> logger,
            IBaseService<Location, int> LocationService) : base(logger, LocationService)
        {
        }
    }
}
