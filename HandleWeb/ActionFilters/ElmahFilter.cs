﻿using ElmahCore;
using SharedHelper.Exceptions;

namespace HandleWeb.ActionFilters
{
    public class ElmahFilter :  IErrorFilter
    {
        public void OnErrorModuleFiltering(object sender, ExceptionFilterEventArgs args)
        {
            // We skip our custom exceptions
            if(args.Exception is DomainException)
                args.Dismiss();
        }
    }
}