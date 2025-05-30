using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorHotelDB25_Katerina.Interfaces;
using RazorHotelDB25_Katerina.Models;

namespace RazorHotelDB25_Katerina.Pages.Hotels
{
    public class AddHotelsModel : PageModel
    {
        #region Instances
        private IHotelService _hotelService;
        #endregion

        #region Properties
        [BindProperty]
        public Hotel Hotel { get; set; }
        #endregion

        #region Constructor
        public AddHotelsModel(IHotelService hotelService)
        {
            _hotelService = hotelService;
        }
        #endregion

        #region Methods
        public void OnGet()
        {

        }
        public async Task<IActionResult> OnPostAsync()
        {
            await _hotelService.CreateHotelAsync(new Hotel(Hotel.HotelNr, Hotel.Navn, Hotel.Adresse));
            return RedirectToPage("ShowHotels");
        }
        #endregion
    }
}
