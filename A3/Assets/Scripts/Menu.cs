using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour {

	public void StartGame()
	{
		Application.LoadLevel (1);
	}

	public void Quit()
	{
		Application.Quit ();
	}

	public void MainMenu()
	{
		Application.LoadLevel (0);
	}
}
