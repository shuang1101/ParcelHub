using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParcelHub.Models
{
    public class UserEmailOption
    {
        public List<string> Receiver { get; set; }
        public string SubjectLine { get; set; }

        public string Body { get; set; }

        public List<KeyValuePair<string, string>> PlaceHolder{get;set;}
    }
}
