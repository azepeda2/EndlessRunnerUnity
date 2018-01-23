using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class crateScript : MonoBehaviour {

    private float maxY;
    private float minY;
    private int direction = 1;
    private SpriteRenderer crateRender;
    public bool inPlay = true;
    private bool releaseCrate = false;

	// Use this for initialization
	void Start () {
        maxY = this.transform.position.y + 0.5f;
        minY = maxY - 1.0f;

        crateRender = this.transform.GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
        this.transform.position = new Vector2(this.transform.position.x, this.transform.position.y + (direction * 0.05f));

        if (this.transform.position.y > maxY)
        {
            direction = -1;
        }
        else if (this.transform.position.y < minY)
        {
            direction = 1;
        }

        if (!inPlay && !releaseCrate)
        {
            respawnCrate();
        }
	}

    private void respawnCrate()
    {
        releaseCrate = true;
        Invoke("placeCrate", (float)Random.Range(3, 10));
    }

    private void placeCrate()
    {
        inPlay = true;
        releaseCrate = false;

        GameObject tmpTile = GameObject.Find("Camera").GetComponent<levelCreatorv2>().tilePos;
        this.transform.position = new Vector2(tmpTile.transform.position.x, tmpTile.transform.position.y + 5.5f);
        maxY = this.transform.position.y + 0.5f;
        minY = maxY - 1.0f;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Player hit crate! Crate value: " + crateRender.sprite.name);
            switch (crateRender.sprite.name)
            {
                case "crates_0":
                    GameObject.Find("Camera").GetComponent<levelCreatorv2>().modGameSpeed(-1.0f);
                    break;
                case "crates_1":
                    GameObject.Find("Player").GetComponent<Rigidbody2D>().AddForce(Vector2.up * 6000);
                    break;
                case "crates_2":
                    GameObject.Find("Camera").GetComponent<scoreHandler>().Points += 10;
                    break;
            }

            inPlay = false;
            this.transform.position = new Vector2(this.transform.position.x, this.transform.position.y + 30.0f);

            GameObject.Find("Camera").GetComponent<soundHandler>().playSound("powerup");
        }
    }
}
