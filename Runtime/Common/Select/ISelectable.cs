namespace Northgard.Presentation.Common.Select
{
    public interface ISelectable
    {
        bool IsSelected { get; }
        void Select(object sender);
        void Deselect(object sender);
        public delegate void SelectableDelegate(ISelectable selectable);
    }
}