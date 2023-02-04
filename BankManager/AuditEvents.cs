using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace BankManager
{
    public enum AuditEventTypes
    {
        OtvoriRacunSuccess = 0,
        OtvoriRacunFailure = 1,
        ZatvoriRacunSuccess = 2,
        ZatvoriRacunFailure = 3,
        ProveriStanjeSuccess = 4,
        ProveriStanjeFailure = 5,
        UplataSuccess = 6,
        UplataFailure = 7,
        IsplataSuccess = 8,
        IsplataFailure = 9,
        OpomenaSuccess = 10,
        OpomenaFailure = 11
    }
    public class AuditEvents
    {
        private static ResourceManager resourceManager = null;
        private static object resourceLock = new object();

        private static ResourceManager ResourceMgr
        {
            get
            {
                lock (resourceLock)
                {
                    if(resourceManager == null)
                    {
                        resourceManager = new ResourceManager(typeof(AuditEventFile).ToString(),
                            Assembly.GetExecutingAssembly());
                    }
                    return resourceManager;
                }
            }
        }

        public static string OtvoriRacunSuccess
        {
            get
            {
                return ResourceMgr.GetString(AuditEventTypes.OtvoriRacunSuccess.ToString());
            }
        }

        public static string OtvoriRacunFailure
        {
            get
            {
                return ResourceMgr.GetString(AuditEventTypes.OtvoriRacunFailure.ToString());
            }
        }

        public static string ZatvoriRacunSuccess
        {
            get
            {
                return ResourceMgr.GetString(AuditEventTypes.ZatvoriRacunSuccess.ToString());
            }
        }

        public static string ZatvoriRacunFailure
        {
            get
            {
                return ResourceMgr.GetString(AuditEventTypes.ZatvoriRacunFailure.ToString());
            }
        }

        public static string ProveriStanjeSuccess
        {
            get
            {
                return ResourceMgr.GetString(AuditEventTypes.ProveriStanjeSuccess.ToString());
            }
        }

        public static string ProveriStanjeFailure
        {
            get
            {
                return ResourceMgr.GetString(AuditEventTypes.ProveriStanjeFailure.ToString());
            }
        }

        public static string UplataSuccess
        {
            get
            {
                return ResourceMgr.GetString(AuditEventTypes.UplataSuccess.ToString());
            }
        }

        public static string UplataFailure
        {
            get
            {
                return ResourceMgr.GetString(AuditEventTypes.UplataFailure.ToString());
            }
        }

        public static string IsplataSuccess
        {
            get
            {
                return ResourceMgr.GetString(AuditEventTypes.IsplataSuccess.ToString());
            }
        }

        public static string IsplataFailure
        {
            get
            {
                return ResourceMgr.GetString(AuditEventTypes.IsplataFailure.ToString());
            }
        }

        public static string OpomenaSuccess
        {
            get
            {
                return ResourceMgr.GetString(AuditEventTypes.OpomenaSuccess.ToString());
            }
        }

        public static string OpomenaFailure
        {
            get
            {
                return ResourceMgr.GetString(AuditEventTypes.OpomenaFailure.ToString());
            }
        }

    }
}
