namespace appt.Storage.Models
{
    public class AppConfiguration
    {
        public AppConfiguration(string costCenter)
        {
            CostCenter = costCenter;
        }
        public string CostCenter { get; private set; }
    }
}
