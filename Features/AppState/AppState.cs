using BlazorState;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace blasve.Features.AppState
{
    public partial class AppState : State<AppState>
    {
        public event EventHandler StateChanged;
        public Data.W01012A.Form AddressBook { get; set; }
        protected override void Initialize() { }
    }
}
