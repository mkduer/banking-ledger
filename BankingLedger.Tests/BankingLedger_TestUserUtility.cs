using Xunit;

namespace BankingLedger.UnitTests
{
    public class BankingLedger_TestUserUtility
    {
        [Theory]
        [InlineData("avalidusername", "")]
        [InlineData("a23r8u9ajfhasfmkld*)478", "")]
        [InlineData("!@#$%^&*()_+~`-[]{}|: ;<,>.?/", "")]
        [InlineData("1234567890", "")]
        public void TestValidateUserID_ValidReferenceUserID(string id, string refUserID)
        {
            Assert.True(UserUtility.validateUserID(id, ref refUserID));
            Assert.Equal(id, refUserID);
        }

        [Theory]
        [InlineData("", "")]
        public void TestValidateUserID_InvalidEmptyStringId(string id, string refUserID)
        {
            Assert.False(UserUtility.validateUserID(id, ref refUserID));
            Assert.Equal(id, refUserID);
        }

        [Theory]
        [InlineData(null, "")]
        public void TestValidateUserID_InvalidNullStringId(string id, string refUserID)
        {
            Assert.False(UserUtility.validateUserID(id, ref refUserID));
            Assert.NotEqual(id, refUserID);
        }

        [Theory]
        [InlineData("Oprah", "Winfrey", "", "")]
        [InlineData("J.K.", "Rowling", "", "")]
        [InlineData("Jon", "Stewart", "", "")]
        [InlineData("Trevor", "Noah", "", "")]
        public void TestValidateRealName_ValidNames(string first, string last, string refFirst, string refLast)
        {
            Assert.True(UserUtility.validateRealName(first, last, ref refFirst, ref refLast));
            Assert.Equal(first, refFirst);
            Assert.Equal(last, refLast);
        }

        [Theory]
        [InlineData("", "", "", "")]
        public void TestValidateRealName_InvalidEmptyStringNames(string first, string last, string refFirst, string refLast)
        {
            Assert.False(UserUtility.validateRealName(first, last, ref refFirst, ref refLast));
            Assert.Equal(first, refFirst);
            Assert.Equal(last, refLast);
        }

        [Theory]
        [InlineData("", "Winfrey", "", "")]
        public void TestValidateRealName_SingleInvalidEmptyFirstName(string first, string last, string refFirst, string refLast)
        {
            Assert.False(UserUtility.validateRealName(first, last, ref refFirst, ref refLast));
            Assert.Equal(first, refFirst);
            Assert.NotEqual(last, refLast);
        }

        [Theory]
        [InlineData("Jon", "", "", "")]
        public void TestValidateRealName_SingleInvalidEmptyLastName(string first, string last, string refFirst, string refLast)
        {
            Assert.False(UserUtility.validateRealName(first, last, ref refFirst, ref refLast));
            Assert.NotEqual(first, refFirst);
            Assert.Equal(last, refLast);
        }


        [Theory]
        [InlineData(null, null, "", "")]
        [InlineData("", null, "", "")]
        [InlineData(null, "", "", "")]
        public void TestValidateRealName_InvalidNullNames(string first, string last, string refFirst, string refLast)
        {
            Assert.False(UserUtility.validateRealName(first, last, ref refFirst, ref refLast));
        }

    }
}