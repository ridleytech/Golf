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
    public Club currentClub;
    public List<Club> clubs;
    public Text currentClubTxt;
    public Text currentClubDistanceTxt;
    public int currentClubInd;
   
    string[] clubNames = { "DRIVER", "3 WOOD", "5 WOOD", "3 IRON", "4 IRON", "5 IRON", "6 IRON", "7 IRON", "8 IRON", "9 IRON", "PW","GW","SW","LW" };
    int[] clubDistances = { 296, 262, 248,228,219,209,197,185,172,159,146,135,124,113 };
    //float[] clubArchs = { 1.0f, .9f, .8f, .7f, .7f, .7, .7, 185, 172, 159, 146, 135, 124, 113 };
    int[] clubForces = { 296, 262, 248, 228, 219, 209, 197, 185, 172, 159, 146, 135, 124, 113 };

    public float currentArch = 1f;
    public int currentForce = 175;


    public class Club
    {
        public string name;
        public int distance;
        public int arch;
        public int force;
    }

    void Start()
    {
        bm = GameObject.Find("ball").GetComponent<BallManager>();
        shotsTimerLbl = GameObject.Find("releaseTime").GetComponent<Text>();
        pb = GameObject.Find("HealthBar").GetComponent<PowerBar>();
        clubs = new List<Club>();

        for (int i = 0; i < clubNames.Length-1; i++)
        {
            Club club1 = new Club();
            club1.name = clubNames[i];
            club1.distance = clubDistances[i];
            club1.force = clubForces[i];
            clubs.Add(club1);
        }

        currentClub = clubs[0];
        currentForce = currentClub.force - 121;
        bm.force = currentForce;
        currentClubTxt.text = currentClub.name;
        currentClubDistanceTxt.text = currentClub.distance.ToString() + " yds";
        //Club 3w = new Club();
        //3w.name = "3 WOOD";
        //3w.distance = 275;
        //3w.Add(nine);

        //Club nine = new Club();
        //nine.name = "5 WOOD";
        //nine.distance = 275;
        //cards.Add(nine);

        //currentClub = { name: "Driver",distance:""}

        origPos = ball.transform.position;

        //hitBall();

        shotsTimerLbl.text = "Shot Timer: 0";


    }

    public void changeArch(int dir)
    {

       
    }

    public void cycleClubs(int dir) {

        currentClubInd++;

        currentClub = clubs[currentClubInd];

        currentClubTxt.text = currentClub.name;
        currentClubDistanceTxt.text = currentClub.distance.ToString() + " yds";
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
