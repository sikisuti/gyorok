using System;
using System.Collections.Generic;
using System.Data.EntityClient;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace SQLConnectionLib
{
    public class DbSettings
    {
        public string ServerIP { get; set; }
        public string ServerInstanceName { get; set; }
        public string primaryBackupPath { get; set; }
        public string secondaryBackupPath { get; set; }

        public string DataSource { get; set; }
        public string InitialCatalog { get; set; }
        public bool IntegratedSecurity { get; set; }
        public bool PersistSecurityInfo { get; set; }
        public bool MultipleActiveResultSets { get; set; }
        public string ApplicationName { get; set; }
        public string MetaData { get; set; }
        public string Provider { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        public DbSettings()
        {
            ServerIP = Properties.Settings.Default.ServerIP;
            ServerInstanceName = Properties.Settings.Default.ServerInstanceName;
            InitialCatalog = Properties.Settings.Default.InitialCatalog;
            IntegratedSecurity = Properties.Settings.Default.IntegratedSecurity;
            PersistSecurityInfo = Properties.Settings.Default.PersistSecurityInfo;
            MultipleActiveResultSets = Properties.Settings.Default.MultipleActiveResultSets;
            ApplicationName = Properties.Settings.Default.App;
            UserName = Properties.Settings.Default.UserName;
            Password = Properties.Settings.Default.Password;
            Provider = Properties.Settings.Default.Provider;
            MetaData = Properties.Settings.Default.MetaData;
        }

        public EntityConnection GetEntityConnection()
        {
            SqlConnectionStringBuilder pConnStr = new SqlConnectionStringBuilder();
            pConnStr.DataSource = ServerIP + @"\" + ServerInstanceName;
            pConnStr.InitialCatalog = InitialCatalog;
            pConnStr.IntegratedSecurity = IntegratedSecurity;
            pConnStr.PersistSecurityInfo = PersistSecurityInfo;
            pConnStr.MultipleActiveResultSets = MultipleActiveResultSets;
            pConnStr.ApplicationName = ApplicationName;
            pConnStr.UserID = UserName;
            pConnStr.Password = Password;

            EntityConnectionStringBuilder sb = new EntityConnectionStringBuilder();
            sb.Provider = Provider;
            sb.ProviderConnectionString = pConnStr.ConnectionString;
            sb.Metadata = MetaData;

            return new EntityConnection(sb.ConnectionString);
        }
    }
}
