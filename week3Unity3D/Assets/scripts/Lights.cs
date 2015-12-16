using UnityEngine;
using System.Collections;

public class Lights : MonoBehaviour {


	[SerializeField] Light lights;
	[SerializeField] bool increaseLight;
	[SerializeField] bool decreaseLight;
	[SerializeField] float maxRange;
	[SerializeField] float increaseAmount;
	[SerializeField] float minRange;
	// Use this for initialization
	void Start () 
	{
		increaseLight = true;
		//sun = GameObject.FindGameObjectWithTag("Sun").GetComponent<DayNNite>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (increaseLight)
		{
			IncreaseRange();
		}
		if (decreaseLight)
		{
			DecreaseRange();
		}

	
	}
	void IncreaseRange()
	{
		lights.range += increaseAmount;

		if (lights.range >= maxRange)
		{
			decreaseLight = true;
			increaseLight = false;
			return;
		}
	}
	void DecreaseRange()
	{
		lights.range -= increaseAmount;

		if (lights.range <= minRange)
		{
			increaseLight = true;
			decreaseLight = false;
			return;
		}
	}
}
