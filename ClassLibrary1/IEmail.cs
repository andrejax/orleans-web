using Orleans;
using System.Threading.Tasks;

namespace GrainInterfaces
{
    public interface IEmail : IGrainWithStringKey
    {
        Task<bool> checkEmail(string email);
        Task<bool> addEmail(string email);
    }
}
