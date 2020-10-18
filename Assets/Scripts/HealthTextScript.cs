using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthTextScript : MonoBehaviour
{
    private static HealthTextScript _healthTextScript;

    public static HealthTextScript Instance
    {
        get
        {
            if (_healthTextScript == null)
            {
                _healthTextScript = FindObjectOfType<HealthTextScript>();
            }
            return _healthTextScript;
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpdateTextHealth(int health){
        GetComponent<Text>().text = $"HP: {health}";
    }
}
