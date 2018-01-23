using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scoreHandler : MonoBehaviour {

    private int _score = 0;
    private int _bestScore;

	// Use this for initialization
	void Start () {
        _bestScore = getHighScoreFromDB();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnGUI()
    {
        GUI.color = Color.white;

        GUIStyle _style = GUI.skin.GetStyle("label");
        _style.alignment = TextAnchor.UpperLeft;
        _style.fontSize = 20;

        GUI.Label(new Rect(20, 20, 200, 200),"Score: " + _score.ToString(), _style);
        _style.alignment = TextAnchor.UpperRight;
        GUI.Label(new Rect(Screen.width - 220, 20, 200, 200), "High Score: " + _bestScore.ToString(), _style);
    }

    public int Points
    {
        get { return _score; }
        set { _score = value; }
    }

    static string Md5Sum(string s)
    {
        s += GameObject.Find("xxmd5").transform.GetChild(0).name;

        System.Security.Cryptography.MD5 hash = System.Security.Cryptography.MD5.Create();
        byte[] data = hash.ComputeHash(System.Text.Encoding.Default.GetBytes(s));

        System.Text.StringBuilder stringB = new System.Text.StringBuilder();
        for (int i = 0; i < data.Length; i++)
        {
            stringB.Append(data[i].ToString("x2"));
        }

        return stringB.ToString();
    }

    public void saveValue(int value)
    {
        string tmpV = Md5Sum(value.ToString());
        PlayerPrefs.SetString("score_hash", tmpV);
        PlayerPrefs.SetInt("score", value);
    }

    private int dec(string val)
    {
        int tmp = 0;

        if (val == "")
        {
            saveValue(tmp);
        }
        else
        {
            if (val.Equals(Md5Sum(PlayerPrefs.GetInt("score").ToString())))
            {
                tmp = PlayerPrefs.GetInt("score");
            }
            else
            {
                saveValue(0);
            }
        }

        return tmp;
    }

    private int getHighScoreFromDB()
    {
        return dec(PlayerPrefs.GetString("score_hash"));
    }

    public void updateHighScore()
    {
        if (_score > _bestScore)
        {
            saveValue(_score);
        }
    }
}
