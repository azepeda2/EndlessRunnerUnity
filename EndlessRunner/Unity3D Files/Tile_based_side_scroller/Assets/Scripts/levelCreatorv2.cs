using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class levelCreatorv2 : MonoBehaviour {

    // Use this for initialization
    private GameObject collectedTiles;
    private const float tileWidth = 1.25f;
    private float startUpPosY;
    private int heightLevel = 0;
    public GameObject tilePos;
    private GameObject tmpTile;
    private GameObject gameLayer;
    private GameObject bgLayer;
    private GameObject _player;
    private float gameSpeed = 4.0f;
    private float outOfBoundsX;
    private int blankCounter = 0;
    private int middleCounter = 0;
    private string lastTile = "right";
    private float startTime;
    private float outOfBoundsY;
    private bool enemyAdded = false;
    private bool playerDead = false;

    private void Awake()
    {
        Application.targetFrameRate = 60;
    }

    void Start()
    {
        Debug.Log("script v2 is running properly");
        gameLayer = GameObject.Find("gameLayer");
        bgLayer = GameObject.Find("backgroundLayer");
        collectedTiles = GameObject.Find("tiles");

        for (int i = 0; i < 25; i++)
        {
            GameObject tmpG1 = Instantiate(Resources.Load("ground_left", typeof(GameObject))) as GameObject;
            tmpG1.transform.parent = collectedTiles.transform.Find("gLeft").transform;
            tmpG1.transform.position = Vector2.zero;
            GameObject tmpG2 = Instantiate(Resources.Load("ground_middle", typeof(GameObject))) as GameObject;
            tmpG2.transform.parent = collectedTiles.transform.Find("gMiddle").transform;
            tmpG2.transform.position = Vector2.zero;
            GameObject tmpG3 = Instantiate(Resources.Load("ground_right", typeof(GameObject))) as GameObject;
            tmpG3.transform.parent = collectedTiles.transform.Find("gRight").transform;
            tmpG3.transform.position = Vector2.zero;
            GameObject tmpG4 = Instantiate(Resources.Load("blank", typeof(GameObject))) as GameObject;
            tmpG4.transform.parent = collectedTiles.transform.Find("gBlank").transform;
            tmpG4.transform.position = Vector2.zero;
        }

        for (int i = 0; i < 10; i++)
        {
            GameObject tmpG5 = Instantiate(Resources.Load("enemy", typeof(GameObject))) as GameObject;
            tmpG5.transform.parent = collectedTiles.transform.Find("killers").transform;
            tmpG5.transform.position = Vector2.zero;
        }

        collectedTiles.transform.position = new Vector2(-60.0f, -20.0f);

        tilePos = GameObject.Find("startTilePosition");
        startUpPosY = tilePos.transform.position.y;
        outOfBoundsX = tilePos.transform.position.x - 6.0f;
        outOfBoundsY = startUpPosY - 3.0f;
        _player = GameObject.Find("Player");

        fillScene();
        startTime = Time.time;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Every five seconds increase the pace of the game
        if (startTime - Time.time % 5 == 0)
        {
            gameSpeed += 0.5f;
        }

        gameLayer.transform.position = new Vector2(gameLayer.transform.position.x - gameSpeed * Time.deltaTime, 0);
        bgLayer.transform.position = new Vector2(bgLayer.transform.position.x - gameSpeed / 4 * Time.deltaTime, 0);

        foreach (Transform child in gameLayer.transform)
        {
            if (child.position.x < outOfBoundsX)
            {
                switch (child.gameObject.name)
                {
                    case "ground_left(Clone)":
                        child.gameObject.transform.position = collectedTiles.transform.Find("gLeft").transform.position;
                        child.gameObject.transform.parent = collectedTiles.transform.Find("gLeft").transform;
                        break;
                    case "ground_right(Clone)":
                        child.gameObject.transform.position = collectedTiles.transform.Find("gRight").transform.position;
                        child.gameObject.transform.parent = collectedTiles.transform.Find("gRight").transform;
                        break;
                    case "ground_middle(Clone)":
                        child.gameObject.transform.position = collectedTiles.transform.Find("gMiddle").transform.position;
                        child.gameObject.transform.parent = collectedTiles.transform.Find("gMiddle").transform;
                        break;
                    case "blank(Clone)":
                        child.gameObject.transform.position = collectedTiles.transform.Find("gBlank").transform.position;
                        child.gameObject.transform.parent = collectedTiles.transform.Find("gBlank").transform;
                        break;
                    case "enemy(Clone)":
                        child.gameObject.transform.position = collectedTiles.transform.Find("killers").transform.position;
                        child.gameObject.transform.parent = collectedTiles.transform.Find("killers").transform;
                        break;
                    case "Reward":
                        GameObject.Find("Reward").GetComponent<crateScript>().inPlay = false;
                        break;
                    default:
                        Destroy(child.gameObject);
                        break;
                }
            }
        }
        if (gameLayer.transform.childCount < 25) spawnTile();

        if (_player.transform.position.y < outOfBoundsY) killPlayer();

    }

    private void spawnTile()
    {
        if (blankCounter > 0)
        {
            setTile("blank");
            blankCounter--;
            return;
        }

        if (middleCounter > 0)
        {
            randomizeEnemy();
            setTile("middle");
            middleCounter--;
            return;
        }

        enemyAdded = false;

        if (lastTile == "blank")
        {
            changeHeight();
            setTile("left");
            middleCounter = (int)Random.Range(1, 8);
        }
        else if (lastTile == "right")
        {
            this.GetComponent<scoreHandler>().Points++;
            blankCounter = (int)Random.Range(1, 3);
        }
        else if (lastTile == "middle")
        {
            setTile("right");
        }
    }

    private void changeHeight()
    {
        int newHeight = (int)Random.Range(0, 4);

        if (newHeight < heightLevel)
            heightLevel--;
        else if (newHeight > heightLevel)
            heightLevel++;
    }

    private void fillScene()
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
        lastTile = type;
    }

    private void randomizeEnemy()
    {
        if (!enemyAdded)
        {
            if ((int)Random.Range(0, 4) == 1)
            {
                GameObject newEnemy = collectedTiles.transform.Find("killers").transform.GetChild(0).gameObject;
                newEnemy.transform.parent = gameLayer.transform;
                newEnemy.transform.position = new Vector2(tilePos.transform.position.x + tileWidth, startUpPosY + (heightLevel * tileWidth + (tileWidth * 2)));
                enemyAdded = true;
            }
        }
        else
        {
            return;
        }
    }

    private void killPlayer()
    {
        if (playerDead) return;

        playerDead = true;
        this.GetComponent<scoreHandler>().updateHighScore();
        this.GetComponent<soundHandler>().playSound("restart");
        Invoke("reloadScene", 1);
    }

    private void reloadScene()
    {
        SceneManager.LoadScene(0);
    }

    public float getGameSpeed()
    {
        return gameSpeed;
    }

    public void modGameSpeed(float deltaSpeed)
    {
        gameSpeed += deltaSpeed;
    }


}
