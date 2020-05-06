using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidController : MonoBehaviour
{
    //speed of the boid
    public float movementSpeed = 5f;
    //direction boid is facing
    public float direction = 0f;

    //rigid body of the Boid
    private Rigidbody2D rigidBody;

    //Boid list that are nearby
    private List<GameObject> boidList;

    [Header("Separation:")]
    //To activate the rule
    public bool activateSeparation;
    //how far can boid see for separation
    public float separationVisionZone = 1f;
    //Speed of turning action
    public float separationTurningSpeed = 5f;

    [Header("Alignment:")]
    //To activate the rule
    public bool activateAlignment;
    //how far can boid see for separation
    public float alignmentVisionZone = 1f;
    //Speed of turning action
    public float alignmentTurningSpeed = 5f;

    [Header("Cohesion:")]
    //To activate the rule
    public bool activateCohesion;
    //how far can boid see for separation
    public float cohesionVisionZone = 1f;
    //Speed of turning action
    public float cohesionTurningSpeed = 5f;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = transform.GetComponent<Rigidbody2D>();
        direction = transform.rotation.eulerAngles.z;
    }

    // Update is called once per frame
    void Update()
    {

        rigidBody.velocity = transform.up * movementSpeed;
        transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, direction);

        boidList = new List<GameObject>(GameObject.Find("BoidGenerator").GetComponent<GenerateTeritories>().ReturnRegionBoids(gameObject));
        boidList.Remove(gameObject);

        //Separation: steer to avoid crowding local flockmates
        if (activateSeparation)
        {
            int toTheRight = 0;
            int toTheLeft = 0;

            foreach (GameObject boid in boidList)
            {
                float distance = Vector2.Distance(transform.position, boid.transform.position);

                if (distance < separationVisionZone)
                {
                    if (Vector2.Angle(boid.transform.position - transform.position, transform.right) < 90)
                    {
                        toTheRight++;
                    }
                    else
                    {
                        toTheLeft++;
                    }
                }
            }

            if (toTheRight > toTheLeft)
            {
                direction += separationTurningSpeed * Time.deltaTime * 2; //turns left
            }
            else if (toTheRight < toTheLeft)
            {
                direction -= separationTurningSpeed * Time.deltaTime * 2; //turns right
            }

            AdjustDirection();
        }

        //alignment: steer towards the average heading of local flockmates
        if (activateAlignment)
        {
            float angle = 0f;
            int adjecentBoidCount = 0;

            foreach (GameObject boid in boidList)
            {
                float distance = Vector2.Distance(transform.position, boid.transform.position);

                if (distance < alignmentVisionZone)
                {
                    angle += boid.GetComponent<BoidController>().direction;
                    adjecentBoidCount++;
                }
            }
            angle /= adjecentBoidCount;

            if (angle < direction)
            {
                direction -= alignmentTurningSpeed * Time.deltaTime; //turns left
            }
            else
            {
                direction += alignmentTurningSpeed * Time.deltaTime; //turns right
            }

            AdjustDirection();
        }
  
        //cohesion: steer to move towards the average position(center of mass) of local flockmates
        if (activateCohesion)
        {
            float middleX = 0;
            float middleY = 0;

            int adjecentBoidCount = 0;

            foreach (GameObject boid in boidList)
            {
                float distance = Vector2.Distance(transform.position, boid.transform.position);

                if (distance < cohesionVisionZone)
                {
                    middleX += boid.transform.position.x;
                    middleY += boid.transform.position.y;

                    adjecentBoidCount++;
                }
            }

            middleX /= adjecentBoidCount;
            middleY /= adjecentBoidCount;

            Vector3 position = new Vector3(middleX, middleY);

            if (Vector2.Angle(position - transform.position, transform.right) < 90)
            {
                direction -= cohesionTurningSpeed * Time.deltaTime; //turns left
            }
            else
            {
                direction += cohesionTurningSpeed * Time.deltaTime; //turns right
            }

            AdjustDirection();
        }
    }

    private void AdjustDirection()
    {
        if (direction < 0)
            direction += 360;
        else if (direction >= 360)
        {
            direction -= 360;
        }
    }
}
