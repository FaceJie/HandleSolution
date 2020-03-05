

namespace ApplicationHelper.Messages
{
    public static class Message
    {
        // Authentication
        public const string NotAuthenticated = "你必须经过认证";
        public const string PermissionRequired = "无权访问 {0}";
        public const string Restricted = "你在这方面受到限制 {0}: {1}";

        // Validation
        public const string BadRequest = "无效的请求";
        public const string ValidationFailed = "验证失败";
        public const string NotFound = "未找到";
        public const string Conflict = "与当前状态有冲突";
        public const string InternalServerError = "内部服务器错误";
       
    }
}