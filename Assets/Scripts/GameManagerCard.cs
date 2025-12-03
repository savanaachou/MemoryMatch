using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

public class GameManagerCard : MonoBehaviour
{
    public static GameManagerCard Instance { get; private set; }
    
    public Card cardPrefab;
    public Sprite cardBack;
    public Sprite[] cardFaces;
    
    private List<Card> cards;
    private List<int> cardIDs;
    public Card firstCard, secondCard;
    
    public Transform cardHolder;
    public TextMeshProUGUI timerText;
    
    public GameObject startPanel;
    public GameObject gamePanel;
    public GameObject endPanel;

    public TextMeshProUGUI resultText;

    private int pairsMatched;
    private int totalPairs;
    private float timer;
    private bool isGameOver;
    private bool isLevelFinished;
    private bool gameStarted = false;

    public float maxTime = 60f;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }
    
    void Start()
    {
        // Start with only start panel visible
        startPanel.SetActive(true);
        gamePanel.SetActive(false);
        endPanel.SetActive(false);

        isGameOver = false;
        isLevelFinished = false;
    }
    
    void InitializeGame()
    {
        cards = new List<Card>();
        cardIDs = new List<int>();
        pairsMatched = 0;
        totalPairs = cardFaces.Length;
        
        timer = maxTime;
        isGameOver = false;
        isLevelFinished = false;

        CreateCards();
    }

    void Update()
    {
        if (!gameStarted) return;

        if (!isGameOver && !isLevelFinished)
        {
            if (timer > 0)
            {
                timer -= Time.deltaTime;
                UpdateTimerText();
            }
            else
            {
                GameOver();
            }
        }
    }

    void CreateCards()
    {
        // build list of IDs (pairs)
        cardIDs = new List<int>();
        for (int i = 0; i < cardFaces.Length; i++)
        {
            cardIDs.Add(i);
            cardIDs.Add(i);
        }

        // shuffle IDs
        for (int i = 0; i < cardIDs.Count; i++)
        {
            int randomIndex = Random.Range(i, cardIDs.Count);
            int temp = cardIDs[i];
            cardIDs[i] = cardIDs[randomIndex];
            cardIDs[randomIndex] = temp;
        }

        // instantiate in shuffled order
        cards = new List<Card>();
        foreach (int id in cardIDs)
        {
            Card newCard = Instantiate(cardPrefab, cardHolder);
            newCard.gameManager = this;
            newCard.cardID = id;
            cards.Add(newCard);
        }
    }

    public void CardFlipped(Card flippedCard)
    {
        if (firstCard == null)
        {
            firstCard = flippedCard;
        }
        else if (secondCard == null)
        {
            secondCard = flippedCard;
            CheckMatch();
        }
    }

    void CheckMatch()
    {
        if (firstCard.cardID == secondCard.cardID)
        {
            pairsMatched++;

            if (pairsMatched == totalPairs)
            {
                LevelFinished();
            }
            
            firstCard = null;
            secondCard = null;
        }
        else
        {
            StartCoroutine(FlipBackCards());
        }
    }

    IEnumerator FlipBackCards()
    {
        yield return new WaitForSeconds(1f);
        firstCard.HideCard();
        secondCard.HideCard();
        firstCard = null;
        secondCard = null;
    }

    void GameOver()
    {
        isGameOver = true;
        FinalPanel();
    }

    void LevelFinished()
    {
        isLevelFinished = true;
        FinalPanel();
    }
    
    public void StartGame()
    {
        startPanel.SetActive(false);
        gamePanel.SetActive(true);
        endPanel.SetActive(false);

        gameStarted = true;

        InitializeGame();
    }
    
    public void QuitGame()
    {
        #if UNITY_EDITOR
                EditorApplication.isPlaying = false;
        #else
                    Application.Quit(); // Quit the application in a build
        #endif
    }

    void FinalPanel()
    {
        gamePanel.SetActive(false);
        endPanel.SetActive(true);

        if (isGameOver)
            resultText.text = "Time's up! Try Again!";
        else if (isLevelFinished)
            resultText.text = "You Win!";
    }

    public void RestartGame()
    {
        // Reset UI
        endPanel.SetActive(false);
        startPanel.SetActive(true);
        gameStarted = false;

        // Delete cards from previous game
        if (cards != null)
        {
            foreach (var card in cards)
            {
                Destroy(card.gameObject);
            }
        }

        cards = new List<Card>();
        cardIDs = new List<int>();

        firstCard = null;
        secondCard = null;
    }

    void UpdateTimerText()
    {
        timerText.text = "Time Left: " + Mathf.Round(timer) + "s";
    }
}
