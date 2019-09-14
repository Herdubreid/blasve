using BlazorState;
using MediatR;
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
            AppState AppState => Store.GetState<AppState>();
            public override Task<Unit> Handle(AddressBookAction aAction, CancellationToken aCancellationToken)
            {
                AppState.AddressBook = aAction.AddressBook;
                return Unit.Task;
            }
            public AddressBookHandler(IStore store) : base(store) { }
        }
    }
}
