using blasve.Services;
using BlazorState;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace blasve.Features.AppState
{
    public partial class AppState
    {
        public class AddressBookHandler : ActionHandler<AddressBookAction>
        {
            E1Service E1Service { get; }
            AppState AppState => Store.GetState<AppState>();
            async public override Task<Unit> Handle(AddressBookAction aAction, CancellationToken aCancellationToken)
            {
                try
                {
                    var rq = new Data.W01012A.Request(aAction.AN8);
                    var rs = await E1Service.RequestAsync<Data.W01012A.Response>(rq);
                    AppState.AddressBook = rs.fs_P01012_W01012A.data;
                    EventHandler handler = AppState.StateChanged;
                    handler?.Invoke(AppState, null);
                }
                catch { }
                return Unit.Value;
            }
            public AddressBookHandler(IStore store, E1Service e1Service) : base(store)
            {
                E1Service = e1Service;
            }
        }
    }
}
