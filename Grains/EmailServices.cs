using System;
using System.Threading.Tasks;
using GrainInterfaces;
using System.Net.Mail;
namespace Grains
{
    public static class EmailServices
    {
        public static async Task<bool> CreateEmail(string email)
        {
            try
            {
                MailAddress eobj = new MailAddress(email);
                var grain = OrleansClient.ClusterClient.GetGrain<IEmail>(eobj.Host);
                return await grain.addEmail(email);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public static async Task<bool> CheckEmail(string email)
        {
            try
            {
                MailAddress eobj = new MailAddress(email);
                var grain = OrleansClient.ClusterClient.GetGrain<IEmail>(eobj.Host);
                return await grain.checkEmail(email);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
