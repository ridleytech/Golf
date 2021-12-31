using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class ResetBall : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
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

    void Awake()
    {
        direction = Vector2.zero;
        touched = false;
    }

    void Start()
    {

    }

    public void OnPointerDown(PointerEventData data)
    {

        //print ("OnPointerDown");

        if (!touched)
        {
            //anim.SetBool("onDefense", false);
            //anim.SetBool("blocking", true);

            touched = true;
            pointerID = data.pointerId;
            origin = data.position;
            gm.resetBall();
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

            touched = false;
        }
    }
}