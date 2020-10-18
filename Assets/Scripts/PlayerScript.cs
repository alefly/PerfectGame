using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    private GameController gameController;
    private HealthTextScript healthTextScript;

    private const int ASTEROIDLAYER = 8;
    private const int BONUSLAYER = 9;

    private const float Y = (float)-4.0;
    private const float Z = (float)-1.0;
    public float step = (float)0.1;
    public float rightBorder;
    public float leftBorder;
    public int startHealth = 3;
    private int health;

    private static PlayerScript _playerScript;

    public static PlayerScript Instance
    {
        get
        {
            if (_playerScript == null)
            {
                _playerScript = FindObjectOfType<PlayerScript>();
            }
            return _playerScript;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        float cameraWidth = Camera.main.orthographicSize * ((float)Screen.width / (float)Screen.height);
        float playerWigth = GetComponent<SpriteRenderer>().sprite.bounds.size.x * transform.localScale.x / 2;
        rightBorder = Camera.main.transform.position.x + cameraWidth - playerWigth;
        leftBorder = Camera.main.transform.position.x - cameraWidth + playerWigth;
        gameController = GameController.Instance;
        healthTextScript = HealthTextScript.Instance;
        SetAlive();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.RightArrow) && transform.position.x < rightBorder && gameController.isRuning)
        {
            transform.position = new Vector3(transform.position.x + step, Y, Z);
        }
        if (Input.GetKey(KeyCode.LeftArrow) && transform.position.x > leftBorder && gameController.isRuning)
        {
            transform.position = new Vector3(transform.position.x - step, Y, Z);
        }

    }

    public void OnCollisionEnter2D(Collision2D coll)
    {
        if (!gameController.isRuning)
            return;
        switch (coll.gameObject.layer) {
            case ASTEROIDLAYER:
                health--;
                healthTextScript.UpdateTextHealth(health);
                gameController.decrementAsteroidsCount();
                break;
            case BONUSLAYER:
                health++;
                healthTextScript.UpdateTextHealth(health);
                break;
        }
        Destroy(coll.gameObject);
        if (health < 1)
        {
            gameController.GameOver();
        }
    }

    public void SetAlive() {
        health = startHealth;
        transform.position = new Vector3(0, Y, Z);
        healthTextScript.UpdateTextHealth(health);
    }
}
