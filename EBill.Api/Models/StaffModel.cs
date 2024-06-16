using System.ComponentModel.DataAnnotations;

namespace TravelInsuranceWebSite.Models
{
    public class StaffModel
    {
        public StaffModel()
        {
            StaffID = "";
            staffno = "";
            staffname = "";
            Password = "";
            positionID = "";
            DOB = new DateTime();
            Gender = "";
            NRC = "";
            Salary = 0.00M;
            Phone = "";
            Address = "";
            HireDate = new DateTime();
            TerminateDate = new DateTime();
            locationID = "";
            Sort = 0;
            IsDeleted = false;
            CreatedDate = new DateTime();
            CreatedBy = "";
        }
        [Key]
        public string StaffID { get; set; }
        [Required(ErrorMessage = "Staff No  is required.")]
        public string staffno { get; set; }
        [Required(ErrorMessage = "Staff Name  is required.")]
        public string staffname { get; set; }
        [Required(ErrorMessage = "Password  is required.")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Position  is required.")]
        public string positionID { get; set; }
        [Required(ErrorMessage = "Date Of Birth  is required.")]
        [DataType(DataType.Date)]
        public DateTime DOB { get; set; }
        [Required(ErrorMessage = "Gender  is required.")]
        public string Gender { get; set; }
        public string NRC { get; set; }
        [RegularExpression(@"^\$?([1-9]{1}[0-9]{0,2}(\,[0-9]{3})*(\.[0-9]{0,2})?|[1-9]{1}[0-9]{0,}(\.[0-9]{0,2})?|0(\.[0-9]{0,2})?|(\.[0-9]{1,2})?)$", ErrorMessage = "{0} must be a Number.")]
        [Required(ErrorMessage = "Salary  is required.")]
        public decimal Salary { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        [Required(ErrorMessage = "Hire Date is required.")]
        [DataType(DataType.Date)]
        public DateTime HireDate { get; set; }
        [DataType(DataType.Date)]
        public Nullable<DateTime> TerminateDate { get; set; }
        [Required(ErrorMessage = "Location  is required.")]
        public string locationID { get; set; }
        [Required(ErrorMessage = "Sort  is required.")]
        public int Sort { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
        public int Sr { get; set; }
        public int MessageType { get; set; }
        public string Message { get; set; }
        public string status { get; set; }
        public string locationdescription { get; set; }
        public string locationcode { get; set; }

    }
}
