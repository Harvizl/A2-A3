using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class AudioManager : MonoBehaviour {

    public AudioSource audioManager { get { return GetComponent<AudioSource>(); } }
    private Button button { get { return GetComponent<Button>(); } }

    public AudioClip sound;
	public AudioClip coin;
	public AudioClip jump;
	public AudioClip jumpOn;
	public AudioClip fire;
	public AudioClip hit;
	public AudioClip pop;
	public AudioClip gameOverBGM;

    void Start()
    {
        //gameObject.AddComponent<AudioSource>();
		//audioManager.clip = sound;

    }

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
		//DontDestroyOnLoad(transform.gameObject);
        Cursor.visible = false;
		audioManager.playOnAwake = false;
	}

    public void PlaySound()
    {
        audioManager.PlayOneShot(sound);
    }
		
	public void StartSound()
	{
		audioManager.PlayOneShot (coin);
	}

	public void OptionsSound()
	{
		audioManager.PlayOneShot (jump);
	}

	public void BackSound()
	{
		audioManager.PlayOneShot (hit);
	}
}
