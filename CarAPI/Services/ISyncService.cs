using Common.Models;

using System.Net.Http;
using System.Threading.Tasks;

namespace CarAPI.Services
{
    public interface ISyncService<T> where T: MongoDocument
    {
       HttpResponseMessage Upsert(T record);


       HttpResponseMessage Delete(T record);
    }
}
