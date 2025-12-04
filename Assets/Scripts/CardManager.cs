using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GridSize
{
    ThreeByTwo,  // 3 columns, 2 rows (6 cards)
    FourByThree  // 4 columns, 3 rows (12 cards)
}

public class CardManager : MonoBehaviour
{
    public Card cardPrefab;
    public Sprite[] cardFaces;
    public Sprite cardBack;
    public Transform cardHolder;

    public event System.Action OnAllPairsMatched;

    private List<Card> cards = new();
    private List<int> shuffledIDs = new();

    private Card firstCard;
    private Card secondCard;

    private int pairsMatched = 0;
    private int totalPairs = 0;
    
    public CommandInvoker commandInvoker { get; private set; }
    
    public GridSize selectedGridSize = GridSize.FourByThree;  
    public UnityEngine.UI.GridLayoutGroup gridLayout;
    
    private void Awake()
    {
        commandInvoker = new CommandInvoker();
    }

    public void SetupCards()
    {
        ClearCards();

        int cardCount = selectedGridSize switch
        {
            GridSize.ThreeByTwo => 6,   // 3x2 grid
            GridSize.FourByThree => 12, // 4x3 grid
            _ => 12
        };

        totalPairs = cardCount / 2;
        pairsMatched = 0;

        shuffledIDs = new List<int>();

        // Only use the number of faces needed
        for (int i = 0; i < totalPairs; i++)
        {
            shuffledIDs.Add(i);
            shuffledIDs.Add(i);
        }

        // shuffle
        for (int i = 0; i < shuffledIDs.Count; i++)
        {
            int randomIndex = Random.Range(i, shuffledIDs.Count);
            (shuffledIDs[i], shuffledIDs[randomIndex]) = (shuffledIDs[randomIndex], shuffledIDs[i]);
        }

        // update grid layout
        ApplyGridLayout();

        // instantiate cards
        foreach (int id in shuffledIDs)
        {
            Card cardObj = Instantiate(cardPrefab, cardHolder);
            cardObj.Init(this, id, cardFaces[id], cardBack);
            cards.Add(cardObj);
        }
    }

    public void ClearCards()
    {
        foreach (var card in cards)
        {
            if (card != null) Destroy(card.gameObject);
        }
        cards.Clear();

        firstCard = null;
        secondCard = null;
    }

    public void CardFlipped(Card card)
    {
        if (firstCard == null)
        {
            firstCard = card;
        }
        else if (secondCard == null)
        {
            secondCard = card;
            CheckMatch();
        }
    }

    private void CheckMatch()
    {
        if (firstCard.cardID == secondCard.cardID)
        {
            ICommand matchCommand = new MatchCardCommand(firstCard, secondCard);
            commandInvoker.ExecuteCommand(matchCommand);
            
            pairsMatched++;

            firstCard = null;
            secondCard = null;

            if (pairsMatched == totalPairs)
            {
                OnAllPairsMatched?.Invoke();
            }
        }
        else
        {
            StartCoroutine(FlipBack());
        }
    }

    private IEnumerator FlipBack()
    {
        yield return new WaitForSeconds(1f);

        // Execute HideCardCommand for each card
        ICommand hideFirst = new HideCardCommand(firstCard);
        ICommand hideSecond = new HideCardCommand(secondCard);

        commandInvoker.ExecuteCommand(hideFirst);
        commandInvoker.ExecuteCommand(hideSecond);

        firstCard = null;
        secondCard = null;
    }
    
    private void ApplyGridLayout()
    {
        switch (selectedGridSize)
        {
            case GridSize.ThreeByTwo:
                gridLayout.constraint = UnityEngine.UI.GridLayoutGroup.Constraint.FixedColumnCount;
                gridLayout.constraintCount = 3;
                break;

            case GridSize.FourByThree:
                gridLayout.constraint = UnityEngine.UI.GridLayoutGroup.Constraint.FixedColumnCount;
                gridLayout.constraintCount = 4;
                break;
        }
    }
    
    public bool IsSelectionLocked()
    {
        return firstCard != null && secondCard != null;
    }

}
