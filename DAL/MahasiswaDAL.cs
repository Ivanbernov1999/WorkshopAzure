using System.Collections.Generic;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using WorkshopASPCore21.Models;
using System;

namespace WorkshopASPCore21.DAL
{
    public class MahasiswaDAL : IMahasiswa
    {
        private IConfiguration _config;
        public MahasiswaDAL(IConfiguration config)
        {
            _config = config;
        }

        private string GetConnStr(){
            return _config.GetConnectionString("DefaultConnection");
        }

        public void Delete(string id)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Mahasiswa> GetAll()
        {
            List<Mahasiswa> lstMhs = new List<Mahasiswa>();
            using(SqlConnection conn = new SqlConnection(GetConnStr())){
                string strSql = @"select * from Mahasiswa1 
                                  order by Nama asc";
                SqlCommand cmd = new SqlCommand(strSql,conn);
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if(dr.HasRows){
                    while(dr.Read()){
                        lstMhs.Add(new Mahasiswa{
                            Nim = dr["Nim"].ToString(),
                            Nama = dr["Nama"].ToString(),
                            IPK = Convert.ToDouble(dr["IPK"]),
                            Email = dr["Email"].ToString()
                        });
                    }
                }
                dr.Close();
                cmd.Dispose();
                conn.Close();

                return lstMhs;
            }
        }

        public Mahasiswa GetById(string id)
        {
           Mahasiswa mhs = new Mahasiswa();
            using(SqlConnection conn = new SqlConnection(GetConnStr())){
                string strSql = @"select * from Mahasiswa1
                                  where Nim=@Nim";
                SqlCommand cmd = new SqlCommand(strSql,conn);
                cmd.Parameters.AddWithValue("@Nim",id);
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if(dr.HasRows){
                    while(dr.Read()){
                        mhs.Nim = dr["Nim"].ToString();
                        mhs.Nama = dr["Nama"].ToString();
                        mhs.IPK = Convert.ToDouble(dr["IPK"]);
                        mhs.Email = dr["Email"].ToString();
                    }
                }
                dr.Close();
                cmd.Dispose();
                conn.Close();
            }
            return mhs;
        }

        public void Insert(Mahasiswa mhs)
        {
            using(SqlConnection conn = new SqlConnection(GetConnStr())){
                string strSql = @"insert into Mahasiswa1(Nim, Nama, IPK, Email)
                                values(@Nim, @Nama, @IPK, @Email)";
                SqlCommand cmd = new SqlCommand(strSql,conn);
                mcd.Parameters.AddWithValue("@Nim",mhs.Nim);
                mcd.Parameters.AddWithValue("@Nama",mhs.Nama);
                mcd.Parameters.AddWithValue("@IPK",mhs.IPK);
                mcd.Parameters.AddWithValue("@Email",mhs.Email);
                try{
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }catch(SqlException sqlEx){
                    
                }
            }
        }

        public void Update(Mahasiswa mhs)
        {
            using(SqlConnection conn = new SqlConnection(GetConnStr())){
                
            }
            
        }
    }
}