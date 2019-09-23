using BlazorState;

namespace blasve.Features.AppState
{
    public enum RowAction
    {
        Get, Save
    }
    public class AddressBookAction : IAction
    {
        public RowAction RowAction { get; set; }
        public string AN8 { get; set; }
    }
    public class EditModeAction : IAction
    {
        public bool EditMode { get; set; }
    }
}
