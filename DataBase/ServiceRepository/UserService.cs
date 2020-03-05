using ApplicationHelper.Messages;
using ApplicationHelper.Requests;
using ApplicationHelper.Responses;
using SharedHelper.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.ServiceRepository
{
    public static class UserService
    {
        public static Task<UserResponse> AddUser(AddUserRequest request)
        {
            // Get user
            var user = DBNull.Value;

            // Check if it exists
            if (user != null) throw new ConflictException(UserMessage.UserAlreadyExists);

            var response = new Task<UserResponse>(() => GetUser("1"));

            // Return
            return response;
        }
        public static UserResponse GetUser(string id)
        {
            UserResponse user = new UserResponse();
            return user;
        }
    }
}
