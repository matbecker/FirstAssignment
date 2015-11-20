using UnityEngine;
using System.Collections;

public class EnemyBullet : MonoBehaviour {

	public Player player;

	void OnCollisionEnter(Collision other)
	{
		Destroy(gameObject);
		if (other.collider.CompareTag("Player"))
		{
			//Destroy(gameObject);
			player.Damage(1);
		}
		else
		{
			Destroy(other.gameObject, 3.0f);
		}


	}
	// Use this for initialization
	void Start () 
	{
		player = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Player>();
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
}
