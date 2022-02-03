using UnityEngine;
using UnityEngine.UI;

public class Ball : MonoBehaviour {
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private AudioSource _source;
    [SerializeField] private AudioClip[] _clips;
    [SerializeField] private GameObject _poofPrefab;

    private bool _isGhost;
    bool peaked;
    public Camera fallCam;
    public void Init(Vector3 velocity, bool isGhost) {
        _isGhost = isGhost;
        _rb.AddForce(velocity, ForceMode.Impulse);
    }
    public GameObject hole;

    public float distance;
    public Text distanceTxt;
    public Cannon cannon;

    public void OnCollisionEnter(Collision col) {
        if (_isGhost) return;
        Instantiate(_poofPrefab, col.contacts[0].point, Quaternion.Euler(col.contacts[0].normal));
        _source.clip = _clips[Random.Range(0, _clips.Length)];
        _source.Play();
    }

    private void Start()
    {
        distance = Vector3.Distance(hole.transform.position, gameObject.transform.position);
        distanceTxt.text = (int)distance + " yds";
    }

    private void Update()
    {
        //print("h: "+gameObject.transform.position.y);

        if (!peaked && gameObject.transform.position.y > 15) {

            peaked = true;
        }

        if (peaked && gameObject.transform.position.y < 15 && cannon.isShot) {
            print("show fall cam");
            fallCam.depth = 2;
        }

        fallCam.transform.LookAt(gameObject.transform);

        if (cannon.isShot) {
            distance = Vector3.Distance(hole.transform.position, gameObject.transform.position);
            distanceTxt.text = (int)distance + " yds";
        }

        if (_rb.velocity.magnitude == 0 && cannon.isShot)
        {
            cannon.isShot = false;
            print("ball stopped");
            fallCam.depth = -1;
        }

    }
}