public class HideCardCommand : ICommand
{
    private Card card;

    public HideCardCommand(Card card)
    {
        this.card = card;
    }

    // Execute hides the card
    public void Execute()
    {
        card.HideCard();
    }

    // Undo flips it back up
    public void Undo()
    {
        card.ShowCard();
    }
}