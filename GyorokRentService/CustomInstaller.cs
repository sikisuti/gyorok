using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;
using System.Windows;
using System.Diagnostics;
using System.Data.SqlClient;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections.Specialized;
using LoggerLib;

namespace GyorokRentService
{
    [RunInstaller(true)]
    public partial class CustomInstaller : System.Configuration.Install.Installer
    {
        public CustomInstaller()
        {
            InitializeComponent();
        }

        private void CustomInstaller_AfterInstall(object sender, InstallEventArgs e)
        {
            try
            {
                if (!EventLog.SourceExists("Totál Szervíz"))
                {
                    EventLog.CreateEventSource("Totál Szervíz", "Application");
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Source + Environment.NewLine + Environment.NewLine + exc.Message + Environment.NewLine + exc.InnerException);
            }

//            try
//            {
//                using (SqlConnection conn = new SqlConnection(@"Data Source=.\sqlexpress;Initial Catalog=master;Integrated Security=SSPI;"))
//                {
//                    string script = @"USE [master]
//GO
//IF NOT EXISTS 
//    (SELECT name  
//        FROM master.sys.server_principals
//        WHERE name = 'gyorok')
//BEGIN
//	CREATE LOGIN [gyorok] WITH PASSWORD=N'gyorok', DEFAULT_DATABASE=[master], DEFAULT_LANGUAGE=[us_english], CHECK_EXPIRATION=OFF, CHECK_POLICY=OFF
//
//	ALTER SERVER ROLE [sysadmin] ADD MEMBER [gyorok]
//END
//GO
//IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'DoRestore')
//DROP PROCEDURE DoRestore
//GO
//CREATE PROCEDURE [dbo].[DoRestore]
//	@path varchar(100)
//AS
//BEGIN
//	-- SET NOCOUNT ON added to prevent extra result sets from
//	-- interfering with SELECT statements.
//	SET NOCOUNT ON;
//
//    -- Insert statements for procedure here
//	IF (EXISTS (SELECT name FROM master.dbo.sysdatabases WHERE ('[' + name + ']' = N'dbGyorok' OR name = N'dbGyorok')))
//	begin
//		alter database dbGyorok set offline with rollback immediate;
//		alter database dbGyorok set online;
//	end
//
//	RESTORE DATABASE dbGyorok FROM DISK = @path WITH RECOVERY,
//		MOVE 'dbGyorok' TO 'c:\Program Files\Microsoft SQL Server\MSSQL11.SQLEXPRESS\MSSQL\DATA\dbGyorok.mdf',
//		MOVE 'dbGyorok_log' TO 'c:\Program Files\Microsoft SQL Server\MSSQL11.SQLEXPRESS\MSSQL\DATA\dbGyorok.ldf';
//END
//GO
//"; // File.ReadAllText(@"DBScripts\CreateUserAndRestoreProc.sql");
//                    string[] batches = Regex.Split(script, @"^(\s)*GO", RegexOptions.Multiline);
//                    StringCollection scl = new StringCollection();
//                    foreach (string batch in batches)
//                    {
//                        if (batch.Trim().Length > 0) scl.Add(batch.Trim());
//                    }

//                    conn.Open();

//                    using (SqlCommand cmd = conn.CreateCommand())
//                    {
//                        foreach (string s in scl)
//                        {
//                            cmd.CommandText = s;
//                            cmd.ExecuteNonQuery();
//                        }
//                    }
//                }
//            }
//            catch (Exception ex)
//            {
//                Logger.Execute.WriteExceptionToLog(ex);
//            }
        }

        private void CustomInstaller_AfterUninstall(object sender, InstallEventArgs e)
        {
            try
            {
                if (EventLog.SourceExists("Totál Szervíz"))
                {
                    EventLog.DeleteEventSource("Totál Szervíz");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Source + Environment.NewLine + Environment.NewLine + ex.Message + Environment.NewLine + ex.InnerException);
            }
        }

        
    }
}
