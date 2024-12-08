namespace DevUtility.Helpers
{
    public static class DateTimeHelper
    {
        public static DateTime Now => DateTime.Now;


        #region Format Date
        public static string FormatDate(DateTime dateTime)
        {
            return dateTime.ToString("yyyy-MM-dd");
        }
        public static string FormatDate(DateTime dateTime, string? format)
        {
            return dateTime.ToString(format ?? "yyyy-MM-dd");
        }
        #endregion
    }
}
