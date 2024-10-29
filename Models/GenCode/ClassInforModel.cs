using DevUtility.Models.GenCode;

namespace DevUtility.Models
{
    public class ClassInforModel
    {
        public string? ClassName { get; set; }
        public List<PropertyInforModel>? Properties { get; set; }

        public ClassInforModel(string className, List<PropertyInforModel> properties)
        {
            ClassName = className;
            Properties = properties;
        }
        public ClassInforModel()
        {
        }
    }
}
