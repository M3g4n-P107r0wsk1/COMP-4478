using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawn : MonoBehaviour
{
    public GameObject RightSide;
    public GameObject[] items;
    public float startDelay, repeatDelay;
    public bool gameOver;

    // Start is called before the first frame update
    void Start()
    {
        // repeatedly spawn dropping items
        InvokeRepeating("Spawn", startDelay, repeatDelay);
    }

    // sets the gameOver value
    public void setGameOver(bool gameOverValue)
    {
        gameOver = gameOverValue;
    }

    void Spawn()
    {
        // if the game is not over, spawn items
        if (!gameOver)
        {
            // choose a random x position between the spawner and right side to drop the item from
            Vector3 pos = new Vector3(Random.Range(gameObject.transform.position.x, RightSide.transform.position.x), gameObject.transform.position.y, 0);

            // spawn the item
            Instantiate(items[Random.Range(0, items.Length)], pos, gameObject.transform.rotation);
        }
       
    }
}
