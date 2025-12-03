public class FlipCardCommand : ICommand
{
    private Card card;

    public FlipCardCommand(Card card)
    {
        this.card = card;
    }

    public void Execute()
    {
        card.Flip();   // we will add this method to Card.cs
    }

    public void Undo()
    {
        card.HideDirect();   // flip back instantly (no coroutine)
    }
}