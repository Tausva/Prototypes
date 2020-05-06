using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GenerateBoids : MonoBehaviour
{
    private Camera screen;

    private float bottom;
    private float top;
    private float left;
    private float right;

    private GameObject boidInstance;

    public int BoidCount = 20;
    public GameObject Boid;

    // Start is called before the first frame update
    void Awake()
    {
        if (SceneManager.GetActiveScene().name == "SimulationScene")
        {
            BoidCount = PlayerPrefs.GetInt("Amount", BoidCount);
        }

        screen = Camera.main;
        bottom = screen.transform.position.y - screen.orthographicSize;
        top = screen.transform.position.y + screen.orthographicSize;
        left = screen.transform.position.x - screen.orthographicSize * screen.aspect;
        right = screen.transform.position.x + screen.orthographicSize * screen.aspect;

        for (int i = 0; i < BoidCount; i++)
        {
            boidInstance = Instantiate(Boid);
            boidInstance.transform.position = new Vector2(Random.Range(left, right), Random.Range(bottom, top));
            boidInstance.transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, Random.Range(0f, 359f));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
