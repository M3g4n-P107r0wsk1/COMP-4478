using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class timeDestroyer : MonoBehaviour
{
    public float aliveTimer;

    // Start is called before the first frame update
    void Start()
    {
        // removes falling items after a given time
        Destroy(gameObject, aliveTimer);
    }
}
