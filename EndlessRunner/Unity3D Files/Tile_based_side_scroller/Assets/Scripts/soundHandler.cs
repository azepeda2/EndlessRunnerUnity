using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soundHandler : MonoBehaviour {

    private AudioSource[] _audioSources;

	// Use this for initialization
	void Start () {
        _audioSources = this.GetComponents<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void playSound(string type)
    {
        switch(type)
        {
            case "jump":
                _audioSources[0].Play();
                break;
            case "powerup":
                _audioSources[1].Play();
                break;
            case "die":
                _audioSources[2].Play();
                break;
            case "restart":
                _audioSources[3].Play();
                break;
        }
    }
}
