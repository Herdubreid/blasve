using blasve.Features.AppState;
using MediatR;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace blasve.Services
{
    public class TerminalService
    {
        IJSRuntime JS { get; }
        E1Service E1Service { get; }
        IMediator Mediator { get; }
        DotNetObjectReference<TerminalService> Ref { get; }
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
            await JS.InvokeVoidAsync("Terminal.BusyOn");
            try
            {
                var rq = new Data.W01012A.Request(an8);
                var rs = await E1Service.RequestAsync<Data.W01012A.Response>(rq);
                await Mediator.Send(new AddressBookAction { AddressBook = rs.fs_P01012_W01012A.data });
                EventHandler handler = StateChanged;
                handler?.Invoke(this, null);
            }
            catch (Exception e)
            {
                await JS.InvokeVoidAsync("Terminal.SetMessage", e.Message);
            }
            await JS.InvokeVoidAsync("Terminal.BusyOff");
        }
        public void Init(string el)
        {
            JS.InvokeVoidAsync("Terminal.Init", el, Ref);
            JS.InvokeVoidAsync("Terminal.SetPrompt", "Customer:");
        }
        public TerminalService(IJSRuntime jS, E1Service e1Service, IMediator mediator)
        {
            JS = jS;
            E1Service = e1Service;
            Mediator = mediator;
            Ref = DotNetObjectReference.Create(this);
        }
    }
}
