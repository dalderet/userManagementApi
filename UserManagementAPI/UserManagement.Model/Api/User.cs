namespace UserManagement.Model.Api
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class User
    {
        [RegularExpression(@"^([012]\d|30|31)/(0\d|10|11|12)/\d{4}$", ErrorMessage = "BirthDate does not fit " +
            "with a valid date format")]
        private string _birthDate;

        [Required, Range(1, int.MaxValue, ErrorMessage = "Id value must be and integer greater than 0"), Key]
        public int Id { get; set; }

        [Required, RegularExpression("^[a-zA-Z]+(([',. -][a-zA-Z ])?[a-zA-Z]*)*$", ErrorMessage =
            "Name must only contains letters from A to Z and spaces")]
        public string Name { get; set; }

        [Required(ErrorMessage = "BirthDate does not fit with a valid date format")]
        public DateTime Birthdate
        {
            get => DateTime.Parse(_birthDate);
            set => _birthDate = value.Date.ToShortDateString();
        }
    }
}
