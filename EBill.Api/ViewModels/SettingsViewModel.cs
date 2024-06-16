using System.ComponentModel.DataAnnotations;
using TravelInsuranceWebSite.Api.ViewModels;

namespace TravelInsuranceWebSite.ViewModels
{
    public class SettingsViewModel : EditImageViewModel
    {
        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }

    

    }
}
