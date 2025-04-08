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
            
        }
        #endregion

        #region Methods
        async public Task<List<Hotel>> GetAllHotelAsync()
        {
            List<Hotel> hoteller = new List<Hotel>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(queryString, connection);
                    await command.Connection.OpenAsync();
                    SqlDataReader reader = await command.ExecuteReaderAsync();
                    while (await reader.ReadAsync())
                    {
                        int hotelNr = reader.GetInt32("Hotel_No");
                        string hotelNavn = reader.GetString("Name");
                        string hotelAdr = reader.GetString("Address");
                        Hotel hotel = new Hotel(hotelNr, hotelNavn, hotelAdr);
                        hoteller.Add(hotel);
                    }
                    await reader.CloseAsync();
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

        public async Task<Hotel?> GetHotelFromIdAsync(int hotelNr)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                Hotel hotel = null;

                try
                {
                    SqlCommand command = new SqlCommand(queryString + " WHERE hotel_no = @ID", connection);
                    command.Parameters.AddWithValue("@ID", hotelNr);
                    await connection.OpenAsync();
                    SqlDataReader reader = await command.ExecuteReaderAsync();

                    if (await reader.ReadAsync())
                    {
                        int hNr = reader.GetInt32("Hotel_No");
                        string hotelNavn = reader.GetString("Name");
                        string hotelAdr = reader.GetString("Address");
                        hotel = new Hotel(hNr, hotelNavn, hotelAdr);
                    }
                    await reader.CloseAsync();
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
        public Task<Hotel> DeleteHotelAsync(int hotelNr)
        { // kan ikke fjerne hotel med værelser

            Task<Hotel?> hotel = GetHotelFromIdAsync(hotelNr);

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
        public async Task<List<Hotel>> GetHotelsByNameAsync(string name)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                List<Hotel> hoteller = new List<Hotel>();
                try
                {
                    SqlCommand command = new SqlCommand(queryString + " where Name like @Search", connection);
                    command.Parameters.AddWithValue("@Search", "%" + name + "%");
                    await command.Connection.OpenAsync();
                    SqlDataReader reader = await command.ExecuteReaderAsync();
                    while (await reader.ReadAsync()) // reads from data not from console
                    {
                        int hotelNr = reader.GetInt32("Hotel_No");
                        string hotelNavn = reader.GetString("Name");
                        string hotelAdr = reader.GetString("Address");
                        Hotel hotel = new Hotel(hotelNr, hotelNavn, hotelAdr);
                        hoteller.Add(hotel);
                    }
                    await reader.CloseAsync();
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
