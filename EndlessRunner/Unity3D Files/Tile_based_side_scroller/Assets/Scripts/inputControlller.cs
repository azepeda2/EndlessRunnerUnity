using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inputControlller : MonoBehaviour {

    private bool isMobile = true;
    private playerHandler _player;

	// Use this for initialization
	void Start () {
        if (Application.isEditor)
            isMobile = false;

        _player = GameObject.Find("Player").GetComponent<playerHandler>();
		
	}
	
	// Update is called once per frame
	void Update () {
        if (isMobile)
        {
            if (Input.GetKeyUp(KeyCode.Escape))
            {
                if (Application.platform == RuntimePlatform.Android)
                {
                    AndroidJavaObject activity = new AndroidJavaClass("com.unity3d.player.UnityPlayer").GetStatic<AndroidJavaObject>("currentActivity");
                    activity.Call<bool>("moveTaskToBack", true);
                }
                else
                {
                    Application.Quit();
                }
            }

            int tmpC = Input.touchCount;
            tmpC--;

            if (Input.GetTouch(tmpC).phase == TouchPhase.Began)
            {
                handleInteraction(true);
            }
            else if (Input.GetTouch(tmpC).phase == TouchPhase.Ended)
            {
                handleInteraction(false);
            }
        }
        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                handleInteraction(true);
            }
            else if (Input.GetMouseButtonUp(0))
            {
                handleInteraction(false);
            }
        }
	}

    void handleInteraction(bool starting)
    {
        if (starting)
        {
            _player.jump();
        }
        else
        {
            _player.jumpPress = false;
        }
    }
}
