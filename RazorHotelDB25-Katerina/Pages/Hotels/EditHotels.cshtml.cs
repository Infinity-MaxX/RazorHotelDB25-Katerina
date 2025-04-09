using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorHotelDB25_Katerina.Interfaces;
using RazorHotelDB25_Katerina.Models;

namespace RazorHotelDB25_Katerina.Pages.Hotels
{
    public class EditHotelsModel : PageModel
    {
        #region Instances
        private IHotelService _hotelService;
        #endregion

        #region Properties
        [BindProperty]
        public Hotel Hotel { get; set; }
        #endregion

        #region Constructor
        public EditHotelsModel(IHotelService hotelService)
        {
            _hotelService = hotelService;
        }
        #endregion

        #region Methods
        public async Task OnGetAsync(int id)
        {
            Hotel = await _hotelService.GetHotelFromIdAsync(id);
        }
        public async Task<IActionResult> OnPostAsync()
        {
            await _hotelService.UpdateHotelAsync(Hotel, Hotel.HotelNr);
            return Redirect("ShowBoats");
        }
        #endregion
    }
}
