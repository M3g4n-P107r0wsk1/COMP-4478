using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    [SerializeField] MemoryCard originalCard; // card to clone from
    [SerializeField] Sprite[] images; // card front sprites

    [SerializeField] TMP_Text movesText;
    [SerializeField] TMP_Text gameOverText;
    [SerializeField] GameObject restartButton;

    public const int gridRows = 4;
    public const int gridColumns = 4;
    public const float offsetX = 3f; // card x offset
    public const float offsetY = 3.2f; // card y offset

    private int score = 0;
    private int moves = 0;
    private MemoryCard firstRevealed; // first card revealed
    private MemoryCard secondRevealed; // second card revealed

    public bool canReveal { get { return secondRevealed == null; } }

    // Start is called before the first frame update
    void Start()
    {
        gameOverText.enabled = false; // hide game over text
        restartButton.SetActive(false); // hide restart button

        movesText.transform.position = new Vector3(-8.5f, -0.15f, -10f); // put moves text under subtitle
        movesText.alignment = TextAlignmentOptions.Left; // left align moves text

        Vector3 startPos = originalCard.transform.position; // get card initial position
        int[] numbers = {0, 0, 1, 1, 2, 2, 3, 3, 4, 4, 5, 5, 6, 6, 7, 7};
        numbers = ShuffleArray(numbers);

        for (int i = 0; i < gridColumns; i++)
        {
            for (int j = 0; j < gridRows; j++)
            {
                MemoryCard card;

                if (i == 0 && j == 0)
                {
                    card = originalCard; // use original card
                }
                else
                {
                    card = Instantiate(originalCard) as MemoryCard; // clone original card
                }

                int index = j * gridColumns + i;
                int id = numbers[index]; // use shuffled list to give card an id
                card.SetCard(id, images[id]); // use id to get card sprite

                float posX = (offsetX * i) + startPos.x;
                float posY = (offsetY * j) + startPos.y;
                card.transform.position = new Vector3(posX, posY, startPos.z); // move card to new position
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        // if the user has correctly matched all cards
        if (score == 8)
        {
            gameOverText.enabled = true; // show game over text
            restartButton.SetActive(true); // show restart button

            movesText.transform.position = new Vector3(-8f, -1.55f, -10f); // put moves text under game over message
            movesText.alignment = TextAlignmentOptions.Center; // center moves text
        }
    }

    // Determines whether the first or second card was revealed and whether they match
    public void CardRevealed(MemoryCard card)
    {
        if (firstRevealed == null)
        {
            firstRevealed = card;
        }
        else
        {
            secondRevealed = card;
            StartCoroutine(CheckMatch()); // start coroutine to check for a match
        }
    }

    // Checks if the first card revealed matches the second card revealed
    private IEnumerator CheckMatch()
    {
        if (firstRevealed.Id == secondRevealed.Id)
        {
            score++;
        }
        else
        {
            yield return new WaitForSeconds(0.75f); // wait briefly

            firstRevealed.Unreveal(); // hide first card
            secondRevealed.Unreveal(); // hide second card
        }

        // add to the moves count
        moves++;
        movesText.text = $"Moves: {moves}";

        // reset first and second revealed cards values to default
        firstRevealed = null;
        secondRevealed = null;
    }

    // Restarts the game for the user
    public void RestartGame()
    {
        SceneManager.LoadScene("SampleScene"); // reload the scene
    }

    // Shuffles the order of int arrays
    private int[] ShuffleArray(int[] numbers)
    {
        int[] newArray = numbers.Clone() as int[];
        
        for (int i = 0; i < newArray.Length; i++)
        {
            int temp = newArray[i];
            int randomInt = Random.Range(i, newArray.Length);

            newArray[i] = newArray[randomInt];
            newArray[randomInt] = temp;
        }

        return newArray;
    }
}
