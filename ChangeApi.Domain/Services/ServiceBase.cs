using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChangeApi.Domain.Services
{
    public abstract class ServiceBase
    {
        protected ServiceBase()
        {
            Errors = new Dictionary<string, string>();
        }
        private Dictionary<string, string> Errors { get; }
        public bool IsValid => !Errors.Any();
        protected void AddError(string property, string message)
        {
            Errors.Add(property, message);
        }

        public List<KeyValuePair<string, string>> GetErrors => Errors.ToList();
    }
}
