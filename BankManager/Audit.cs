using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankManager
{
    public class Audit : IDisposable
    {
        public static EventLog customLog = null;
        const string SourceName = "Manager.Audit";
        const string LogName = "AuditSBES";

        static Audit()
        {
            try
            {
                if (!EventLog.SourceExists(SourceName))
                {
                    EventLog.CreateEventSource(SourceName, LogName);
                }
                customLog = new EventLog(LogName, Environment.MachineName, SourceName);
            }
            catch(Exception e)
            {
                customLog = null;
                Console.WriteLine("Error while trying to create log handle. Error = {0}", e.Message);
            }
        }

        public static void OtvoriRacunSuccess(string username)
        {
            if(customLog != null)
            {
                string otvoriRacunSuccess = AuditEvents.OtvoriRacunSuccess;
                string message = String.Format(otvoriRacunSuccess, username);
                customLog.WriteEntry(message, EventLogEntryType.Information, (int)AuditEventTypes.OtvoriRacunSuccess);
            }
            else
            {
                throw new ArgumentException(string.Format("Error while trying to write event (eventid = {0}) to event log.",
                    (int)AuditEventTypes.OtvoriRacunSuccess));
            }
        }
     
        public static void OtvoriRacunFailure(string username, string reason)
        {
            if(customLog != null)
            {
                string otvoriRacunFailure = AuditEvents.OtvoriRacunFailure;
                string message = String.Format(otvoriRacunFailure, username, reason);
                customLog.WriteEntry(message, EventLogEntryType.FailureAudit, (int)AuditEventTypes.OtvoriRacunFailure);
            }
            else
            {
                throw new ArgumentException(string.Format("Error while trying to write event (eventid = {0}) to event log.",
                    (int)AuditEventTypes.OtvoriRacunFailure));
            }
        }
        public static void ZatvoriRacunSuccess(long broj)
        {
            if (customLog != null)
            {
                string zatvoriRacunSuccess = AuditEvents.ZatvoriRacunSuccess;
                string message = String.Format(zatvoriRacunSuccess, broj.ToString());
                customLog.WriteEntry(message, EventLogEntryType.Information, (int)AuditEventTypes.ZatvoriRacunSuccess);
            }
            else
            {
                throw new ArgumentException(string.Format("Error while trying to write event (eventid = {0}) to event log.",
                    (int)AuditEventTypes.ZatvoriRacunSuccess));
            }
        }

        public static void ZatvoriRacunFailure(long broj, string reason)
        {
            if (customLog != null)
            {
                string zatvoriRacunFailure = AuditEvents.OtvoriRacunFailure;
                string message = String.Format(zatvoriRacunFailure, broj.ToString(), reason);
                customLog.WriteEntry(message, EventLogEntryType.FailureAudit, (int)AuditEventTypes.ZatvoriRacunFailure);
            }
            else
            {
                throw new ArgumentException(string.Format("Error while trying to write event (eventid = {0}) to event log.",
                    (int)AuditEventTypes.ZatvoriRacunFailure));
            }
        }
        public static void ProveriStanjeSuccess(long broj)
        {
            if (customLog != null)
            {
                string proveriStanjeSuccess = AuditEvents.ProveriStanjeSuccess;
                string message = String.Format(proveriStanjeSuccess, broj.ToString());
                customLog.WriteEntry(message, EventLogEntryType.Information, (int)AuditEventTypes.ProveriStanjeSuccess);
            }
            else
            {
                throw new ArgumentException(string.Format("Error while trying to write event (eventid = {0}) to event log.",
                    (int)AuditEventTypes.ProveriStanjeSuccess));
            }
        }

        public static void ProveriStanjeFailure(long broj, string reason)
        {
            if (customLog != null)
            {
                string proveriStanjeFailure = AuditEvents.ProveriStanjeFailure;
                string message = String.Format(proveriStanjeFailure, broj.ToString(), reason);
                customLog.WriteEntry(message, EventLogEntryType.FailureAudit, (int)AuditEventTypes.ProveriStanjeFailure);
            }
            else
            {
                throw new ArgumentException(string.Format("Error while trying to write event (eventid = {0}) to event log.",
                    (int)AuditEventTypes.ProveriStanjeFailure));
            }
        }

        public static void UplataSuccess(long broj)
        {
            if (customLog != null)
            {
                string uplataSuccess = AuditEvents.UplataSuccess;
                string message = String.Format(uplataSuccess, broj.ToString());
                customLog.WriteEntry(message, EventLogEntryType.Information, (int)AuditEventTypes.UplataSuccess);
            }
            else
            {
                throw new ArgumentException(string.Format("Error while trying to write event (eventid = {0}) to event log.",
                    (int)AuditEventTypes.UplataSuccess));
            }
        }

        public static void UplataFailure(long broj, string reason)
        {
            if (customLog != null)
            {
                string uplataFailure = AuditEvents.UplataFailure;
                string message = String.Format(uplataFailure, broj.ToString(), reason);
                customLog.WriteEntry(message, EventLogEntryType.FailureAudit, (int)AuditEventTypes.UplataFailure);
            }
            else
            {
                throw new ArgumentException(string.Format("Error while trying to write event (eventid = {0}) to event log.",
                    (int)AuditEventTypes.UplataFailure));
            }
        }
        public static void IsplataSuccess(long broj)
        {
            if (customLog != null)
            {
                string isplataSuccess = AuditEvents.IsplataSuccess;
                string message = String.Format(isplataSuccess, broj.ToString());
                customLog.WriteEntry(message, EventLogEntryType.Information, (int)AuditEventTypes.IsplataSuccess);
            }
            else
            {
                throw new ArgumentException(string.Format("Error while trying to write event (eventid = {0}) to event log.",
                    (int)AuditEventTypes.IsplataSuccess));
            }
        }

        public static void IsplataFailure(long broj, string reason)
        {
            if (customLog != null)
            {
                string isplataFailure = AuditEvents.IsplataFailure;
                string message = String.Format(isplataFailure, broj.ToString(), reason);
                customLog.WriteEntry(message, EventLogEntryType.FailureAudit, (int)AuditEventTypes.IsplataFailure);
            }
            else
            {
                throw new ArgumentException(string.Format("Error while trying to write event (eventid = {0}) to event log.",
                    (int)AuditEventTypes.IsplataFailure));
            }
        }

        public static void OpomenaSuccess(long broj)
        {
            if (customLog != null)
            {
                string opomenaSuccess = AuditEvents.OpomenaSuccess;
                string message = String.Format(opomenaSuccess, broj.ToString());
                customLog.WriteEntry(message, EventLogEntryType.Information, (int)AuditEventTypes.OpomenaSuccess);
            }
            else
            {
                throw new ArgumentException(string.Format("Error while trying to write event (eventid = {0}) to event log.",
                    (int)AuditEventTypes.OpomenaSuccess));
            }
        }

        public static void OpomenaFailure(long broj, string reason)
        {
            if (customLog != null)
            {
                string opomenaFailure = AuditEvents.OpomenaFailure;
                string message = String.Format(opomenaFailure, broj.ToString(), reason);
                customLog.WriteEntry(message, EventLogEntryType.FailureAudit, (int)AuditEventTypes.OpomenaFailure);
            }
            else
            {
                throw new ArgumentException(string.Format("Error while trying to write event (eventid = {0}) to event log.",
                    (int)AuditEventTypes.OpomenaFailure));
            }
        }

        public void Dispose()
        {
            if (customLog != null)
            {
                customLog.Dispose();
                customLog = null;
            }
        }
    }
}
