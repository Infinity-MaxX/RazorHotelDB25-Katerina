using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorHotelDB25_Katerina.Interfaces;
using RazorHotelDB25_Katerina.Models;

namespace RazorHotelDB25_Katerina.Pages.Rooms
{
    public class ShowRoomsModel : PageModel
    {
        #region Instances
        private IRoomService _roomService;
        private IHotelService _hotelService;
        #endregion

        #region Properties
        public List<Room> Rooms { get; set; }
        public Hotel Hotel { get; set; }
        #endregion

        #region Constructor
        public ShowRoomsModel(IRoomService roomService, IHotelService hotelService)
        {
            _roomService = roomService;
            _hotelService = hotelService;
        }
        #endregion

        #region Methods
        public async Task OnGetAsync()
        {
            Rooms = await _roomService.GetAllRoomAsync();
        }

        public async Task OnGetWHotel(int HotelNo)
        {
            Hotel = await _hotelService.GetHotelFromIdAsync(HotelNo);
            Rooms = await _roomService.GetAllRoomInHotelAsync(HotelNo);
        }

        /// <summary>
        /// A delete function as one does not need an extra page to delete a hotel.
        /// </summary>
        public async Task<IActionResult> OnPostDelete(int roomNo, int hotelNo)
        {
            await _roomService.DeleteRoomAsync(roomNo, hotelNo);
            return RedirectToPage("ShowRooms");
        }
        #endregion
    }
}
