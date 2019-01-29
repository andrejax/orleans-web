using Orleans.Providers;
using GrainInterfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using Orleans;
using System;

namespace Grains
{
    public class CacheContainer
    {
        public HashSet<string> domain { set; get; }
        public CacheContainer() 
        {
            this.domain = new HashSet<string>();
        }
    }
    [StorageProvider(ProviderName = "BlobStorage")]
    public class EmailGrain : Grain<CacheContainer>, IEmail
    {
        public Task<bool> addEmail(string email)
        {
            return Task.FromResult(State.domain.Add(email));
        }
        public Task<bool> checkEmail(string email)
        {
            return Task.FromResult(State.domain.Contains(email));
        }
        public override Task OnActivateAsync()
        {
            RegisterTimer(Save, null, TimeSpan.FromMinutes(0), TimeSpan.FromMinutes(1));
            return base.OnActivateAsync();
        }
        async Task Save(object o)
        {
            await base.WriteStateAsync();
        }
    }
}
