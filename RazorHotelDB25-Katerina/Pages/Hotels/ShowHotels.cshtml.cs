using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorHotelDB25_Katerina.Interfaces;
using RazorHotelDB25_Katerina.Models;

namespace RazorHotelDB25_Katerina.Pages.Hotels
{
    public class ShowHotelsModel : PageModel
    {
        #region Instances
        private IHotelService _hotelService;
        #endregion

        #region Properties
        public List<Hotel> Hotels { get; set; }
        #endregion

        #region Constructor
        public ShowHotelsModel(IHotelService hotelRepo)
        {
            _hotelService = hotelRepo;
        }
        #endregion

        #region Methods
        public async Task OnGetAsync()
        {
            Hotels = await _hotelService.GetAllHotelAsync();
        }
        #endregion
    }
}
