using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyScript : MonoBehaviour {

	// Use this for initialization
	void Start () {

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("hit player");
            GameObject tmpPlayer = GameObject.Find("Player");

            tmpPlayer.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 2000);
            tmpPlayer.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 2000);
            tmpPlayer.GetComponent<Collider2D>().enabled = false;
            GameObject.Find("Camera").GetComponent<soundHandler>().playSound("die");
        }
    }
}
