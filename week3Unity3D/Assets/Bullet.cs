using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

	[SerializeField] Material material;
	[SerializeField] Player player;
	[SerializeField] Material[] bulletColors;
	// Use this for initialization
	void Start () 
	{

	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetKeyDown("z"))
		{

			material.color = new Color(Random.Range(0.0f,1.0f),Random.Range(0.0f,1.0f),Random.Range(0.0f,1.0f));
//			for (int i = 0; i > bulletColors.Length; i++)
//			{
//
//			}
		}

	}
}
