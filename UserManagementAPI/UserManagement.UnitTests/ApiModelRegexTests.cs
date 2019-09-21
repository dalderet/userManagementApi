namespace UserManagement.UnitTests
{
    using Xunit;
    using System;
    using System.Text.RegularExpressions;

    public class ApiModelRegexTests
    {
        private readonly Regex NameRegex = new Regex("^[a-zA-Z]+(([',. -][a-zA-Z ])?[a-zA-Z]*)*$");
        private readonly Regex BirthDateRegex = new Regex(@"^([012]\d|30|31)-(0\d|10|11|12)-\d{4}$");
        private const string MyName = "Daniel Alderete Merino";
        private const string BadNameExample = "3Daniel Alderete Merino";
        private readonly DateTime MyBirhtDate = new DateTime(1990, 11, 30);

        [Fact]
        public void TestNameRegex()
        {
            Assert.Matches(NameRegex, MyName);
            Assert.DoesNotMatch(NameRegex, BadNameExample);
        }

        [Fact]
        public void TestBirthDateRegex()
        {
            var myBirthDateString = MyBirhtDate.ToString("dd-MM-yyyy");
            Assert.Matches(BirthDateRegex, myBirthDateString);
            var badFormatDateString = MyBirhtDate.ToString("dd/MM/yyyy");
            Assert.DoesNotMatch(BirthDateRegex, badFormatDateString);
        }
    }
}
