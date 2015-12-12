using UnityEngine;
using System.Collections;

public class DayNNite : MonoBehaviour {


	[SerializeField] float dayLength;
	[SerializeField] float dayStart;
	[SerializeField] float updateInterval;
	private float dayElaspedTime;
	private float updateElaspedTime;
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
		}
	}
}
