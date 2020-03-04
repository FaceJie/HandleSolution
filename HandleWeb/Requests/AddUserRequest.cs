using System.ComponentModel.DataAnnotations;

namespace HandleWeb.Requests
{
    public class AddUserRequest
    {
        [Required] public string UserId { get; set; }
    }
}
