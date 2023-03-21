using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemoryCard : MonoBehaviour
{
    [SerializeField] GameObject cardBack; // back of card sprite
    [SerializeField] SceneController controller; // scene controller
    
    private int _id; // card id
    public int Id { get { return _id; } } // get card id

    // Set card sprite and id
    public void SetCard(int id, Sprite image)
    {
        _id = id;
        GetComponent<SpriteRenderer>().sprite = image;
    }

    // Show card back to hide card front
    public void Unreveal()
    {
        cardBack.SetActive(true);
    }

    // Hide card back when card is clicked
    public void OnMouseDown()
    {
        if (cardBack.activeSelf && controller.canReveal)
        {
            cardBack.SetActive(false);
            controller.CardRevealed(this);
        }
    }
}
