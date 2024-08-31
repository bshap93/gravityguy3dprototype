namespace Player.Interaction.Common
{
    public abstract class MenuAction
    {
        IInteractable SelectedObject { get; set; }
        protected abstract void Execute();
    }
}
