using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField] private List<GameObject> asteroidPrefs;
    [SerializeField] private Text gameOverText;
    [SerializeField] private Button buttonRetry;
    [SerializeField] private GameObject healthBonus;

    public int maxNumAsteroids = 3;
    public int countAsteroids = 3;
    public int nonBonusStartTime = 10;
    private const float Y = (float)7.0;
    private const float Z = (float)-1.0;
    public bool isRuning = true;
    private PlayerScript player;
    System.Random random = new System.Random();
    public int score = 0; // TODO: Сделать подсчет очков 

    private static GameController _gameController;

    public static GameController Instance
    {
        get
        {
            if (_gameController == null)
            {
                _gameController = FindObjectOfType<GameController>();
            }
            return _gameController;
        }
    }

    public void decrementAsteroidsCount() {
        countAsteroids--;
    }

    // Start is called before the first frame update
    void Start()
    {
        player = PlayerScript.Instance;
        StartCoroutine(GenerateBonus(nonBonusStartTime));
    }

    // Update is called once per frame
    void Update()
    {
        if (countAsteroids < maxNumAsteroids && isRuning) {
            GenerateAsteroid();
        }
    }

    void GenerateAsteroid() {
        var asteroid = asteroidPrefs[random.Next() % asteroidPrefs.Count];
        float cameraWidth = Camera.main.orthographicSize * ((float)Screen.width / (float)Screen.height);
        float y = Y + (float)(random.NextDouble() * 2.0 * Y);
        float x = (float)(random.NextDouble() * 2.0 * cameraWidth - cameraWidth);
        Instantiate(asteroid, new Vector3(x, y, Z), Quaternion.identity);
        countAsteroids++;
    }

    void GenerateHealthBonus()
    {
        float cameraWidth = Camera.main.orthographicSize * ((float)Screen.width / (float)Screen.height);
        float y = Y + (float)(random.NextDouble() * 2.0 * Y);
        float x = (float)(random.NextDouble() * 2.0 * cameraWidth - cameraWidth);
        Instantiate(healthBonus, new Vector3(x, y, Z), Quaternion.identity);
    }

    public void GameOver() {
        gameOverText.gameObject.SetActive(true);
        buttonRetry.gameObject.SetActive(true);
        isRuning = false;
    }

    public void Restart() {
        gameOverText.gameObject.SetActive(false);
        buttonRetry.gameObject.SetActive(false);
        isRuning = true;
        player.SetAlive();
        StartCoroutine(GenerateBonus(nonBonusStartTime));
    }
    

    IEnumerator GenerateBonus(int seconds)
    {
        yield return new WaitForSeconds(seconds);
        if (isRuning)
        {
            seconds = 2 + random.Next() % 7;
            Debug.Log(seconds);
            GenerateHealthBonus();
            StartCoroutine(GenerateBonus(seconds));
        }
    }
}
