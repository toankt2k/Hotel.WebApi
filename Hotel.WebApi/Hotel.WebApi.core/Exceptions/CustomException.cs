using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.WebApi.core.Exceptions
{
    public class CustomException:Exception
    {
        public string Messenger { get; set; }
        public IDictionary ErrorData { get; set; }
        public CustomException(string msg, IDictionary data = null)
        {
            this.Messenger = msg;
            this.ErrorData = data;
        }
        public override IDictionary Data => this.ErrorData;
        public override string Message => this.Messenger;
    }
}
