namespace Northgard.Presentation.Common.Select
{
    internal interface ISelectable
    {
        bool IsSelected { get; }
        void Select();
        void Deselect();
    }
}