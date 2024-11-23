using System.ComponentModel.DataAnnotations;

namespace Tqmrin2.Models
{
    public class RegisterViewModel
    {
        [StringLength(10)]
        [Required(ErrorMessage = "لطفا فیلد را پر کنید")]
        public string NationalCode { get; set; }

        [StringLength(100)]
        [MinLength(3)]
        [Required(ErrorMessage = "لطفا فیلد را پر کنید")]
        public string Name { get; set; }

        [StringLength(100)]
        [MinLength(3)]
        [Required(ErrorMessage = "لطفا فیلد را پر کنید")]
        public string Username{ get; set; }

        [StringLength(100)]
        [MinLength(8)]
        [Required(ErrorMessage = "لطفا فیلد را پر کنید")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [StringLength(100)]
        [MinLength(8)]
        [Required(ErrorMessage = "لطفا فیلد را پر کنید")]
        [DataType(DataType.Password)]
        [Compare("Password" , ErrorMessage ="رمز ها مطابقت ندارد، دوباره تلاش کنید")]
        public string ConfrimPassword  { get; set; }
        public DateTime CreatedAccount { get; set; }
    }
}
