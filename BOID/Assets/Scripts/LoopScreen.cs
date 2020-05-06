using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopScreen : MonoBehaviour
{
    private Camera screen;

    private float bottom;
    private float top;
    private float left;
    private float right;

    public float offset = 0;

    // Start is called before the first frame update
    void Start()
    {
        screen = Camera.main;
        bottom = screen.transform.position.y - (screen.orthographicSize + offset);
        top = screen.transform.position.y + (screen.orthographicSize + offset);
        left = screen.transform.position.x - (screen.orthographicSize * screen.aspect + offset);
        right = screen.transform.position.x + (screen.orthographicSize * screen.aspect  + offset);
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x > right)
        {
            transform.position = new Vector2(left, transform.position.y);
        }
        else if (transform.position.x < left)
        {
            transform.position = new Vector2(right, transform.position.y);
        }

        if (transform.position.y > top)
        {
            transform.position = new Vector2(transform.position.x, bottom);
        }
        else if (transform.position.y < bottom)
        {
            transform.position = new Vector2(transform.position.x, top);
        }
    }
}
