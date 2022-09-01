namespace EMS.Api.Controller;
public class TextBookController : ControllerBase
{
    private readonly IRepository<TextBookInfo> _textBookInfo;
    private readonly IQueryHelper<TextBookInfo> _queryHelper;
    private readonly IUpdateDataServer<TextBookInfo, UpdateTextBookDto> _updateServer;

    public TextBookController
    (
        IRepository<TextBookInfo> textBookInfo,
        IQueryHelper<TextBookInfo> queryHelper,
        IUpdateDataServer<TextBookInfo, UpdateTextBookDto> updateServer
    )
    {
        _textBookInfo = textBookInfo;
        _queryHelper = queryHelper;
        _updateServer = updateServer;
    }
    [HttpGet("/textbook/{textbookId}")]
    # region 获取教材信息
    public async Task<string> GetTextBookInfo(Guid textbookId)
    {
        var textbook = await _textBookInfo.GetById(textbookId);
        if (textbook == null) return ReturnTextBookIsEmpty(textbookId);

        return ReturnTextBookInfo(textbook, "获取教材信息成功");
    }
    # endregion

    [HttpGet("/textbook")]
    # region 获取教材列表
    public string GetTextBookList()
    {
        int index = int.Parse(_queryHelper.GetRequestQueryParam("index") ?? "1");
        int size = int.Parse(_queryHelper.GetRequestQueryParam("size") ?? "5");

        var students = _textBookInfo.Table.Where(_queryHelper.ValidateData);
        var count = students.Count();

        if (count > 0)
        {
            if (Request.Query.ContainsKey("orderbydesc") && (!Request.Query.ContainsKey("orderby")))
            {
                students = students
                .OrderByDescending(_queryHelper.SortByRequestParam)
                .Skip((index - 1) * size)
                .Take(size)
                .ToList();
            }
            else
            {
                students = students
                .OrderBy(_queryHelper.SortByRequestParam)
                .Where(_queryHelper.ValidateData)
                .Skip((index - 1) * size)
                .Take(size)
                .ToList();
            }
        }

        return ReturnTextBookInfo(new { Data = students, Count = count }, "获取教材列表成功");
    }
    # endregion

    [HttpPost("/textbook")]
    # region 添加教材
    public async Task<string> CreateTextBookInfo([FromBody] CreateTextBookDto textBookDto)
    {
        if (textBookDto == null) return new { Code = 3000, Msg = "时间格式错误,请使用形如yyyy-MM-ddThh:mm:ss+timezone的格式"}.SerializeObject();

        var textBookDtoProps = textBookDto.GetType().GetProperties();
        var textBookInfo = Activator.CreateInstance<TextBookInfo>();
        var textBookInfoProps = textBookInfo.GetType().GetProperties();

        foreach (var bookDto in textBookDtoProps)
        {
            var value = bookDto.GetValue(textBookDto);
            if (value == null || value.ToString().IsNullOrEmpty())
            {
                return new
                {
                    Code = 3003,
                    Msg = $"{bookDto.Name}不能为空"
                }.SerializeObject();
            }

            textBookInfoProps.FirstOrDefault(bookInfo => bookInfo.Name.Equals(bookDto.Name))
            ?.SetValue(textBookInfo, bookDto.GetValue(textBookDto));
        }

        // 把带时区的时间转化为不带时区的时间（+00）, 不转的话会报错
        textBookInfo.PublishAt = textBookInfo.PublishAt.ToUniversalTime();

        await _textBookInfo.AddAsync(textBookInfo);
        return ReturnTextBookInfo(textBookInfo, "添加教材信息成功");
    }
    # endregion

    [HttpPut("/textbook/{textbookId}")]
    # region 修改教材
    public async Task<string> UpdateTextBook(Guid textbookId, [FromBody] UpdateTextBookDto textbookDto)
    {
        var textbook = await _textBookInfo.GetById(textbookId);
        if (textbook == null) return ReturnTextBookIsEmpty(textbookId);

        var origin = await _updateServer.UpdateDataAsync(textbook, textbookDto);
        if (textbook == origin)
        {
            return new
            {
                Code = 3003,
                msg = "传入的信息与原本的信息一致或为空",
                data = new
                {
                    Origin = origin,
                    Target = textbook
                }
            }.SerializeObject();
        }

        return ReturnTextBookInfo(new { Origin = origin, Updated = textbook }, "修改教材信息成功");
    }
    # endregion

    [HttpDelete("/textbook/{textbookId}")]
    # region 删除教材
    public async Task<string> DeleteTextBookInfo(Guid textbookId)
    {
        var textbook = await _textBookInfo.GetById(textbookId);
        if (textbook == null) return ReturnTextBookIsEmpty(textbookId);
        await _textBookInfo.DeleteAsync(textbookId);
        return ReturnTextBookInfo(textbook, "删除教材信息成功");
    }
    #endregion

    [NonAction]
    private string ReturnTextBookIsEmpty(Guid textbookId)
    {
        return new
        {
            Code = 4000,
            Msg = $"Id为{textbookId}的教材不存在"
        }.SerializeObject();
    }

    [NonAction]
    private string ReturnTextBookInfo(dynamic data, string msg)
    {
        return new
        {
            Code = 1000,
            Data = data,
            Msg = msg
        }.SerializeObject();
    }
}
