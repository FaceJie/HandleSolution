

namespace ApplicationHelper.Messages
{
    public static class Message
    {
        // Authentication
        public const string NotAuthenticated = "未经授权";
        public const string PermissionRequired = "权限不足 {0}";
        public const string Restricted = "访问受限 {0}: {1}";

        // Validation
        public const string BadRequest = "错误请求";
        public const string ValidationFailed = "验证异常";
        public const string NotFound = "未找到目标";
        public const string Conflict = "发生冲突";
        public const string InternalServerError = "内部服务器错误";
       
    }
}