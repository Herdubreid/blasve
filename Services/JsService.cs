using blasve.Features.AppState;
using MediatR;
using Microsoft.JSInterop;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using System.Web;

namespace blasve.Services
{
    public class JsService
    {
        IJSRuntime JS { get; }
        E1Service E1Service { get; }
        IMediator Mediator { get; }
        DotNetObjectReference<JsService> Ref { get; }
        public event EventHandler StateChanged;
        [JSInvokable]
        async public void List(string cmd)
        {
            try
            {
                var keywords = cmd.Split(" ");
                var rq = new Data.F0101.Request(keywords);
                var rs = await E1Service.RequestAsync<Data.F0101.Response>(rq);
                var rows = rs.fs_DATABROWSE_F0101.data.gridData.rowset;
                await JS.InvokeVoidAsync("Terminal.ChangeHistory", rows);
                await JS.InvokeVoidAsync("Terminal.SetMessage", $"{rows.Length}{(rs.fs_DATABROWSE_F0101.data.gridData.summary.moreRecords ? " or more" : "")} matching customers.");
                if (rows.Length == 1)
                {
                    Command(rows[0].F0101_AN8.ToString());
                }
            }
            catch (Exception e)
            {
                await JS.InvokeVoidAsync("Terminal.SetMessage", e.Message);
            }
        }
        [JSInvokable]
        async public void Command(string an8)
        {
            await JS.InvokeVoidAsync("Terminal.OpenWindow", "AddressBook", an8);
        }
        [JSInvokable]
        async public Task Menu(string item)
        {
            if (string.Compare(item, "Edit") == 0)
            {
                await JS.InvokeVoidAsync("Menu.Clear");
                await Mediator.Send(new EditModeAction { EditMode = true });
            }
        }
        [JSInvokable]
        public void Notify(string cmd)
        {
            Mediator.Send(new AddressBookAction { AN8 = cmd, RowAction = RowAction.Get });
        }
        [JSInvokable]
        async public Task EscapeTerminal()
        {
            await JS.InvokeVoidAsync("Terminal.Clear");
            await JS.InvokeVoidAsync("Menu.Init", Ref, "menu", new string[] { "Back", "Edit" });
        }
        async public Task InitTerminal(string el)
        {
            await JS.InvokeVoidAsync("Terminal.Init", Ref, el);
            await JS.InvokeVoidAsync("Terminal.SetPrompt", "Customer:");
        }
        async public Task InitMenu(string el, string[] options)
        {
            await JS.InvokeVoidAsync("Menu.Init", Ref, el, options);
        }
        public void InitChildWindow()
        {
            JS.InvokeVoidAsync("Notification.Init", Ref);
        }
        public JsService(IJSRuntime jS, E1Service e1Service, IMediator mediator)
        {
            JS = jS;
            E1Service = e1Service;
            Mediator = mediator;
            Ref = DotNetObjectReference.Create(this);
        }
    }
}
