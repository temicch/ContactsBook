using System.Collections.Generic;

namespace ContactsBook.WebApi.Models.Contact;

public class ErrorResponse
{
    public string Type { get; set; }
    public string Title { get; set; }
    public int Status { get; set; }
    public string TraceId { get; set; }
    public IDictionary<string, ICollection<string>> Errors { get; set; }
}
