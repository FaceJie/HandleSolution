


using System.ComponentModel.DataAnnotations;

namespace ApplicationHelper.Requests
{
    public class AddUserRequest
    {
        [Required] public string UserId { get; set; }
    }
}
