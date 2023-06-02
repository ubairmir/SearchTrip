using System.Collections.Generic;
using System.Threading.Tasks;
using SearchTrips.Models.Requst;

namespace SearchTrips.Repository.Abstract
{
    public interface IEmailRepository
    {
        string SendEmail(Quotes quote);
        string SendFeedback(Feedback feedback);
    }
}
