using Common.WebApi.Infrastructure.Models.Request;
using Common.Core.Generic.DynamicQueryFilter.Interfaces;

namespace Common.WebApi.Application.Models.Location
{
#nullable enable

    /// <summary>
    /// Filter for dynamic Fields
    /// </summary>
    public class LocationDynamicFieldsQueryFilter : BaseRequest, IDynamicQueryFilter
    {
        public List<int>? ListId { get; set; }
        public int? MaxId { get; set; }
        public int? MinId { get; set; }
        public string? ContainsName { get; set; }
        public string? ListName { get; set; }
        public DateTime? FromDateCreated { get; set; }
        public DateTime? ToDateCreated { get; set; }
    }
}
