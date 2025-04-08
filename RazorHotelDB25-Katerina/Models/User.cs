namespace RazorHotelDB25_Katerina.Models
{
    public class User
    {
        #region Instances
        private string _userName;
        private string _hashCode;
        #endregion

        #region Constructor
        public User(string user, string hash)
        {
            _userName = user;
            _hashCode = hash;
        }
        #endregion
    }
}
