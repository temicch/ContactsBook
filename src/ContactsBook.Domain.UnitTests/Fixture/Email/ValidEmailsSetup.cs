using Xunit;

namespace ContactsBook.Domain.UnitTests.Fixture.Email
{
    public class ValidEmailsSetup : TheoryData<string>
    {
        public ValidEmailsSetup()
        {
            Add("a@email.com");
            Add("a@il.com");
            Add("a@ifl.m");
            Add("aasdaf@i.m");
            Add("");
            Add(null);
        }
    }
}