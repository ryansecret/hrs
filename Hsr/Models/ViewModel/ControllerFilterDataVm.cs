using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hsr.Models.ViewModel
{
    public class ControllerFilterDataVm
    {
       
           public int TestId { get; set; }
            public int? MsgId
            {
                get;
                set;
            }

         
            public string ModuleName
            {
                get;
                set;
            }

       
            public string MethodeName
            {
                get;
                set;
            }

         
            public DateTime? OperationTime
            {
                get;
                set;
            }

          
            public string Description
            {
                get;
                set;
            }
             
         
    }
}