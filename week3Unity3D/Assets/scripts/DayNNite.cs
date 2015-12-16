using UnityEngine;
using System.Collections;

public class DayNNite : MonoBehaviour {


	[SerializeField] float dayLength;
	[SerializeField] float dayStart;
	[SerializeField] float updateInterval;
	[SerializeField] GameObject[] orbs;
	public float dayElaspedTime;
	private float updateElaspedTime;
	public bool isDay;
	// Use this for initialization
	void Start () 
	{
		dayElaspedTime = dayStart;

	}
	
	// Update is called once per frame
	void Update () 
	{
		dayElaspedTime += Time.deltaTime;
		updateElaspedTime += Time.deltaTime;

		if (updateElaspedTime >= updateInterval)
		{
			updateElaspedTime -= updateInterval;
			transform.LookAt(new Vector3(0.0f,Mathf.Sin((dayElaspedTime / dayLength) * 2 * Mathf.PI), Mathf.Cos((dayElaspedTime / dayLength) * 2 * Mathf.PI)));

			if (dayElaspedTime >= dayLength)
			{
				dayElaspedTime = 0;
			}

			if (dayElaspedTime > 0.0f && dayElaspedTime < 150.0f)
			{
				if (isDay)
				{
					for (int i = 0; i < orbs.Length; i++)
					{
						orbs[i].SetActive(true);
					}
//					GameObject[] lightList = GameObject.FindGameObjectsWithTag("Lights");
//
//					foreach (GameObject light in lightList)
//					{
//						light.SetActive(true);
//					}
					isDay = false;
				}

			}
			else 
			{
				if (!isDay)
				{
					for (int i = 0; i < orbs.Length; i++)
					{
						orbs[i].SetActive(false);
					}
//					GameObject[] lightList = GameObject.FindGameObjectsWithTag("Lights");
//					
//					foreach (GameObject light in lightList)
//					{
//						light.SetActive(false);
//
//					}
					isDay = true;
				}

			}


		}
	}
}
