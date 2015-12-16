using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StatsManager : MonoBehaviour {
	[System.Serializable]
	class Stat{
		const int BASE_XP = 10;
		const float MULTIPLIER = 1.3f;
		public float levelUpAmount;
		public int currentLevel;
		public Image statBar;
		public Image border;
		public float currentExperience;
		public Text currentLevelText;

		public void AddXp(int xp){
			currentExperience += xp;
			if (currentExperience > levelUpAmount)
			{
				currentLevel++;
				levelUpAmount = (currentLevel + Mathf.Log(currentLevel)*MULTIPLIER) * BASE_XP;
			}
		}

	};

	[SerializeField] Stat[] stats;
	[SerializeField] float rectWidth;
	[SerializeField] float rectHeight;
	public static StatsManager instance;

	// Use this for initialization
	void Start () {
		instance = this;
		DisableStatBarImages();
		for(var i = 0; i < 10; i++){
			//Debug.Log ((i + Mathf.Log (i) * 1.3f)*10f);
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		foreach(var stat in stats){
			float currentEXP = stat.currentExperience / stat.levelUpAmount;
			stat.statBar.rectTransform.sizeDelta = new Vector2(rectWidth * currentEXP, rectHeight);
			stat.currentLevelText.text = ("" + stat.currentLevel);
		}
	}

	public void DisableStatBarImages()
	{
		foreach(var stat in stats){
			stat.statBar.CrossFadeAlpha(0.0f,0.5f,true);
			stat.border.CrossFadeAlpha(0.0f,0.5f,true);
			stat.currentLevelText.CrossFadeAlpha(0.0f,0.5f,true);
		}
	}
	public void EnableStatBarImages()
	{
		foreach(var stat in stats){
			stat.statBar.CrossFadeAlpha(1.0f,0.5f,true);
			stat.border.CrossFadeAlpha(1.0f,0.5f,true);
			stat.currentLevelText.CrossFadeAlpha(1.0f,0.5f,true);
		}
	}

	public void AddStat(string color, int xp){
		var stat = stats[GetColorIndex(color)];
		stat.AddXp(xp);
		StartCoroutine(FadeInOut(1.0f, stat));


	}
	IEnumerator FadeInOut(float delay, Stat stat)
	{
		stat.statBar.CrossFadeAlpha(1.0f,0.5f,false);

		yield return new WaitForSeconds(0.5f + delay);

		stat.statBar.CrossFadeAlpha(0.0f,0.5f,false);
	}

	int GetColorIndex(string color){
		if (color == "Red")
		{
			return 0;
		}
		if (color == "Blue")
		{
			return 1;
		}
		if (color == "Yellow")
		{
			return 2;
		}
		if (color == "Green")
		{
			return 3;
		}
		if (color == "Orange")
		{
			return 4;
		}
		if (color == "Purple")
		{
			return 5;
		}
		return -1;
	}

}
