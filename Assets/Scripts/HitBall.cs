using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class HitBall : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{

    private Vector2 origin;
    private Vector2 direction;
    private Vector2 smoothDirection;
    public bool touched;
    private int pointerID;
    public float jumpForce;
    bool canJump;
    int touchCount;

    public GameManager gm;
    public BallManager bm;
    public AudioSource source;

    void Awake()
    {
        direction = Vector2.zero;
        touched = false;
    }

    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        bm = GameObject.Find("ball").GetComponent<BallManager>();
        source = GetComponent<AudioSource>();
    }

    public void OnPointerDown(PointerEventData data)
    {
        //print ("OnPointerDown");

        if (!touched && gm.ballHit == false)
        {
            //anim.SetBool("onDefense", false);
            //anim.SetBool("blocking", true);

            touched = true;
            pointerID = data.pointerId;
            origin = data.position;

            bool usingTimer = true;

            if (usingTimer)
            {
                bm.isShooting = true;
                bm.usingBM = true;
            }
            else
            {
                gm.hitBall();
            }
        }
        else
        {
            bm.hookBall();
        }
    }

    public void OnDrag(PointerEventData data)
    {

        if (data.pointerId == pointerID)
        {
            Vector2 currentPosition = data.position;
            Vector2 directionRaw = currentPosition - origin;
            direction = directionRaw.normalized;

            //Debug.Log ("direction: "+direction);
        }
    }

    public void OnPointerUp(PointerEventData data)
    {
        if (data.pointerId == pointerID)
        {
            direction = Vector3.zero;

            //shoot.isShooting = false;

            //defAnim.SetBool("onDefense", false);
            //defAnim.SetBool("runToPosition", true);

            if (!gm.ballHit)
            {
                source.Play();
                gm.ballHit = true;
                bm.isShooting = false;
                touched = false;
            }  
        }
    }
}