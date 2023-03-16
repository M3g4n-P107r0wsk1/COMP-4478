using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class itemChecker : MonoBehaviour
{
    public int score;
    public GameObject GameOverObject;
    public GameObject ScoreObject;
    Text scoreText;
    private ItemSpawn itemSpawn;

    // Start is called before the first frame update
    private void Start()
    {
        scoreText = ScoreObject.GetComponent<Text>();
        GameOverObject.SetActive(false);
    }

    private void Awake()
    {
        itemSpawn = GameObject.FindObjectOfType<ItemSpawn>();
    }

    // sets the scoreText value of the ScoreObject
    void setScore(int score)
    {
        scoreText.text = "Score: " + score.ToString();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Good")
        {
            // update the score
            score = score + 10;

            // set the score text
            setScore(score);

            // remove the item which collided with the player
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.tag == "Bad")
        {
            // show the "Game Over!" text
            GameOverObject.SetActive(true);

            // send true to set gameOver boolean in the item spawner to stop from spawning more items
            itemSpawn.setGameOver(true);

            // set the score text
            setScore(score);

            // remove the item which collided with the player
            Destroy(collision.gameObject);

            // pause the currently dropping items and player
            Time.timeScale = 0;
        }
    }
}
