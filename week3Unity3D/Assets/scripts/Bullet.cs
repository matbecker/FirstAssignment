using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

	[SerializeField] Material material;
	[SerializeField] Player player;
	[SerializeField] Color[] bulletColors;
	public static int index = 0;

	// Use this for initialization
	void Start () 
	{

	}
	
	// Update is called once per frame
	void Update () 
	{


	}
	public void SetNextColor()
	{
		if (index >= bulletColors.Length)
		{
			index = 0;
		}
		GetComponent<Renderer>().material.color = bulletColors[index++];
	}
}
