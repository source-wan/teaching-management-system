#nullable disable
using System.Reflection;
using EMS.Domain.Base;

namespace EMS.Api.Service
{
    public class QueryHelper<T> : IQueryHelper<T> where T : BaseEntity
    {

        private readonly IHttpContextAccessor _httpContextAccessor;

        public QueryHelper(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public object SortByRequestParam(T arg)
        {
            string column = this.GetRequestQueryParam("orderbydesc")
                ?? "CreatedAt";

            PropertyInfo studentProp = arg.GetType().GetProperty(
                column,
                BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
            var value = studentProp?.GetValue(arg);
            return value ?? arg.CreatedAt;
        }

        public string GetRequestQueryParam(string paramName)
        {
            return
            _httpContextAccessor.HttpContext.Request.Query.ContainsKey(paramName)
                ? _httpContextAccessor.HttpContext.Request.Query[paramName][0]
                : null;
        }

        public bool ValidateData(T data)
        {
            // 先验证数据是否被软删除
            if (data.IsDeleted == true || data.IsActived == false) return false;
            var ignoreString = "index,size,orderby,orderbydesc";
            var keys = _httpContextAccessor.HttpContext.Request.Query.Keys.Where(x => x.IndexOf(ignoreString) == -1);
            if (keys.Count() == 0) return true;
            // 请求携带的查询字符串的每个字段, index 和 size 除外
            foreach (var key in keys)
            {
                // 先用反射获取属性的值, 避免后续重复生成浪费时间
                var propValue = data.GetType()
                    .GetProperty(key, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance)
                    ?.GetValue(data);

                // 判断请求携带的字段是否合法， 如果合法（也就是说数据表中具有该字段， 且值也相等
                //则返回 true,否则继续下一次循环，直到数据的所有字段都被枚举完毕
                if
                (
                    // 判断传入的数据是否具有请求的字段
                    propValue != null
                        // 判断该字段是否为空， 如果为空，则返回 "null" <-- 这是个字符串
                        && (propValue is not null
                        // 该字段不为空， 判断该字段是否是 boolean, 如果是，则返回对应的值
                        ? propValue is not bool
                        // 如果以上两种情况都不是， 说明可以直接转换该字段的值为字符串
                        ? propValue.ToString()
                        : propValue is true ? "true" : "false"
                        : "null").IndexOf(GetRequestQueryParam(key)) != -1
                )
                {
                    // 有字段与所需的字段相同
                    return true;
                }
            }

            // 所有字段均不与需要的字段相同
            return false;
        }

    }
}