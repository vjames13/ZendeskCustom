using System;
using System.Collections.Generic;
using System.Text;

namespace ZendeskCustom.Models.Ticket
{
    public class FieldOptions
    {
        //this contains the Custom Field IDs and accompanying Names to loop through while entering custom field values
        public IList<long?> idvals { get; set; }
        public IList<string> fieldnames { get; set; }
        public IList<string> fieldtypes { get; set; }
        public IList<bool> fieldrequired { get; set; }
    }
}
