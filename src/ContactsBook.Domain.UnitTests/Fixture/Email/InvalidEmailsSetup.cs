using Xunit;

namespace ContactsBook.Domain.UnitTests.Fixture.Email
{
    public class InvalidEmailsSetup : TheoryData<string>
    {
        public InvalidEmailsSetup()
        {
            Add("@email.com");
            Add("a@.com");
            Add("a@i@fl.m");
            Add("aasdaf@i.m@");
            Add(".");
            Add("@.");
        }
    }
}