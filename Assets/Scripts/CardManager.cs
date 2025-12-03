using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public void SetupCards()
    {
        ClearCards();

        pairsMatched = 0;
        totalPairs = cardFaces.Length;

        shuffledIDs = new List<int>();

        // create ID pairs
        for (int i = 0; i < cardFaces.Length; i++)
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

        firstCard.HideCard();
        secondCard.HideCard();

        firstCard = null;
        secondCard = null;
    }
    
    public bool IsSelectionLocked()
    {
        return firstCard != null && secondCard != null;
    }

}
