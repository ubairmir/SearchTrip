using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using SearchTrips.Repository.Abstract;
using SearchTrips.Models.Requst;
using Microsoft.AspNetCore.Http;
using SearchTrips.Utility;

namespace SearchTrips.Repository.Concrete
{
    public class EmailRepository : IEmailRepository
    {

        
        public string SendEmail(Quotes model)
        {
            mail QuoteMial = new mail();
           return QuoteMial.SendQuotes(model.Name,model.Mobile,model.Email,model.Address); 

        }
        public string SendFeedback(Feedback model)
        {
            mail feedbackMail = new mail();
            return  feedbackMail.SendFeedback(model.Name, model.Email, model.Message);

        }
    }
}
