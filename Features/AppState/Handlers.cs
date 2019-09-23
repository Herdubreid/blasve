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
            JsService JsService { get; }
            AppState AppState => Store.GetState<AppState>();
            async public override Task<Unit> Handle(AddressBookAction aAction, CancellationToken aCancellationToken)
            {
                try
                {
                    var rq = new Data.W01012A.Request(aAction.AN8);
                    if (aAction.RowAction == RowAction.Save)
                    {
                        rq.SaveAction(AppState.AddressBook);
                        AppState.EditMode = false;
                        await JsService.InitMenu("menu", new string[] { "Back", "Edit" });
                    }
                    var rs = await E1Service.RequestAsync<Data.W01012A.Response>(rq);
                    AppState.AddressBook = rs.fs_P01012_W01012A.data;
                    EventHandler handler = AppState.StateChanged;
                    handler?.Invoke(AppState, null);
                }
                catch { }
                return Unit.Value;
            }
            public AddressBookHandler(IStore store, E1Service e1Service, JsService jsService) : base(store)
            {
                E1Service = e1Service;
                JsService = jsService;
            }
        }
        public class EditModeHandler : ActionHandler<EditModeAction>
        {
            AppState AppState => Store.GetState<AppState>();
            public override Task<Unit> Handle(EditModeAction aAction, CancellationToken aCancellationToken)
            {
                AppState.EditMode = aAction.EditMode;
                EventHandler handler = AppState.StateChanged;
                handler?.Invoke(AppState, null);
                return Unit.Task;
            }
            public EditModeHandler(IStore store) : base(store) { }
        }
    }
}
