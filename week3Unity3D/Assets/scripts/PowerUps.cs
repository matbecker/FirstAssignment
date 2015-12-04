using UnityEngine;
using System.Collections;

public class PowerUps : MonoBehaviour {


	public Player player;
	[SerializeField] bool isHealthPowerUp;
	[SerializeField] bool isRapidFire;
	[SerializeField] bool isInfiniteRun;
	public bool hasRapidFire;
	public bool hasInfiniteRun;

	// Use this for initialization
	void Start () 
	{
		player = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Player>();
		hasRapidFire = false;
		hasInfiniteRun = false;
	
	}
	void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player") && isHealthPowerUp)
		{
			player.health++;

			if (player.health > player.maxHealth)
			{
				player.health = player.maxHealth;
			}

		}
		if (other.CompareTag("Player") && isRapidFire)
		{
			player.hasRapidFire = true;
		}
		if (other.CompareTag("Player") && isInfiniteRun)
		{
			player.hasInfiniteRun = true;
		}
		if (other.CompareTag("Player"))
		{
			Destroy(gameObject);
		}
	}
	// Update is called once per frame
	void Update () 
	{
	
	}
}
