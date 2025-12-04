using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filminurk.Core.dto
{
    public class EmailDTO
    {
        public string SendToThisAddress { get; set; }
        public string EmailSubject { get; set; }
        public string EmailContent { get; set; }
    }
}
