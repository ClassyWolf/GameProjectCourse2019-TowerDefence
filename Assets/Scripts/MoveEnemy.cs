using UnityEngine;
using System.Collections;

public class MoveEnemy : MonoBehaviour
{
    [SerializeField]private Animator animator;

    [HideInInspector]
    public GameObject[] waypoints;
    public float speed = 1.0f;
    public float slowDuration = 3;
    public bool slowed = false;

    [SerializeField] private float slowMultiplier = 1f;
    
    private Transform sprite;
    private int currentWaypoint = 0;
    private float lastWaypointSwitchTime;
    private float slowedDurationHolder;

    public float SlowedDurationHolder
    {
        get
        {
            return slowedDurationHolder;
        }
    }

    // Use this for initialization
    void Start()
    {
        lastWaypointSwitchTime = Time.time;
        sprite = GetComponentInChildren<Transform>();

        transform.position = waypoints[0].transform.position;
        slowedDurationHolder = slowDuration;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // From the waypoints array, you retrieve the start and end position for the current path segment.
        Vector3 endPosition = waypoints[currentWaypoint + 1].transform.position;

        // Calculate the time needed for the whole distance with the formula time = distance / speed, 
        //then determine the current time on the path. Using Vector2.Lerp, you interpolate the current 
        //position of the enemy between the segment's start and end positions.
        if (slowed == false)
        {
            NormalMove(endPosition);
        }

        else if(slowed == true)
        {

            SlowedMove(endPosition);
            slowDuration -= Time.deltaTime;

            if(slowDuration < 0)
            {
                slowed = false;
                slowDuration = slowedDurationHolder;
            }
        }

        //calculates current movement direction by subtracting the current waypoint’s position from that of the next waypoint.
        Vector3 newStartPosition = waypoints[currentWaypoint].transform.position;
        Vector3 newEndPosition = waypoints[currentWaypoint + 1].transform.position;
        Vector3 newDirection = (newEndPosition - newStartPosition);

        //It uses Mathf.Atan2 to determine the angle toward which newDirection points, in radians, 
        //assuming zero points to the right. Multiplying the result by 180 / Mathf.PI converts the angle to degrees.
        float x = newDirection.x;
        float y = newDirection.y;
        float rotationAngle = Mathf.Atan2(y, x) * 180 / Mathf.PI;

        // Check whether the enemy has reached the endPosition. If yes, handle these two possible scenarios:
        //The enemy is not yet at the last waypoint, so increase currentWaypoint and update lastWaypointSwitchTime.
        //The enemy reached the last waypoint, so this destroys it and triggers a sound effect. 
        if (gameObject.transform.position.Equals(endPosition))
        {
            if (currentWaypoint < waypoints.Length - 2)
            {
                // Switch to next waypoint
                currentWaypoint++;
                lastWaypointSwitchTime = Time.time;

                RotateIntoMoveDirection();
            }
            else
            {
                // Destroy enemy
                Destroy(gameObject);
            }
        }
    }

    private void RotateIntoMoveDirection()
    {
        //calculates current movement direction by subtracting the current waypoint’s position from that of the next waypoint.
        Vector3 newStartPosition = waypoints[currentWaypoint].transform.position;
        Vector3 newEndPosition = waypoints[currentWaypoint + 1].transform.position;
        Vector3 newDirection = (newEndPosition - newStartPosition);



        //It uses Mathf.Atan2 to determine the angle toward which newDirection points, in radians, 
        //assuming zero points to the right. Multiplying the result by 180 / Mathf.PI converts the angle to degrees.
        float x = newDirection.x;
        float y = newDirection.y;
        float rotationAngle = Mathf.Atan2(y, x) * 180 / Mathf.PI;

        if(rotationAngle == 0)
        {
            animator.SetInteger("rotationAngel", 0);
            sprite.localScale = new Vector3(1, 1, 1);
        }

        else if(rotationAngle == 180)
        {
            animator.SetInteger("rotationAngel", 0);
            sprite.localScale = new Vector3(-1, 1, 1);
        }

        else if(rotationAngle == -90)
        {
            animator.SetInteger("rotationAngel", -90);
            sprite.localScale = new Vector3(1, 1, 1);
        }
        
        else if(rotationAngle == 90)
        {
            animator.SetInteger("rotationAngel", 90);
            sprite.localScale = new Vector3(1, 1, 1);
        }


        //Finally, it retrieves the child named Sprite and rotates it rotationAngle degrees along the z-axis. 
        //Note that you rotate the child instead of the parent so the health bar — you’ll add it soon — remains horizontal.
        //GameObject sprite = gameObject.transform.Find("Sprite").gameObject;
        //sprite.transform.rotation = Quaternion.AngleAxis(rotationAngle, Vector3.forward);
        //Debug.Log("Rotation angle: " + rotationAngle);
    }

    private void NormalMove(Vector3 endPosition)
    {
        slowMultiplier = 1f;
        gameObject.transform.position = Vector2.MoveTowards(gameObject.transform.position, endPosition, Time.deltaTime * speed * slowMultiplier);
    }

   private void SlowedMove(Vector3 endPosition)
    {
        slowMultiplier = 0.5f;
        gameObject.transform.position = Vector2.MoveTowards(gameObject.transform.position, endPosition, Time.deltaTime * speed * slowMultiplier);
    }
}
