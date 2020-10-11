using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Configuration;

using MysqlConnect.Models;
using Renci.SshNet.Security.Cryptography;

namespace MysqlConnect.Services
{
    public class DBService
    {
        
        MySqlConnection myconnect = new MySqlConnection(ConfigurationManager.ConnectionStrings["mysqlDbconnection"].ConnectionString);

        public string checkConnection()
        {
            string value = "";
            try
            {
                myconnect.Open();
                value = "Connected to DB";
                myconnect.Clone();
            }
            catch (Exception)
            {
                value = "Not Connected";
            }
            

            return value;
        }

        public string addDataUsingQuery(Registeration reg)
        {
            string value = "";
            
                myconnect.Open();
            string query = "insert into tbl_Register(userName,empid,mobile,email,dateofJoin,createdDate,statuss) values ('"+ reg.userName+ "','"+reg.empid+"','"+reg.mobile+"','"+reg.email+"','"+reg.dateofJoin+"',curdate(),'i')";
            MySqlCommand cmd = new MySqlCommand(query, myconnect);
            cmd.ExecuteNonQuery();
            myconnect.Clone();
            value = "Data Added";

            return value;
        }
        public string updateDataUsingQuery(Registeration reg)
        {
            string value = "";

            myconnect.Open();
            string query = "update tbl_Register set userName='" + reg.userName + "',empid='" + reg.empid + "',mobile='" + reg.mobile + "',email='" + reg.email + "' where sno="+reg.sno;
            MySqlCommand cmd = new MySqlCommand(query, myconnect);
            cmd.ExecuteNonQuery();
            myconnect.Clone();
            value = "Data updated";

            return value;
        }
        public IList<Registeration> getRecordsOfUser()
        {
            IList<Registeration> data = new List<Registeration>();
            try
            {
                myconnect.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT sno, userName, empid, mobile, email, dateofJoin, createdDate, statuss FROM tbl_Register", myconnect);
                MySqlDataReader reader = cmd.ExecuteReader();
                while(reader.Read())
                {
                    Registeration reg = new Registeration();
                    reg.sno = int.Parse(reader[0].ToString());
                    reg.userName = reader[1].ToString();
                    reg.empid = reader[2].ToString();
                    reg.mobile = reader[3].ToString();
                    reg.email = reader[4].ToString();
                    reg.dateofJoin = reader[5].ToString();
                    reg.createdDate = reader[6].ToString();
                    reg.statuss = reader[7].ToString();
                    data.Add(reg);

                }
                reader.Close();
                myconnect.Close();
            }
            catch (Exception)
            {

                throw;
            }


            return data;
        }
        public Registeration getRecordsOfUserById(int sno)
        {
            Registeration reg = new Registeration();
            try
            {
                myconnect.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT sno, userName, empid, mobile, email, dateofJoin, createdDate, statuss FROM tbl_Register where sno="+sno.ToString(), myconnect);
                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                   
                    reg.sno = int.Parse(reader[0].ToString());
                    reg.userName = reader[1].ToString();
                    reg.empid = reader[2].ToString();
                    reg.mobile = reader[3].ToString();
                    reg.email = reader[4].ToString();
                    reg.dateofJoin = reader[5].ToString();
                    reg.createdDate = reader[6].ToString();
                    reg.statuss = reader[7].ToString();
                   

                }
                reader.Close();
                myconnect.Close();
            }
            catch (Exception)
            {

                throw;
            }


            return reg;
        }

        public void deleteRecordsFromSP(int sno)
        {
            try
            {
                myconnect.Open();
                MySqlCommand cmd = new MySqlCommand("SP_DeleteRecord", myconnect);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", sno);
                cmd.ExecuteNonQuery();
                myconnect.Close();
            }
            catch (Exception e)
            {
                
            }
        }
    }
}