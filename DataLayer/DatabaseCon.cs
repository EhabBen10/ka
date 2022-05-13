using System;
using System.Data.SqlClient;
using DTO;
using System.Linq;

namespace DataLayer
{
    public class DatabaseCon
    {

        private SqlConnection sqlConnection;

        private string connectionstring = "Data Source=172.20.10.5\\SQLEXPRESS;Initial Catalog=EKGData;User ID=ehab;Password=vym39ejx;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        public DatabaseCon()
        {
            sqlConnection = new SqlConnection(connectionstring);
        }
        public void InsertToDataBase(DTO_EKGmaaling eKGmaaling)
        {
            //string str = "Data Source=172.20.10.5\\SQLEXPRESS;Initial Catalog=EKGData;User ID=ehab;Password=vym39ejx;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

            //SqlConnection sqlConnection = new SqlConnection(str);

            string insertStringParam = $"INSERT INTO Patients (CPR,Tid,EkgData,SampleRate) output inserted.MalId VALUES(@cpr, @startTid, @ekgdata, @samplerate)";
            //string query = "INSERT INTO Patients (CPR,Tid,EkgData,SampleRate) VALUES (@cpr,@startTid,@ekgdata,@samplerate)";
            using (SqlCommand sqlCommand = new SqlCommand(insertStringParam, sqlConnection))
            {
                sqlCommand.Parameters.AddWithValue("@cpr", eKGmaaling.CPR);
                sqlCommand.Parameters.AddWithValue("@startTid", eKGmaaling.StartTid);
                sqlCommand.Parameters.AddWithValue("@ekgdata", eKGmaaling.EkgData.SelectMany(value =>
                BitConverter.GetBytes(value)).ToArray());
                sqlCommand.Parameters.AddWithValue("@samplerate", eKGmaaling.SampleRate);
                sqlConnection.Open();
                sqlCommand.ExecuteNonQuery();
            }



        }
        /// <summary>
        /// Fortæller mID til den aller sidste række i data basen.
        /// </summary>
        /// <returns>Returnerer værdien for mId på næstkommende entry.</returns>
        public int CountId()
        {
            int mID;

            SqlCommand sqlCommand = new SqlCommand("Select Count(*) from Patients", sqlConnection);
            //sqlConnection.Open();

            mID = (int)sqlCommand.ExecuteScalar();
            sqlConnection.Close();

            return mID;
        }

    }
}
