using SearchTrips.Models.Requst;
using SearchTrips.Repository.Abstract;
using SearchTrips.Repository.Concrete;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SearchTrips.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmailController : Controller
    {
         private readonly IEmailRepository _emailRepository;
        public EmailController(IEmailRepository emailRepository)
        {
            _emailRepository = emailRepository;
        }

        [HttpPost("[action]")]
        public string Quotes([FromBody]  Quotes mailmodel)
        {
            if (mailmodel == null) return "Not Found";

           return _emailRepository.SendEmail(mailmodel).ToString();
        }

        [HttpPost("[action]")]
        public string FeedBack([FromBody] Feedback mailmodel)
        {
            if (mailmodel == null) return "Not Found";

            return _emailRepository.SendFeedback(mailmodel).ToString();
        }
    }
}
