using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace EADProject.Models
{
    public static class DBHelper
    {

        private static String connString = @"Data Source=DESKTOP-ERLET40\SQLEXPRESS2016;Initial Catalog=MusicStore;Integrated Security=True";


        public static UsersDTO Validate(String Login, String Password)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                try
                {
                    conn.Open();

                    String sqlQuery = String.Format("Select * from Users where UserName = '{0}' and Password = '{1}'", Login, Password);
                    SqlCommand comm = new SqlCommand(sqlQuery, conn);
                    SqlDataReader reader = comm.ExecuteReader();

                    if (reader.Read() == true)
                    {
                        UsersDTO dto = new UsersDTO();
                        dto.UserID = reader.GetInt32(0);
                        dto.UserName = reader.GetString(1);
                        dto.Password = reader.GetString(2);

                        return dto;
                    }

                    return null;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

        public static Boolean ValidateSignUp(String Login, String Password)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                try
                {
                    conn.Open();

                    String sqlQuery = String.Format("Select * from Users");
                    SqlCommand comm = new SqlCommand(sqlQuery, conn);
                    SqlDataReader reader = comm.ExecuteReader();

                    while (reader.Read() == true)
                    {
                         string UserName = reader.GetString(1);
                        if (Login == UserName)
                            return false;
                        
                    }
                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }

        public static List<SongsDTO> getAllSongs()
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                try
                {
                    conn.Open();

                    String sqlQuery = String.Format("Select * from Songs");
                    SqlCommand comm = new SqlCommand(sqlQuery, conn);
                    SqlDataReader reader = comm.ExecuteReader();

                    List<SongsDTO> list = new List<SongsDTO>();

                    while (reader.Read() == true)
                    {
                        SongsDTO dto = new SongsDTO();
                        dto.SongID = reader.GetInt32(0);
                        dto.Name = reader.GetString(1);
                        dto.Singer = reader.GetString(2);
                        dto.Link = reader.GetString(3);

                        list.Add(dto);
                    }

                    return list;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

        public static List<SongsDTO> getRockSongs()
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                try
                {
                    conn.Open();

                    String sqlQuery = String.Format("Select * from Songs Where Category like '{0}'", "Rock");
                    SqlCommand comm = new SqlCommand(sqlQuery, conn);
                    SqlDataReader reader = comm.ExecuteReader();

                    List<SongsDTO> list = new List<SongsDTO>();

                    while (reader.Read() == true)
                    {
                        SongsDTO dto = new SongsDTO();
                        dto.SongID = reader.GetInt32(0);
                        dto.Name = reader.GetString(1);
                        dto.Singer = reader.GetString(2);
                        dto.Link = reader.GetString(3);

                        list.Add(dto);
                    }

                    return list;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

        public static List<SongsDTO> getPopSongs()
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                try
                {
                    conn.Open();

                    String sqlQuery = String.Format("Select * from Songs Where Category like '{0}'", "Pop");
                    SqlCommand comm = new SqlCommand(sqlQuery, conn);
                    SqlDataReader reader = comm.ExecuteReader();

                    List<SongsDTO> list = new List<SongsDTO>();

                    while (reader.Read() == true)
                    {
                        SongsDTO dto = new SongsDTO();
                        dto.SongID = reader.GetInt32(0);
                        dto.Name = reader.GetString(1);
                        dto.Singer = reader.GetString(2);
                        dto.Link = reader.GetString(3);

                        list.Add(dto);
                    }

                    return list;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

        public static List<SongsDTO> getJazzSongs()
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                try
                {
                    conn.Open();

                    String sqlQuery = String.Format("Select * from Songs Where Category like '{0}'", "Jazz");
                    SqlCommand comm = new SqlCommand(sqlQuery, conn);
                    SqlDataReader reader = comm.ExecuteReader();

                    List<SongsDTO> list = new List<SongsDTO>();

                    while (reader.Read() == true)
                    {
                        SongsDTO dto = new SongsDTO();
                        dto.SongID = reader.GetInt32(0);
                        dto.Name = reader.GetString(1);
                        dto.Singer = reader.GetString(2);
                        dto.Link = reader.GetString(3);

                        list.Add(dto);
                    }

                    return list;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

        public static List<SongsDTO> getPlaylistSongs(int id)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                try
                {
                    conn.Open();

                    String sqlQuery = String.Format("Select s.SongID from Songs s join UserSong us on s.SongID = us.SongID  join Users u on u.UserID = us.UserID");
                    SqlCommand comm = new SqlCommand(sqlQuery, conn);
                    SqlDataReader reader = comm.ExecuteReader();

                    List<SongsDTO> list = new List<SongsDTO>();

                    while (reader.Read() == true)
                    {
                        SongsDTO dto = new SongsDTO();
                        int Songid = reader.GetInt32(0);

                        dto = getSongByID(Songid);

                        list.Add(dto);
                    }

                    return list;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

        public static SongsDTO getSongByID(int id)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                try
                {
                    conn.Open();

                    String sqlQuery = String.Format("Select * from Songs where SongID = {0}", id);
                    SqlCommand comm = new SqlCommand(sqlQuery, conn);
                    SqlDataReader reader = comm.ExecuteReader();

                    SongsDTO dto = new SongsDTO();

                    if (reader.Read() == true)
                    {
                        dto.SongID = reader.GetInt32(0);
                        dto.Name = reader.GetString(1);
                        dto.Singer = reader.GetString(2);
                        dto.Link = reader.GetString(3);                        
                    }

                    return dto;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

        public static void updateSong(int id, string name, string singer)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                try
                {
                    conn.Open();

                    String sqlQuery = String.Format("Update Songs set Name = '{0}', Singer = '{1}' where SongID = {2}", name, singer, id);
                    SqlCommand comm = new SqlCommand(sqlQuery, conn);
                    comm.ExecuteNonQuery();

                }
                catch (Exception ex)
                {
                    
                }
            }
        }

        public static void deleteSong(int id)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                try
                {
                    conn.Open();

                    String sqlQuery = String.Format("Delete from Songs where SongID = {0}", id);
                    SqlCommand comm = new SqlCommand(sqlQuery, conn);
                    comm.ExecuteNonQuery();

                }
                catch (Exception ex)
                {

                }
            }
        }

        public static void addSong(string name, string singer, string category, string link)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                try
                {
                    conn.Open();

                    String sqlQuery = String.Format("Insert into Songs (Name, Singer, Link, Category) values('{0}', '{1}', '{2}', '{3}')", name, singer, link, category);
                    SqlCommand comm = new SqlCommand(sqlQuery, conn);
                    comm.ExecuteNonQuery();

                }
                catch (Exception ex)
                {

                }
            }

        }

        public static int addUser(string login, string password)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                try
                {
                    conn.Open();

                    String sqlQuery = String.Format("Insert into Users (UserName, Password) values('{0}', '{1}')", login, password);
                    SqlCommand comm = new SqlCommand(sqlQuery, conn);
                    comm.ExecuteNonQuery();

                    sqlQuery = String.Format("Select UserID from Users where UserName = '{0}'", login);
                    comm = new SqlCommand(sqlQuery, conn);
                    SqlDataReader reader = comm.ExecuteReader();

                    int id = -1;
                    if (reader.Read() == true)
                    {
                        id = reader.GetInt32(0);
                    }

                    return id;

                }
                catch (Exception ex)
                {
                    return -1;
                }
            }

        }


        public static void addUserSong(int uid, int sid)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                try
                {
                    conn.Open();

                    String sqlQuery = String.Format("Insert into UserSong (UserID, SongID) values({0}, {1})", uid, sid);
                    SqlCommand comm = new SqlCommand(sqlQuery, conn);
                    comm.ExecuteNonQuery();

                }
                catch (Exception ex)
                {

                }
            }

        }

        public static void removeUserSong(int uid, int sid)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                try
                {
                    conn.Open();

                    String sqlQuery = String.Format("Delete from UserSong Where UserID = {0} and SongID = {1}", uid, sid);
                    SqlCommand comm = new SqlCommand(sqlQuery, conn);
                    comm.ExecuteNonQuery();

                }
                catch (Exception ex)
                {

                }
            }

        }
    }
}