using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EnemyAI : MonoBehaviour {

	[System.Serializable]
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
	[SerializeField] bool stopChasing;
	private float timer;
	[SerializeField] Color enemyColor;
	[SerializeField] Material material;
	[SerializeField] bool isPrimary;
	[SerializeField] bool isSecondary;
	[SerializeField] float health;
	[SerializeField] float maxHealth;
	public Bullet bullets;
	[SerializeField] string type;
	[SerializeField] Image healthBarImage;
	[SerializeField] Color[] secondaryColor;
	[SerializeField] float chaseTimer;
	[SerializeField] float chasePeriod;
	[SerializeField] GameObject[] powerUp;
	[SerializeField] int rand;
	[SerializeField] int randPowerUp;
	[SerializeField] bool invincible;


	// Use this for initialization
	void Start () 
	{
		health = maxHealth;
		player = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Player>();
		bullets = GameObject.FindGameObjectWithTag("Gun").GetComponent<Bullet>();
		GetComponent<Renderer>().material.color = enemyColor;
	}
	void LookAtPlayer(float rotationDamping)
	{
		Quaternion rotation = Quaternion.LookRotation(player.transform.position - transform.position, Vector3.up);
		transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotationDamping);
	}
	void ChasePlayer(float ms)
	{
		transform.Translate(Vector3.forward * ms * Time.deltaTime);
	}
	void StopChasing()
	{
		stopChasing = true;
	}
	void ShootPlayer(float s, bool burst)
	{
		if (!burst)
		{
			Rigidbody InstantiateProjectile = Instantiate(rb, currentPos, currentRot) as Rigidbody;
			InstantiateProjectile.velocity = transform.TransformDirection(new Vector3(0.0f,0.0f,s));
		}
		if (burst)
		{
			for (int i = 0; i < 3; i++)
			{
				Rigidbody InstantiateProjectile = Instantiate(rb, new Vector3(currentPos.x, currentPos.y, currentPos.z - 1 + i), currentRot) as Rigidbody;
				InstantiateProjectile.velocity = transform.TransformDirection(new Vector3(0.0f,0.0f,s));
			}
		}
	}
	void Damage(int dmg)
	{
		//Debug.Log("current bullet:" + bullets.currentColor + "  " + "Enemy:" + type);
		//Debug.Log("next bullet:" + bullets.nextColor + "  " + "Enemy:" + type);
		if (player.keyUp && bullets.currentColor == type || !player.keyUp && bullets.prevColor == type)
		{
			dmg *= 5;
		}

		health -= dmg;
		Debug.Log(dmg);
		if (health <= 0)
		{
			OnEnemyDeath();
		}
	}
	void SetEnemyColor()
	{
		if (type == "Blue" || type == "Red" || type == "Yellow")
		{
			isPrimary = true;
		}
		else
		{
			isSecondary = true;
			isPrimary = false;
		}
	}
	void OnCollisionEnter(Collision other)
	{
		if (other.collider.CompareTag("Bullet") && !invincible)
		{
			Damage(1);
			invincible = true;
			//Destroy(other.gameObject);
		}
		if (!isSecondary && this.CompareTag("Blue") && other.collider.CompareTag("Red") || !isSecondary && this.CompareTag("Red") && other.collider.CompareTag("Blue"))
		{
			type = "Purple";
			GetComponent<Renderer>().material.color = secondaryColor[0];
		}
		if (!isSecondary && this.CompareTag("Red") && other.collider.CompareTag("Yellow") || !isSecondary && this.CompareTag("Yellow") && other.collider.CompareTag("Red"))
		{
			type = "Orange";
			GetComponent<Renderer>().material.color = secondaryColor[1];
		}
		if (isPrimary && this.CompareTag("Blue") && other.collider.CompareTag("Yellow") || isPrimary && this.CompareTag("Yellow") && other.collider.CompareTag("Blue"))
		{
			type = "Green";
			GetComponent<Renderer>().material.color = secondaryColor[2];
		}
		if (other.collider.CompareTag("Player") && isSecondary)
		{
			player.Damage(1);
		}

	}
	void OnCollisionExit(Collision other)
	{
		if (other.collider.CompareTag("Bullet"))
		{
			invincible = false;
		}
	}
	
	void UpdateHealthUI()
	{
		float currHealth = health / maxHealth;
		healthBarImage.rectTransform.localScale = new Vector3(currHealth, healthBarImage.rectTransform.localScale.y, healthBarImage.rectTransform.localScale.z);
	}
	void OnEnemyDeath()
	{

		rand = Random.Range(0,5);
		randPowerUp = Random.Range(0,3);
		//Debug.Log(randPowerUp);
		//instantiate a power up in the random value returns 2
		//if (rand == 2)
		//{
			Instantiate(powerUp[randPowerUp], transform.position, Quaternion.identity);
			player.checkForPowerUps = true;
		//}
		Destroy(gameObject);
	}
	// Update is called once per frame
	void Update () 
	{
		SetEnemyColor();
		UpdateHealthUI();
		playerDistance = Vector3.Distance(player.transform.position, transform.position);
		currentRot = transform.rotation;
		currentPos = lookPoint.transform.position;

		if (isPrimary)
		{
			if (playerDistance < 25.0f)
			{
				LookAtPlayer(2.0f);
			}
			if (playerDistance < 15.0f && !stopChasing)
			{
				ChasePlayer(5.0f);
			}

			if (playerDistance < 5.0f)
			{

				StopChasing();
				shotTimer += Time.deltaTime;

				if (shotTimer > shotDelay)
				{
					ShootPlayer(20.0f, false);
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

		if (isSecondary)
		{
			if (type == "Purple")
			{
				LookAtPlayer(2.0f);
				ChasePlayer(7.0f);
			}


			if (type == "Orange")
			{

				LookAtPlayer(2.0f);
				if (!stopChasing)
				{
					ChasePlayer(10.0f);
				}

				chaseTimer += Time.deltaTime;

				if (chaseTimer > chasePeriod)
				{
					StopChasing();

					if (chaseTimer > 3.0f)
					{
						ShootPlayer(30.0f, false);
					}

					if (chaseTimer > 4.0f)
					{
						chaseTimer = 0.0f;
						stopChasing = false;
					}

				}
			}
			if (type == "Green")
			{
				LookAtPlayer(5.0f);
				shotTimer += Time.deltaTime;

				if (shotTimer > 0.5f)
				{
					ShootPlayer(40.0f, true);
					shotTimer = 0.0f;
				}

			}
		}
	
	}
}
