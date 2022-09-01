using EMS.App.Common.Interface;
using EMS.Domain.Entity;
using Microsoft.AspNetCore.Mvc.Filters;

namespace EMS.Infrastructure.Filters
{
    public class HTTPGlobalExceptionFilter:IAsyncExceptionFilter
    {
        private readonly IRepository<AppException> _appException;

        public HTTPGlobalExceptionFilter(IRepository<AppException> AppException)
        {
            _appException=AppException;
        }
        
        public async Task OnExceptionAsync(ExceptionContext context)
        {
            //捕获全局异常，记入数据库日志
            await _appException.AddAsync(new AppException
            {
                ShortMessage = context.Exception.Message,
                FullMessage = context.Exception.ToString()
            });
            context.ExceptionHandled = true;
        }
    }
}