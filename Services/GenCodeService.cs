using DevUtility.Base;
using DevUtility.Models;
using DevUtility.Models.GenCode;
using System.Text.RegularExpressions;

namespace DevUtility.Services
{
    public class GenCodeService
    {
        public Result<ClassInforModel> GetClassInfor(string cSharpCode)
        {
            string msg;
            try
            {
                if (string.IsNullOrEmpty(cSharpCode))
                {
                    msg = "Dữ liệu đầu vào không hợp lệ";
                    return Result.Error<ClassInforModel>("01", msg);
                }

                const string pattern1 = "public\\s+class\\s+(\\w+)(?:\\s*:\\s*[\\w\\s,]+)?\\s*\\{([^{}]*(?:\\{[^{}]*\\}[^{}]*)*)\\}";
                var classMatch = Regex.Match(cSharpCode, pattern1, RegexOptions.Singleline);

                if (!classMatch.Success)
                {
                    msg = "Dữ liệu đầu vào không đúng định dạng";
                    return Result.Error<ClassInforModel>("01", msg);
                }

                var className = classMatch.Groups[1].Value;
                var classBody = classMatch.Groups[2].Value;

                const string pattern2 = @"public\s+((?:List<\w+>|[\w?]+(?:\[\])?)|[\w<>]+\s*[\w\s,]*)\s+(\w+)\s*\{\s*get;\s*set;\s*\};";
                var propsMatch = Regex.Matches(classBody, pattern2);

                var listProps = new List<PropertyInforModel>();
                foreach (Match propMatch in propsMatch)
                {
                    var prop = new PropertyInforModel()
                    {
                        Type = propMatch.Groups[1].Value,
                        Name = propMatch.Groups[2].Value
                    };
                    listProps.Add(prop);
                }

                var rs = new ClassInforModel(className, listProps);
                return Result.Ok(rs);
            }
            catch (Exception ex)
            {
                msg = "Đã có lỗi xảy ra Khi GetClassInfor";
                return Result.Exception<ClassInforModel>(msg, ex);
            }
        }
    }
}
