using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class BallManager : MonoBehaviour
{

    public GameObject target;
    public bool wasShot;
    public float shotSpeed;
    public float shotArc;
    public Vector3 ballPos;
    public Transform shootPos;
    public GameObject ball;
    public float shotTimer;
    public float shotSpeedInterval;
    public GameObject markerGO;
    public bool wasMade;
    private Vector3 shotPosition;
    public GameManager gm;
    private float repositionTimer;
    //private float repositionTime;
    public GameObject hand;
    public bool isShooting;
    public Rigidbody rb;
    public bool hitGround;
    public bool usingBM;


    public float force;
    public float height = 100;
    public float xOffset = 2.5f;
    public float zOffset = 2.5f;
    public bool isHook;
    public bool ballStopped;
    public PowerBar pb;
    public bool leftGround;
    public AudioSource source;
    public AudioClip[] clips;
    public GameObject hole;

    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        pb = GameObject.Find("HealthBar").GetComponent<PowerBar>();

        //shotSpeed = 20f;
        //shotArc = 1f;
        shotSpeedInterval = 3f;

        ball = transform.gameObject;
        rb = transform.GetComponent<Rigidbody>();

        //repositionTime = 2f;

        target = GameObject.Find("target");
        hole = GameObject.Find("hole");

        ballPos = ball.transform.position;
    }

    public void hookBall() {

        print("hook");

        Vector3 direction;

        if (isHook)
        {
            direction = ball.transform.right;
        }
        else
        {
            direction = -ball.transform.right;
        }

        rb.AddForce(direction * (force / 4));
    }

    void Update()
    {
        if (isShooting)
        {
            ballStopped = false;

            //if (Input.GetButton ("Shoot")) {

            //print ("shotTimer");

            shotTimer += Time.deltaTime;

            //print("shotTimer: "+shotTimer);

            gm.shotsTimerLbl.text = shotTimer.ToString();

            if(pb._power <= 150)
            {
                pb._power = shotTimer * 150;
            }
            else
            {
                pb._power = 150 - shotTimer * 150;
            }
        }

        //gets ball distance to marker

        //float distanceToMarker = Vector3.Distance(ball.transform.position, markerGO.transform.position);

        //float distanceX = Mathf.Abs(ball.transform.position.x - markerGO.transform.position.x);
        //float distanceZ = Mathf.Abs(ball.transform.position.z - markerGO.transform.position.z);

        float distanceToRim = Vector3.Distance(ball.transform.position, target.transform.position);

        if (!wasShot)
        {
            //transform.position = ball.transform.position;
        }

        if (wasShot == false && (!isShooting && gm.ballHit) && usingBM) // && !ballStopped
        {
            //print("adding force");
            rb.isKinematic = false;

            //transform.position = shootPos.position;

            //float off = 0.15f; //35%

            float randomXPos = Random.Range (-xOffset, xOffset);
            float randomZPos = Random.Range (-zOffset, zOffset);

            float xShotResult = xOffset - shotTimer;
            float zShotResult = zOffset - shotTimer;

            //float shotResult = 0f;
            //shotSpeed = 20f;

            //print("xShotResult: " + xShotResult);
            //print("zShotResult: " + zShotResult);

            //print ("randomPos: " + randomPos);

            //print("go");

            Vector3 aimPosition = new Vector3(target.transform.position.x + xShotResult,
                                      target.transform.position.y,
                                      target.transform.position.z + zShotResult);

            //print("aimPosition: " + aimPosition);

            //Vector3 pos = findInitialVelocity(ball.transform.position, aimPosition);

            ////print ("pos: "+pos);

            //rb.AddForce(pos * shotSpeed);

            //Vector3 shoot = (target.transform.position - ball.transform.position).normalized;
            Vector3 shoot = (aimPosition - ballPos).normalized;

            float power;

            if(shotTimer <= 1)
            {
                power = force * shotTimer;
            }
            else
            {
                power = force * .5f;
            }

            print("power: " + power + " force: " + force);

            //rb.AddForce(shoot * force);
            rb.AddForce(shoot * power);
            rb.AddForce(ball.transform.up * height);


            //if (!hitGround)
            //{

            //}
            //else
            //{
            //    print("allow roll");
            //}

            wasShot = true;

            shotPosition = ball.transform.position;
            gm.totalShots++;
            //gm.totalGameShots++;

            if (gm.totalShots == 1) {
                //print("move target");
                target.transform.position = new Vector3(hole.transform.position.x+1, hole.transform.position.y, hole.transform.position.z-1);
            }

            //print("shotTimer: "+shotTimer);

            shotTimer = 0;

            //print("mag: "+ rb.velocity.magnitude);
        }

        //check if in flight

        if (!leftGround)
        {
            float abs = Mathf.Abs(ball.transform.position.y - ballPos.y);

            //print("abs:" + abs);

            if (wasShot && (abs > 3))
            {
                leftGround = true;
            }
        }


        //print("mag" + rb.velocity.magnitude);

        if (rb.velocity.magnitude == 0 && leftGround && !ballStopped && hitGround)
        {
            //print("ball stopped");

            //uncomment when tracking hits

            //ballPos = ball.transform.position;

            ResetBall();
        }

        //print ("inMarker: "+inMarker);

        //print ("distanceToMarker: " + distanceToMarker);
        //print ("distanceX: " + distanceX + " distanceZ: " + distanceZ);
        //print ("distanceToRim: " + distanceToRim);

        //if (transform.position.y < -1f) //if ball goes below floor
        //{
        //    transform.position = shootPos.position;
        //}
    }

    public void ResetBall() {

        //print("ResetBall");

        ballStopped = true;
        wasShot = false;
        leftGround = false;
        gm.ballHit = false;
        usingBM = false;
        hitGround = false;
        rb.drag = 0;
        //rb.gameObject.transform.LookAt(target.transform);
        var targetRotation = Quaternion.LookRotation(target.transform.position - rb.gameObject.transform.position);
        rb.gameObject.transform.rotation = targetRotation;
        ballPos = ball.transform.position;
    }

    //public void resetBall()
    //{
    //    //print ("reset");

    //    wasShot = false;
    //    isShooting = false;
    //    gm.ballHit = false;
    //}

    /*
 * Finds the initial velocity of a projectile given the initial positions and some offsets
 * @param Vector3 startPosition - the starting position of the projectile
 * @param Vector3 finalPosition - the position that we want to hit
 * @param float maxHeightOffset (default=0.6f) - the amount we want to add to the height for short range shots. 
 * We need enough clearance so the
 * ball will be able to get over the rim before dropping into the target position
 * @param float rangeOffset (default=0.11f) - the amount to add to the range to 
 * increase the chances that the ball will go through the rim
 * @return Vector3 - the initial velocity of the ball to make it hit the target 
 * under the current gravity force.
 */
    private Vector3 findInitialVelocity(Vector3 startPosition, Vector3 finalPosition, float maxHeightOffset = 0.6f, float rangeOffset = 0.11f)
    {
        // get our return value ready. Default to (0f, 0f, 0f)
        Vector3 newVel = new Vector3();

        // Find the direction vector without the y-component
        Vector3 direction = new Vector3(finalPosition.x, 0f, finalPosition.z) - new Vector3(startPosition.x, 0f, startPosition.z);

        // Find the distance between the two points (without the y-component)
        float range = direction.magnitude;

        // Add a little bit to the range so that the ball is aiming at hitting the back of the rim.
        // Back of the rim shots have a better chance of going in.
        // This accounts for any rounding errors that might make a shot miss (when we don't want it to).
        range += rangeOffset;

        // Find unit direction of motion without the y component
        Vector3 unitDirection = direction.normalized;

        // Find the max height
        // Start at a reasonable height above the target, so short range shots will have enough clearance to go in the basket
        // without hitting the front of the rim on the way up or down.
        float maxYPos = target.transform.position.y + maxHeightOffset;

        // check if the range is far enough away where the shot may have flattened out enough to hit the front of the rim
        // if it has, switch the height to match a 45 degree launch angle
        if (range / 2f > maxYPos)
            maxYPos = range / 2f;

        // find the initial velocity in y direction
        newVel.y = Mathf.Sqrt(-2.0f * Physics.gravity.y * (maxYPos - startPosition.y));

        // find the total time by adding up the parts of the trajectory
        // time to reach the max
        float timeToMax = Mathf.Sqrt(-2.0f * (maxYPos - startPosition.y) / Physics.gravity.y);

        // time to return to y-target
        float timeToTargetY = Mathf.Sqrt(-2.0f * (maxYPos - finalPosition.y) / Physics.gravity.y);

        // add them up to find the total flight time
        float totalFlightTime = timeToMax + timeToTargetY;

        // find the magnitude of the initial velocity in the xz direction
        float horizontalVelocityMagnitude = range / totalFlightTime;

        // use the unit direction to find the x and z components of initial velocity
        newVel.x = horizontalVelocityMagnitude * unitDirection.x;
        newVel.z = horizontalVelocityMagnitude * unitDirection.z;

        return newVel;
    }
}
