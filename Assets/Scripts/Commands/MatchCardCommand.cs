using UnityEngine;

public class MatchCardCommand : ICommand
{
    private Card cardA;
    private Card cardB;

    public MatchCardCommand(Card a, Card b)
    {
        cardA = a;
        cardB = b;
    }

    public void Execute()
    {
        Debug.Log("[MatchCardCommand] Execute() — matching cards");

        cardA.SetMatched();
        cardB.SetMatched();
    }

    public void Undo()
    {
        Debug.Log("[MatchCardCommand] Undo() — unmatching cards");

        cardA.UnsetMatched();
        cardB.UnsetMatched();
    }
}