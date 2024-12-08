using DevUtility.Base;
using DevUtility.Models;
using System.Text;
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

                const string pattern2 = @"public\s+((?:List<\w+>|[\w?]+(?:\[\])?)|[\w<>]+\s*[\w\s,]*)\s+(\w+)\s*\{\s*get;\s*set;\s*\}";
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

                var rs = new ClassInforModel()
                {
                    ClassName = className,
                    Properties = listProps
                };

                return Result.Ok(rs);
            }
            catch (Exception ex)
            {
                msg = "Đã có lỗi xảy ra khi GetClassInfor";
                return Result.Exception<ClassInforModel>(msg, ex);
            }
        }

        public Result<string> GenCodeModelTS(ClassInforModel classInfor)
        {
            string msg;
            var rs = new StringBuilder();
            try
            {
                if (classInfor == null)
                {
                    msg = "Dữ liệu đầu vào không hợp lệ";
                    return Result.Error<string>("01", msg);
                }

                rs.AppendLine($"namespace My {{");
                rs.AppendFormat("\texport class {0} extends ViewModel {{\n", classInfor.ClassName);

                foreach (var prop in classInfor.Properties!)
                {
                    string tsType = MapTypeCSharpToTS(prop.Type);
                    rs.AppendFormat("\t\tpublic {0}: {1};\n", prop.Name, tsType);
                }

                rs.AppendLine("\t}");
                rs.AppendLine("}");

                return Result.Ok<string>(rs.ToString());
            }
            catch (Exception ex)
            {
                msg = "Đã có lỗi xảy khi GenCodeModelTS";
                return Result.Exception<string>(msg, ex);
            }
        }

        public Result<string> GenCodeIndexTS(GenCodeIndexTSRequestModel request)
        {
            string msg;
            var rs = new StringBuilder();
            try
            {
                if(request == null) 
                {
                    msg = "Dữ liệu đầu vào không hợp lệ";
                    return Result.Error<string>("01", msg);
                }

                var indexModel = request.IndexModel;
                var formModel = request.FormModel;
                var optionModel = request.OptionModel;

                return Result.Ok("");
            }
            catch (Exception ex)
            {
                msg = "Đã có lỗi xảy ra khi GenCodeIndexTS";
                return Result.Exception<string>(msg, ex);
            }
        }

        private string MapTypeCSharpToTS(string type)
        {
            return type switch
            {
                "string" => "string",
                "int" => "number",
                "decimal" => "number",
                "float" => "number",
                "double" => "number",
                "bool" => "boolean",
                "DateTime" => "Date",
                _ => "any"

            };
        }
    }
}
