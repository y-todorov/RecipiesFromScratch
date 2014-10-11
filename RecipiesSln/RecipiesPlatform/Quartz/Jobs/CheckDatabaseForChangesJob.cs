using System;
using System.Data.SqlClient;
using Quartz;
using RecipiesModelNS;

//using RecipiesWebFormApp.Caching;

namespace RecipiesWebFormApp.Quartz.Jobs
{
    public class CheckDatabaseForChangesJob : JobBase
    {
        private static SqlConnection sqlConn;
        private static SqlCommand command;

        private static DateTime? lastProductChangeDate;

        static CheckDatabaseForChangesJob()
        {
            sqlConn = new SqlConnection(ContextFactory.Current.Database.Connection.ConnectionString);
            command = new SqlCommand();
            command.Connection = sqlConn;
        }

        public override void Execute(IJobExecutionContext context)
        {
            // This code throws exceptions - there is already open reader associated with this command
            /*
            // Entire database 
            command.CommandText = @"SELECT  max(last_user_update) last_user_update
FROM sys.dm_db_index_usage_stats
WHERE database_id = DB_ID( 'recipies')";
            try
            {
                if (command.Connection.State != System.Data.ConnectionState.Open)
                {
                    sqlConn = new SqlConnection(ContextFactory.Current.Database.Connection.ConnectionString);
                    command.Connection = sqlConn;
                    command.Connection.Open();
                }

                if (command.Connection.State != System.Data.ConnectionState.Open)
                {
                    return;
                }

                object rowDate = command.ExecuteScalar();
                if (rowDate == DBNull.Value)
                {
                    return;
                }
            
                DateTime date = (DateTime)rowDate;
                if (!lastProductChangeDate.HasValue)
                {
                    lastProductChangeDate = date;
                }
                else
                {
                    if (lastProductChangeDate.GetValueOrDefault() != date)
                    {
                        lastProductChangeDate = date;
                        //MyCacheManager.Instance.RemoveItems();
                        
                       
                    }
                }
            }
            catch (Exception)
            {
            }
            // do not call this for now
            //base.Execute(context);
             */
        }
    }
}