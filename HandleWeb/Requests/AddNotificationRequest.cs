using System.ComponentModel.DataAnnotations;

namespace HandleWeb.Requests
{
    public class AddNotificationRequest
    {
        [Required] public string Id { get; set; }
        [Required] public string Message { get; set; }
    }
}
