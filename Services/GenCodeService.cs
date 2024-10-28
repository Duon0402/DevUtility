using DevUtility.Base;
using DevUtility.Models;
using DevUtility.Models.GenCode;
using System.Text;
using System.Text.RegularExpressions;

namespace DevUtility.Services
{
    public class GenCodeService
    {
        public Task<Result<GenCodeResultModel>> GenCodeCSharpToTS(ClassInforModel indexModel, ClassInforModel? formModel, ClassInforModel? optionModel)
        {
            string msg = "";
            try
            {
                string indexModelResult = "";
                string formModelResult = "";
                string optionModelResult = "";
                if (indexModel == null)
                {
                    msg = "Model Index không được để trống";
                    return Result<GenCodeResultModel>.Error("01", msg);
                }

                indexModelResult = GenCSharpClassToTsClass(indexModel);

                if (formModel != null)
                {
                    formModelResult = GenCSharpClassToTsClass(formModel);
                }
                if (optionModel != null)
                {
                    optionModelResult = GenCSharpClassToTsClass(optionModel);
                }

                return Result<GenCodeResultModel>.Ok(new GenCodeResultModel()
                {
                    CodeIndexModel = indexModelResult,
                    CodeFormModel = formModelResult,
                    CodeOptionModel = optionModelResult
                });
            }
            catch (Exception ex)
            {
                msg = "Có lỗi xảy ra trong quá trình Gen Code";
                return Result<GenCodeResultModel>.Exception(msg, ex);
            }
        }

        private static string GenCSharpClassToTsClass(ClassInforModel infor)
        {
            StringBuilder result = new();

            result.AppendLine($"namespace My {{");
            result.AppendLine($"\texport class {infor.ClassName} extends ViewModel {{");

            if (infor.Properties != null)
            {
                foreach (var prop in infor.Properties)
                {
                    string tsType = MapCSharpTypeToTSType(prop.Type);
                    result.AppendLine($"\t\t{prop.Name}: {tsType};");
                }
            }

            result.AppendLine("\t}");
            result.AppendLine("}");
            result.AppendLine();

            return result.ToString();
        }

        private static string MapCSharpTypeToTSType(string type)
        {
            return type switch
            {
                "string" => "string",
                "int" => "number",
                "float" => "number",
                "double" => "number",
                "decimal" => "number",
                "bool" => "boolean",
                "DateTime" => "Date",
                _ => "any"
            };
        }

        public Task<Result<ClassInforModel>> GetClassInfor(string cSharpCode)
        {
            string msg;
            try
            {
                var result = new ClassInforModel();
                const string patten = @"\s+class\s+(\w+)(?:\s*:\s*[\w\s,]+)?\s*\{([^{}]*(?:\{[^{}]*\}[^{}]*)*)\}";
                var classMatch = Regex.Match(cSharpCode, patten, RegexOptions.Singleline);
                if(!classMatch.Success)
                {
                    msg = "Dữ liệu đầu vào không đúng định dạng";
                    return Result<ClassInforModel>.Error("02", msg);
                }
                result.ClassName = classMatch.Groups[1].Value;
                string classBody = classMatch.Groups[2].Value;
                result.Properties = new List<PropertyInforModel>();
                var propertyMatches = Regex.Matches(classBody, @"\s+(\w+)\s+(\w+)\s*\{");
                foreach (Match propMatch in propertyMatches)
                {
                    
                    var property = new PropertyInforModel
                    {
                        Type = propMatch.Groups[1].Value,
                        Name = propMatch.Groups[2].Value
                    };

                    result.Properties?.Add(property);
                }

                return Result<ClassInforModel>.Ok(result);
            }
            catch (Exception ex)
            {
                msg = "Có lỗi xảy ra trong quá trình Get Class Infor";
                return Result<ClassInforModel>.Exception(msg, ex);
            }
        }
    }
}
