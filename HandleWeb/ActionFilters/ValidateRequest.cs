﻿using System;
using System.Collections.Generic;
using System.Linq;
using ApplicationHelper.Messages;
using ApplicationHelper.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace HandleWeb.ActionFilters
{

    public class ValidateRequestAttribute : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (!filterContext.ModelState.IsValid)
            {
                // 400 Bad Request
                foreach (var value in filterContext.ModelState.Values)
                {
                    foreach (var error in value.Errors)
                    {
                        if (error.Exception != null)
                        {
                            var errorResponse = new ErrorResponse(nameof(Message.BadRequest), 400, Message.BadRequest);
                            filterContext.Result = new ObjectResult(errorResponse) { StatusCode = 400 };
                            return;
                        }
                    }
                }

                // 422 Unprocessable Entity
                var errors = filterContext.ModelState.Where(x => x.Value.Errors.Count > 0).ToDictionary(
                    kvp => kvp.Key,
                    kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray()
                );

               
                var validationErrorsResponse = new List<ValidationErrorResponse>();
                foreach (var error in errors)
                {
                    foreach (var value in error.Value)
                    {
                        var index = value.IndexOf(" ", StringComparison.Ordinal);
                        var code = value.Substring(0, index);
                        var message = value.Substring(index + 1);
                        validationErrorsResponse.Add(new ValidationErrorResponse(code, error.Key, message));
                    }
                }
                var validationsResponse = new ValidationResponse(nameof(Message.ValidationFailed), 422, Message.ValidationFailed, validationErrorsResponse);

                filterContext.Result = new ObjectResult(validationsResponse) { StatusCode = 422 };
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
        }
    }
}