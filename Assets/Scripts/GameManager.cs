using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public Rigidbody rb;
    public GameObject ball;
    public GameObject[] gms;
    public float force = 500;
    public float height = 100;
    public Transform target;

    public Vector3 origPos;
    public bool ballHit;

    public Text shotsTimerLbl;
    [SerializeField] Text shotsMadeLbl;
    [SerializeField] Text playerLbl;

    public BallManager bm;
    public GameObject placeholderBall;
    public PowerBar pb;
    public int totalShots;

    void Start()
    {
        bm = GameObject.Find("ball").GetComponent<BallManager>();
        shotsTimerLbl = GameObject.Find("releaseTime").GetComponent<Text>();
        pb = GameObject.Find("HealthBar").GetComponent<PowerBar>();

        origPos = ball.transform.position;

        //hitBall();

        shotsTimerLbl.text = "Shot Timer: 0";
    }

    public void resetBall()
    {
        GameObject pb1 = Instantiate(placeholderBall, ball.transform.position, Quaternion.identity);

        print("Reset");
        ballHit = false;
        rb.isKinematic = true;
        ball.transform.position = origPos;

        bm.wasShot = false;
        bm.isShooting = false;
        //gm.ballHit = false;

        bm.hitGround = false;
        pb._power = 0;
        shotsTimerLbl.text = "Shot Timer: 0";

        bm.leftGround = false;
        bm.ballStopped = false;
    }

    public void hitBall()
    {
        print("hitBall");

        rb.isKinematic = false;
        //rb.mass = 1;

        ballHit = true;

        Vector3 shoot = (target.position - ball.transform.position).normalized;

        rb.AddForce(shoot * force);
        rb.AddForce(ball.transform.up * height);
    }


    // Update is called once per frame
    //void Update()
    //{

    //}

}
