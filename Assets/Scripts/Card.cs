using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    [Header("Card Visuals")]
    public Image cardImage;

    [HideInInspector] public int cardID;
    private Sprite faceSprite;
    private Sprite backSprite;

    private bool isFlipped = false;
    private bool isMatched = false;


    private CardManager cardManager;

    // Called by CardManager immediately after instantiation
    public void Init(CardManager manager, int id, Sprite face, Sprite back)
    {
        cardManager = manager;
        cardID = id;
        faceSprite = face;
        backSprite = back;

        HideCard();
    }

    public void OnCardClicked()
    {
        if (isFlipped || cardManager.IsSelectionLocked())
            return;

        ICommand command = new FlipCardCommand(this);
        cardManager.commandInvoker.ExecuteCommand(command);
    }

    public void ShowCard()
    {
        isFlipped = true;
        cardImage.sprite = faceSprite;
    }

    public void HideCard()
    {
        isFlipped = false;
        cardImage.sprite = backSprite;
    }
    
    public void Flip()
    {
        isFlipped = true;
        cardImage.sprite = faceSprite;
        cardManager.CardFlipped(this);
    }

    public void HideDirect()
    {
        isFlipped = false;
        cardImage.sprite = backSprite;
    }

    public void SetMatched()
    {
        isMatched = true;
    }

    public void UnsetMatched()
    {
        isMatched = false;
    }

    public bool IsMatched()
    {
        return isMatched;
    }

}