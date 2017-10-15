using System.Configuration;

namespace NumatoRelayHelper
{
    public class RelaySettings
    {
        public static int COMPort {
            get {
                int comPort;
                if (int.TryParse(ConfigurationManager.AppSettings["COMPort"], out comPort))
                { return comPort; }
                return 3;
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
