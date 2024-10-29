namespace DevUtility.Models
{
    public class GenCodeIndexTSRequestModel
    {
        public ClassInforModel IndexModel { get; set; }
        public ClassInforModel? FormModel { get; set; } = null;
        public ClassInforModel? OptionModel { get; set; } = null;

        public string ControlerName { get; set; } = "ControllerName";
        public bool AllowInsert { get; set; } = false;
        public bool AllowUpdate { get; set; } = false;
        public bool AllowDelete { get; set; } = false;
    }
}
