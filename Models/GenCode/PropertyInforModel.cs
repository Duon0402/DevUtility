namespace DevUtility.Models.GenCode
{
    public class PropertyInforModel
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public bool? IsVisible { get; set; } = true;

        public PropertyInforModel(string name, string type, bool? isVisible)
        {
            Name = name;
            Type = type;
            IsVisible = isVisible;
        }
    }
}
