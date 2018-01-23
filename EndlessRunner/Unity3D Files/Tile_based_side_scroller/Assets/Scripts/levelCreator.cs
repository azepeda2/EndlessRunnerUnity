using UnityEngine;
using System.Collections;

public class levelCreator : MonoBehaviour {

	// Use this for initialization
	private GameObject collectedTiles;
	private const float tileWidth= 1.25f;
    private float startUpPosY;
    public GameObject tilePos;
    private GameObject tmpTile;

    private int heightLevel = 0;

	private GameObject gameLayer;
	private GameObject bgLayer;

	void Start () 
	{
        Debug.Log("script is running properly");
		gameLayer = GameObject.Find("gameLayer");
		bgLayer = GameObject.Find("backgroundLayer");
		collectedTiles = GameObject.Find("tiles");
		
        for (int i = 0; i < 21; i++)
        {
            GameObject tmpG1 = Instantiate(Resources.Load("ground_left", typeof(GameObject))) as GameObject;
            tmpG1.transform.parent = collectedTiles.transform.Find("gLeft").transform;
            GameObject tmpG2 = Instantiate(Resources.Load("ground_middle", typeof(GameObject))) as GameObject;
            tmpG2.transform.parent = collectedTiles.transform.Find("gMiddle").transform;
            GameObject tmpG3 = Instantiate(Resources.Load("ground_right", typeof(GameObject))) as GameObject;
            tmpG3.transform.parent = collectedTiles.transform.Find("gRight").transform;
            GameObject tmpG4 = Instantiate(Resources.Load("blank", typeof(GameObject))) as GameObject;
            tmpG4.transform.parent = collectedTiles.transform.Find("gBlank").transform;
        }

        collectedTiles.transform.position = new Vector2(-60.0f, -20.0f);

        tilePos = GameObject.Find("startTilePosition");
        startUpPosY = tilePos.transform.position.y;
        fillScene();

    }

	// Update is called once per frame
	void FixedUpdate () 
	{

		

	}

	private	void fillScene()
	{
        for (int i = 0; i < 15; i++)
        {
            setTile("middle");
        }

        setTile("right");
	}

	private void setTile(string type)
	{
		switch (type)
        {
            case "left":
                tmpTile = collectedTiles.transform.Find("gLeft").transform.GetChild(0).gameObject;
                break;
            case "right":
                tmpTile = collectedTiles.transform.Find("gRight").transform.GetChild(0).gameObject;
                break;
            case "middle":
                tmpTile = collectedTiles.transform.Find("gMiddle").transform.GetChild(0).gameObject;
                break;
            case "blank":
                tmpTile = collectedTiles.transform.Find("gBlank").transform.GetChild(0).gameObject;
                break;
        }

        tmpTile.transform.parent = gameLayer.transform;
        tmpTile.transform.position = new Vector2(tilePos.transform.position.x + (tileWidth), startUpPosY + (heightLevel * tileWidth));

        tilePos = tmpTile;
	}


}
