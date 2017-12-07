using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

	public AudioSource audioManager;

	public void Play()
	{
		audioManager.Play ();
	}

	public void Pause()
	{
		audioManager.Pause ();
	}

	void Awake ()
	{
		DontDestroyOnLoad(transform.gameObject);
        Cursor.visible = false;
	}
}
