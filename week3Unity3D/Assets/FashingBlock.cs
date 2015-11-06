using UnityEngine;
using System.Collections;

public class FashingBlock : MonoBehaviour {

	[SerializeField] Material material;
	[SerializeField] Player player;
	// Use this for initialization
	void Start () 
	{
		player = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Player>();
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	void OnCollisionEnter(Collision other)
	{
		if (other.collider.CompareTag("Bullet"))
		{
			material.color = new Color(Random.Range(0.0f,1.0f),Random.Range(0.0f,1.0f),Random.Range(0.0f,1.0f));
		}
		if (other.collider.CompareTag("Player"))
		{
			material.color = new Color(Random.Range(0.0f,1.0f),Random.Range(0.0f,1.0f),Random.Range(0.0f,1.0f));
		}

	}
}
