using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour {

	[SerializeField] GameObject pauseScreen;
	private float stopTime;
	private bool paused;
	private float lastTime;
	private int currentSprite = 0;
	[SerializeField] Sprite[] sprites;
	[SerializeField] Image image;
	// Use this for initialization
	void Start () {
		pauseScreen.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown("p"))
		{
			lastTime = Time.realtimeSinceStartup;
			paused = !paused;
			if (paused)
			{
				Pause();
			}
			if (!paused)
			{
				Continue();
			}
		}

	
	}
	public void Pause()
	{
		pauseScreen.SetActive(true);
		StatsManager.instance.EnableStatBarImages();
		Time.timeScale = 0.0f;

	}
	public void Continue()
	{
		pauseScreen.SetActive(false);
		StatsManager.instance.DisableStatBarImages();
		Time.timeScale = 1.0f;
	}
	public void Restart()
	{
		//reload the current level the user is playing
		Application.LoadLevel(Application.loadedLevel);
	}
	public void Quit()
	{
		//load the index of zero which will eventually be a main menu
		Application.LoadLevel (0);
	}
	public void AnimateSprites()
	{
		var currentTime = Time.realtimeSinceStartup;
		if (currentTime - lastTime >= 0.1f)
		{
			lastTime = currentTime;
			currentSprite++;
			
			if (currentSprite >= sprites.Length)
			{
				currentSprite = 0;
			}
		}
		image.sprite = sprites[currentSprite];
		
		
	}
}