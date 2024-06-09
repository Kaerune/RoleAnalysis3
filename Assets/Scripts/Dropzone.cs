using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Dropzone : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] List<Card> playedCards;
    [SerializeField] DiscardPile discard;
    [SerializeField] PlayerArea playerArea;
    [SerializeField] public ConvoTopic selectedConvoTopic;
    [SerializeField] TopicContainer topicContainer;
    int score = 0;
    // Start is called before the first frame update
    void Awake()
    {
        playedCards = new List<Card>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void addCard(Card value)
    {
        playedCards.Add(value);
        playerArea.RemoveCards(value);
    }
    public void removeCard(Card value)
    {

    }
    public void scoreCard()
    {
        for (int i = 0; i < playedCards.Count; i++)
        {
            score += playedCards[i].Power;
            if (i + 1 < playedCards.Count)
            {
                Card card1 = playedCards[i];
                Card card2 = playedCards[i + 1];
                if (card1.Type == card2.Type) { score++; }
                if (card1.Power == card2.Power) { score++; }
                
            }
            discard.addToDiscard(playedCards[i]);
            playedCards[i].transform.parent = discard.transform;
            playedCards[i].container.SetActive(false);
        }
        
        selectedConvoTopic.PowerNum -= score;
        selectedConvoTopic.numText.text = selectedConvoTopic.PowerNum.ToString();
        scoreText.text = "Score: " + score.ToString();
        playedCards.Clear();

        if (selectedConvoTopic.PowerNum <= 0)
        {
            selectedConvoTopic.isClicked= false;
            topicContainer.enableButtons();
            selectedConvoTopic.gameObject.SetActive(false);
            topicContainer.doneConvos.Add(selectedConvoTopic);
            topicContainer.convoTopics.Remove(selectedConvoTopic);
            selectedConvoTopic = null;

        }
    }
}
