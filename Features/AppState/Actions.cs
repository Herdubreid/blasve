using BlazorState;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace blasve.Features.AppState
{
    public class AddressBookAction : IAction
    {
        public Data.W01012A.Form AddressBook { get; set; }
    }
}
