using UnityEngine;
using System.Collections;

public class EnemyBullet : MonoBehaviour {

	public Player player;

	void OnCollisionEnter(Collision other)
	{
		if (other.collider.CompareTag("Player"))
		{
			player.Damage(1);
			Destroy(gameObject);
		}
		Destroy(gameObject, 3.0f);

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
