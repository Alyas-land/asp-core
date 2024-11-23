using System.ComponentModel.DataAnnotations;

namespace Tqmrin2.Models
{
    public class User
    {
        [Key]
        [Required(ErrorMessage = "لطفا این فیلد را پر کنید")]
        [RegularExpression(@"\d{10}", ErrorMessage = "کد ملی باید 10 رقم باشد.")]
        public string NationalId { get; set; }

        [Required(ErrorMessage = "لطفا این فیلد را پر کنید")]
        [StringLength(100, ErrorMessage = "طول نام نا معتبر", MinimumLength = 3)]
        public string Name { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public DateTime CreatedAccount { get; set; }

        public List<BorrowedItems> BorrowedItems { get; set; }

        public User()
        {
            BorrowedItems = new List<BorrowedItems>();
        }
    }
}
