using System.Configuration;

namespace NumatoRelayHelper
{
    public class RelaySettings
    {
        public static string COMPort {
            get 
            {
                return ConfigurationManager.AppSettings["COMPort"]; 
            }
        }

        public static int OnForSeconds
        {
            get
            {
                int comPort;
                if (int.TryParse(ConfigurationManager.AppSettings["OnForSeconds"], out comPort))
                { return comPort; }
                return 5;
            }
        }
    }
}
