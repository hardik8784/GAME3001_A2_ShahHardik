using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBehaviour : MonoBehaviour
{
    [Range(0.5f, 10.0f)]
    public float speed = 5.0f;
    [Range(0.1f,1.0f)]
    public float circleRadius = 1;
    public bool clockwise = true;
    [Range(0.0f,360.0f)]
    public float angle = 270;
    public GameObject ball;
    public float minimumAngle = 0;
    public float maximumAngle = 90;
    public bool completeCircle = true;
    public SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        circleRadius = Mathf.Abs(ball.transform.localPosition.y);
        angle = angle * Mathf.PI / 180;
        MarkNewTarget();
    }

    public void MarkNewTarget()
    {
        float validSpawn = GameManager.Instance.validSpawn[GameManager.Instance.level];
        float validDistance = GameManager.Instance.validDistance[GameManager.Instance.level];
        float eulerAngle = (angle * 180 / Mathf.PI) % 360;
        
        minimumAngle = (eulerAngle + validSpawn + Random.Range(0, 25)) % 360;            // Generate angle between 0 to Pie
        if (minimumAngle + validDistance > 360)
        {
            minimumAngle -= validDistance;
        }
        maximumAngle = minimumAngle + validDistance;


        //Setup the Visual into the Circle
        spriteRenderer.material.SetFloat("_ArcStart", minimumAngle);
        spriteRenderer.material.SetFloat("_ArcEnd", maximumAngle);

        if ((eulerAngle < minimumAngle) && clockwise)
        {
            completeCircle = false;
        }
        if ((eulerAngle > maximumAngle) && !clockwise)
        {
            completeCircle = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        float finalSpeed = clockwise ? -speed * Time.deltaTime : speed * Time.deltaTime;
        angle += finalSpeed;

       
        if (clockwise)
        {
            while (angle < 0)
            {
                angle += 2 * Mathf.PI;                  //angle will be 0 to 2pi
                completeCircle = true;
            }
        }
        else
        {
            while (angle > 2 * Mathf.PI)
            {
                completeCircle = true;
                angle -= 2 * Mathf.PI;
            }
        }

        ball.transform.localPosition = new Vector3(circleRadius * Mathf.Cos(angle), circleRadius * Mathf.Sin(angle), 0);
        float eulerAngle = (angle * 180 / Mathf.PI) % 360;

        // Senses whether the target has escaped        
        if (clockwise && completeCircle && eulerAngle < minimumAngle)
        {
            Debug.Log("No Input while in clockwise ");
            GameManager.Instance.FailToClick();
            MarkNewTarget();
        }
        if (!clockwise && completeCircle && eulerAngle > maximumAngle)
        {
            Debug.Log("No Input while in counterClockwise");
            GameManager.Instance.FailToClick();
            MarkNewTarget();
        }
        if (Input.GetButtonDown("Fire1"))
        {
            //Debug.Log(minimumAngle + " < " + eulerAngle + " < " + maximumAngle);
            // Changes direction before verifying scoring requirements
            clockwise = !clockwise;
            // Verifies if the currentAngle
            if (eulerAngle > minimumAngle && eulerAngle < maximumAngle)
            {
                GameManager.Instance.Score();
                MarkNewTarget();
            }
            else
            {
                GameManager.Instance.FailToClick();
                MarkNewTarget();
            }
        }
    }

}
