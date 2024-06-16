using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace TravelInsuranceWebSite.Api.ViewModels
{
    public class UploadImageViewModel
    {
        [Display(Name = "Picture")]
        public IFormFile SpeakerPicture { get; set; }
    }
}
