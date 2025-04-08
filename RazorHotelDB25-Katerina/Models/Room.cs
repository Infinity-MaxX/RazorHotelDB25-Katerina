namespace RazorHotelDB25_Katerina.Models
{
    public class Room
    {
        #region Instances

        #endregion

        #region Properties
        public int RoomNo { get; set; }
        public int HotelNo { get; set; }
        public string Type { get; set; }
        public double Price { get; set; }
        #endregion

        #region Constructor
        public Room(int no, int hotelNo, string type, double price)
        {
            RoomNo = no;
            HotelNo = hotelNo;
            Type = type;
            Price = price;
        }
        #endregion

        #region Methods
        public override string ToString()
        {
            return $"{RoomNo} tilhører hotel nummer: {HotelNo}.\nHar typen: {Type} og koster {Price:0.00} per overnatning";
        }
        #endregion
    }
}
