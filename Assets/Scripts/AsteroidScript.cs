using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidScript : MonoBehaviour
{
    private float zoneDeath;
    private GameController gameController;

    // Start is called before the first frame update
    void Start()
    {
        zoneDeath = -Camera.main.orthographicSize - 2;
        gameController = GameController.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < zoneDeath) {              
            Destroy(gameObject);
            gameController.decrementAsteroidsCount();
        }
    }
}
