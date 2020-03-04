


using System.ComponentModel.DataAnnotations;

namespace ApplicationHelper.Requests
{
    public class AddNotificationRequest
    {
        [Required] public string Id { get; set; }
        [Required] public string Message { get; set; }
    }
}
