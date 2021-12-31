using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitGround : MonoBehaviour {

    public GameManager gm;
    public BallManager bm;
    public GetTerrainTexture tt;
    public AudioClip[] clips;
    AudioClip clip;
    public AudioSource source;

    void Start () {

        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        bm = GameObject.Find("ball").GetComponent<BallManager>();
        source = GetComponent<AudioSource>();
    }

    void OnTriggerEnter (Collider other) {

        //if(other.gameObject.name == "ball")
        //{
        //    print("hit ground");
        //    BallManager bm = other.gameObject.GetComponent<BallManager>();
        //    bm.hitGround = true;
        //}

        if (other.gameObject.name.Contains("Terrain") && gm.ballHit)
        {
            //print("hit ground");

            //BallManager bm = other.gameObject.GetComponent<BallManager>();

            if (!bm.hitGround)
            {
                if (tt.terrainName.Contains("fairway"))
                {
                    clip = clips[0];
                }
                else
                {
                    clip = clips[1];
                }

                source.clip = clip;

                source.Play();

                bm.hitGround = true;
            }
        }
    }
	
	// Update is called once per frame
	//void Update () {
		
	//}
}
