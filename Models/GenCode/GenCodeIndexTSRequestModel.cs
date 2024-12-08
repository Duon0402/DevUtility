namespace DevUtility.Models
{
    public class GenCodeIndexTSRequestModel
    {
        public ClassInforModel? IndexModel { get; set; }
        public ClassInforModel? FormModel { get; set; }
        public ClassInforModel? OptionModel { get; set; }
        public GenCodeIndexTSOptions? Options { get; set; }
    }
    public class GenCodeIndexTSOptions
    {
        public string ControlerName { get; set; } = "ControllerName";
        public bool AllowInsert { get; set; } = false;
        public bool AllowUpdate { get; set; } = false;
        public bool AllowDelete { get; set; } = false;
    }
}
