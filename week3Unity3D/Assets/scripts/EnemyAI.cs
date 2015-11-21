using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour {

	public enum Type
	{
		White,
		Blue,
		Red,
		Yellow,
		Green,
		Orange,
		Purple

	}
	public Player player;
	[SerializeField] float playerDistance;
	[SerializeField] float rotationDamping;
	[SerializeField] float moveSpeed;
	[SerializeField] float chaseCooldown;
	[SerializeField] GameObject bullet;
	[SerializeField] Transform lookPoint;
	[SerializeField] Rigidbody rb;
	[SerializeField] float speed;
	[SerializeField] float shotDelay;
	private Quaternion currentRot;
	private Vector3 currentPos;
	private float shotTimer;
	private bool stopChasing;
	private float timer;
	[SerializeField] Color[] enemyColor;
	[SerializeField] string enemyType;
	[SerializeField] Material material;
	[SerializeField] bool isPrimary;
	[SerializeField] bool isSecondary;
	[SerializeField] int health;
	Type type = Type.White;

	// Use this for initialization
	void Start () 
	{
		GetEnemyType();
		SetEnemyType();
		player = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Player>();

	}
	void LookAtPlayer()
	{
		Quaternion rotation = Quaternion.LookRotation(player.transform.position - transform.position, Vector3.up);
		transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotationDamping);
	}
	void ChasePlayer()
	{
		transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
	}
	void StopChasing()
	{
		stopChasing = true;
	}
	void ShootPlayer()
	{
		Rigidbody InstantiateProjectile = Instantiate(rb, currentPos, currentRot) as Rigidbody;
		InstantiateProjectile.velocity = transform.TransformDirection(new Vector3(0.0f,0.0f,speed));
	}
	void SetEnemyType()
	{
		switch (type)
		{
		case Type.Red:
			material.color = enemyColor[0];
			break;
		case Type.Blue:
			material.color = enemyColor[1];
			break;
		case Type.Purple:
			material.color = enemyColor[2];
			break;
		case Type.Orange:
			material.color = enemyColor[3];
			break;
		case Type.Yellow:
			material.color = enemyColor[4];
			break;
		case Type.Green:
			material.color = enemyColor[5];
			break;
		case Type.White:
			material.color = enemyColor[6];
			break;
		default:
			break;
			
		}
	}
	void GetEnemyType()
	{
		if (enemyType == "Blue")
		{
			type = Type.Blue;
		}
		else if (enemyType == "Red")
		{
			type = Type.Red;
		}
		else if (enemyType == "Yellow")
		{
			type = Type.Yellow;
		}
		else
		{
			isSecondary = true;
		}

		if (enemyType == "Green")
		{
			type = Type.Green;
		}
		else if (enemyType == "Orange")
		{
			type = Type.Orange;
		}
		else if (enemyType == "Purple")
		{
			type = Type.Purple;
		}
		else
		{
			isPrimary = true;
		}
	}
	// Update is called once per frame
	void Update () 
	{
		SetEnemyType();
		playerDistance = Vector3.Distance(player.transform.position, transform.position);
		currentRot = transform.rotation;
		currentPos = lookPoint.transform.position;
		if (playerDistance < 25.0f)
		{
			LookAtPlayer();
		}
		if (playerDistance < 15.0f && !stopChasing)
		{
			ChasePlayer();
		}

		if (playerDistance < 5.0f)
		{

			StopChasing();
			shotTimer += Time.deltaTime;
			if (shotTimer > shotDelay)
			{
				ShootPlayer();
				shotTimer = 0.0f;
			}
		}
		else
		{
			timer += Time.deltaTime;

			if (timer > chaseCooldown)
			{
				stopChasing = false;
				timer = 0.0f;
			}
		}
	
	}
}
