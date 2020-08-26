using Application.BLL.Services;
using Application.DAL.Entity;
using System.Threading.Tasks;
using System.Web.Http;

namespace WebApplication2.Controllers
{
    [RoutePrefix("api/contacts")]
    public class ContactsController : ApiController
    {
        public IContactsService ContactsService { get; set; }
        
        public ContactsController(IContactsService contactsService)
        {
            ContactsService = contactsService;
        }

        [HttpGet]
        [Route("name/{name}")]
        public async Task<IHttpActionResult> GetByName(string name)
        {
            return Json(await ContactsService.GetContactsByNameAsync(name));
        }

        [HttpGet]
        [Route("all")]
        public async Task<IHttpActionResult> Get()
        {
            var contacts = await ContactsService.GetAllContactsAsync();
            return Json(contacts);
        }

        [HttpGet]
        [Route("contact/{id}")]
        public async Task<IHttpActionResult> Get(int id)
        {
            var contact = await ContactsService.GetContactByIdAsync(id);
            if (contact == null)
                return NotFound();
            return Json(contact);
        }

        [HttpPost]
        [Route("create")]
        public async Task<IHttpActionResult> Post([FromBody]IContact value)
        {
            var result = await ContactsService.AddContactAsync(value);
            if (result == 0)
                return BadRequest();
            return Ok(result);
        }

        [HttpPut]
        [Route("contact/{id}")]
        public async Task<IHttpActionResult> Put(int id, [FromBody]IContact value)
        {
            var result = await ContactsService.UpdateContactAsync(id, value);
            if (result == false)
                return BadRequest();
            return Ok();
        }

        [HttpDelete]
        [Route("contact/{id}")]
        public async Task<IHttpActionResult> Delete(int id)
        {
            var result = await ContactsService.RemoveContactByIdAsync(id);
            if (result == false)
                return BadRequest();
            return Ok();
        }

        [HttpGet]
        [Route("email/{email}")]
        public async Task<IHttpActionResult> GetByEmail(string email)
        {
            return Json(await ContactsService.GetContactsByEmailAsync(email));
        }

        [HttpGet]
        [Route("number/{number}")]
        public async Task<IHttpActionResult> GetByNumber(string number)
        {
            return Json(await ContactsService.GetContactsByPhoneNumberAsync(number));
        }
    }
}
