using System;

namespace Velox.Api.Infrastructure.DTO
{
    public class SessionDTO
    {
        public string SessionId { get; set; }
        public int UserId { get; set; }
        public string SessionData { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ExpiresAt { get; set; }
    }
}
