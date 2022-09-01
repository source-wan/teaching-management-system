namespace EMS.App.ResDto;
/// <summary>
/// 响应的状态码的枚举
/// </summary>
public enum ResponseCode
{
    Success = 1000,
    Fail = 3003,
    NotFound = 4000
}

/// <summary>
/// 转化为字符串后就可以被响应出去的类
/// </summary>
public class ResponseDto
{
    /// <summary>
    /// 本次操作的响应的状态码
    /// </summary>
    /// <value></value>
    public ResponseCode Code { get; set; }
    /// <summary>
    /// 需要被携带的数据
    /// </summary>
    /// <value></value>
    public dynamic? Data { get; set; }
    /// <summary>
    /// 附带的信息
    /// </summary>
    /// <value></value>
    public string Message { get; set; } = null!;

    // 下面都是用于创建对应对象的模板函数
    public ResponseDto CustomResponse(ResponseCode code, string message, dynamic? data = null)
    {
        return new ResponseDto
        {
            Code = code,
            Message = message,
            Data = data
        };
    }
    public ResponseDto DataIsEmpty(Guid Id)
    {
        return new ResponseDto
        {
            Code = ResponseCode.NotFound,
            Message = $"Id为{Id}的数据不存在"
        };
    }

    public ResponseDto DataIsEmpty(IEnumerable<Guid> id)
    {
        return new ResponseDto
        {
            Code = ResponseCode.NotFound,
            Message = $"id 为 {id} 的数据不存在"
        };
    }

    public ResponseDto ReturnDataInfoWithCount(dynamic data, int count)
    {
        return new ResponseDto
        {
            Code = ResponseCode.Success,
            Message = "获取数据成功",
            Data = new
            {
                Data = data,
                Count = count
            }
        };
    }
    public ResponseDto ReturnDataInfo(dynamic data)
    {
        return new ResponseDto
        {
            Code = ResponseCode.Success,
            Message = "获取数据成功",
            Data = data
        };
    }

    public ResponseDto UpdateDataSuccess(dynamic origin, dynamic updated)
    {
        return new ResponseDto
        {
            Code = ResponseCode.Success,
            Data = new
            {
                Origin = origin,
                Updated = updated
            },
            Message = "更新数据成功"
        };
    }

    public ResponseDto TargetSameAsSouce(dynamic origin, dynamic target)
    {
        return new ResponseDto
        {
            Code = ResponseCode.Fail,
            Data = new
            {
                Origin = origin,
                Target = target
            },
            Message = "源与目标一致,无需更新"
        };
    }

    public ResponseDto CreateDataSuccess(dynamic data)
    {
        return new ResponseDto
        {
            Code = ResponseCode.Success,
            Message = "新建数据成功",
            Data = data
        };
    }

    public ResponseDto CreateDataFail(string propName, dynamic data)
    {
        return new ResponseDto
        {
            Code = ResponseCode.Fail,
            Message = $"{propName}不能为空",
            Data = data
        };
    }

    public ResponseDto DeleteDataSuccess(dynamic data)
    {
        return new ResponseDto
        {
            Code = ResponseCode.Success,
            Message = "删除数据成功",
            Data = data
        };
    }
}
