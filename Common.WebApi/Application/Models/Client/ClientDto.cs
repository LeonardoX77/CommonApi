using Common.Core.Data.Interfaces;

namespace Common.WebApi.Application.Models.Client
{
#nullable enable
    public class ClientDto : IEntity
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public DateTime DateCreated { get; } = DateTime.Now; // << Clients are not allowed to set this field
    }

}
