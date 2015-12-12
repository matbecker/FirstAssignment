using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {

	[SerializeField] GameObject[] enemyType;
	[SerializeField] int randNumber;
	[SerializeField] float spawnTimer;
	[SerializeField] float spawnDuration;
	[SerializeField] bool playerInTrigger;
	[SerializeField] Transform spawnPoint;
	[SerializeField] Material material;
	[SerializeField] int currentFrame;
	[SerializeField] ParticleSystem ps;
	// Use this for initialization
	void Start () 
	{
		playerInTrigger = false;
	}
	
	// Update is called once per frame
	void Update () 
	{
		spawnTimer += Time.deltaTime;

		if (spawnTimer >= spawnDuration & playerInTrigger)
		{
			SpawnEnemy();
			spawnTimer = 0.0f;
		}
		if (playerInTrigger)
		{
			currentFrame++;
			//slow down the animation
			if (currentFrame > 5)
			{
				material.color = new Color(Random.Range(0.0f,1.0f),Random.Range(0.0f,1.0f),Random.Range(0.0f,1.0f));
				currentFrame = 0;
			}

		}
	
	}
	void SpawnEnemy()
	{
		ps.startColor = new Color(Random.Range(0.0f,1.0f),Random.Range(0.0f,1.0f),Random.Range(0.0f,1.0f));
		ps.Play();
		randNumber = Random.Range(0,3);

		GameObject obj = Instantiate(enemyType[randNumber],spawnPoint.position, transform.rotation) as GameObject;

		//enemyType[randNumber].GetComponent<Rigidbody>().AddForce(Vector3.up * 1000.0f);
		obj.GetComponent<Rigidbody>().velocity = new Vector3(0.0f, 10.0f, 0.0f) + Random.insideUnitSphere * 5;
	}
	void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			playerInTrigger = true;
		}
	}
	void OnTriggerExit(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			playerInTrigger = false;
		}
	}
}
