using Microsoft.Data.SqlClient;
using RazorHotelDB25_Katerina.Helpers;
using RazorHotelDB25_Katerina.Interfaces;
using RazorHotelDB25_Katerina.Models;
using System.Data;

namespace RazorHotelDB25_Katerina.Services
{
    public class HotelService : IHotelService
    {
        #region Instances
        public List<Hotel> _hoteller;
        private string queryString = "SELECT Hotel_No, Name, Address FROM Hotel";
        private string insertSql = "INSERT INTO Hotel Values(@ID, @Navn, @Adresse)";
        private string deleteSql = "DELETE FROM Hotel WHERE Hotel_No = @ID";
        private string updateSql = "UPDATE Hotel SET Name = @Name, Address = @Address WHERE Hotel_No = @ID";
        private string connectionString = ConnectionManager.Connection;
        #endregion

        #region Constructor
        public HotelService()
        {
            _hoteller = GetAllHotel();
        }
        #endregion

        #region Methods
        public List<Hotel> GetAllHotel()
        {
            List<Hotel> hoteller = new List<Hotel>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(queryString, connection);
                    command.Connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        int hotelNr = reader.GetInt32("Hotel_No");
                        string hotelNavn = reader.GetString("Name");
                        string hotelAdr = reader.GetString("Address");
                        Hotel hotel = new Hotel(hotelNr, hotelNavn, hotelAdr);
                        hoteller.Add(hotel);
                    }
                    reader.Close();
                }
                catch (SqlException sqlExp)
                {
                    Console.WriteLine("Database error" + sqlExp.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Generel fejl: " + ex.Message);
                }
                finally
                {

                }
            }
            return hoteller;
        }

        public Hotel? GetHotelFromId(int hotelNr)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                Hotel hotel = null;

                try
                {
                    SqlCommand command = new SqlCommand(queryString + " WHERE hotel_no = @ID", connection);
                    command.Parameters.AddWithValue("@ID", hotelNr);
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        int hNr = reader.GetInt32("Hotel_No");
                        string hotelNavn = reader.GetString("Name");
                        string hotelAdr = reader.GetString("Address");
                        hotel = new Hotel(hNr, hotelNavn, hotelAdr);
                    }
                    reader.Close();
                }
                catch (SqlException sqlExp)
                {
                    Console.WriteLine("Database error" + sqlExp.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Generel fejl: " + ex.Message);
                }
                finally
                {

                }
                return hotel;
            }
        }

        public bool CreateHotel(Hotel hotel)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(insertSql, connection);
                    command.Parameters.AddWithValue("@ID", hotel.HotelNr);
                    command.Parameters.AddWithValue("@Navn", hotel.Navn);
                    command.Parameters.AddWithValue("@Adresse", hotel.Adresse);
                    connection.Open();
                    command.ExecuteNonQuery();

                    /*
                    if (!_hoteller.Contains(hotel))
                    {
                        _hoteller.Add(hotel);
                    }
                    return false;
                    */
                }
                catch (SqlException sqlExp)
                {
                    Console.WriteLine("Database error" + sqlExp.Message);
                    return false;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Generel fejl: " + ex.Message);
                    return false;
                }
                finally
                {

                }
                return true;
            }
        }

        public bool UpdateHotel(Hotel hotel, int hotelNr)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand(updateSql, connection);
                    cmd.Parameters.AddWithValue("@ID", hotelNr);
                    cmd.Parameters.AddWithValue("@Name", hotel.Navn);
                    cmd.Parameters.AddWithValue("@Address", hotel.Adresse);
                    connection.Open();
                    cmd.ExecuteNonQuery();
                }
                catch (SqlException sqlExp)
                {
                    Console.WriteLine("Database error" + sqlExp.Message);
                    return false;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Generel fejl: " + ex.Message);
                    return false;
                }
                finally
                {

                }
                return true;
            }
        }
        public Hotel DeleteHotel(int hotelNr)
        { // kan ikke fjerne hotel med værelser

            Hotel? hotel = GetHotelFromId(hotelNr);

            if (hotel == null) { return null; }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(deleteSql, connection);
                    // command.Parameters.Remove(GetHotelFromId(hotelNr));
                    command.Parameters.AddWithValue("@ID", hotelNr);
                    connection.Open();

                    int numberOfRows = command.ExecuteNonQuery();
                    if (numberOfRows == 0) { return null; }
                }
                catch (SqlException sqlExp)
                {
                    Console.WriteLine("Database error" + sqlExp.Message);
                    hotel = null;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Generel fejl: " + ex.Message);
                    hotel = null;
                }
                finally
                {

                }
                return hotel;
            }
        }
        public List<Hotel> GetHotelsByName(string name)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                List<Hotel> hoteller = new List<Hotel>();
                try
                {
                    SqlCommand command = new SqlCommand(queryString + " where Name like @Search", connection);
                    command.Parameters.AddWithValue("@Search", "%" + name + "%");
                    command.Connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read()) // reads from data not from console
                    {
                        int hotelNr = reader.GetInt32("Hotel_No");
                        string hotelNavn = reader.GetString("Name");
                        string hotelAdr = reader.GetString("Address");
                        Hotel hotel = new Hotel(hotelNr, hotelNavn, hotelAdr);
                        hoteller.Add(hotel);
                    }
                    reader.Close();
                }
                catch (SqlException sqlExp)
                {
                    Console.WriteLine("Database error" + sqlExp.Message);

                }
                catch (Exception ex)
                {
                    Console.WriteLine("Generel fejl: " + ex.Message);

                }
                finally { }
                return hoteller;
            }
        }
        #endregion
    }
}
