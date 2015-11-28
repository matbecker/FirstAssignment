using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Player : MonoBehaviour {

	public Camera cam;
	public float speed;
	public Rigidbody rb;
	[SerializeField] Transform shootPoint;
	private float timer;
	[SerializeField] float shotDelay;
	private bool hasShot;
	[SerializeField] GameObject bullet;
	[SerializeField] Material material;
	[SerializeField] ParticleSystem ps;
	[SerializeField] float health;
	[SerializeField] float maxHealth;
	[SerializeField] Text healthText;
	[SerializeField] Text bulletColor;
	[SerializeField] float bulletShot;
	[SerializeField] Image healthBar;
	[SerializeField] Image heatBar;
	[SerializeField] float overHeatAmount;
	public Bullet bullets;
	[SerializeField] bool gunOverHeating;
	[SerializeField] bool setInvincible;
	float rectWidth = 200f;
	bool switchColor = false;
	public bool keyUp = true;
	[SerializeField] float coolDown;
	[SerializeField] float invincibleTimer;
	// Use this for initialization
	void Start () 
	{
		health = maxHealth;
		cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (hasShot)
		{
			ps.Play();
			timer += Time.deltaTime;
		}
		if (timer > shotDelay)
		{
			timer = 0.0f;
			hasShot = false;
		}
		if (setInvincible)
		{
			invincibleTimer += Time.deltaTime;
		}
		if (invincibleTimer > coolDown)
		{
			invincibleTimer = 0.0f;
			setInvincible = false;
		}

		if (Input.GetKeyUp("z") && keyUp)
		{
			//index doesnt increase until the key is pressed up 
			Bullet.SetNextColor();
			keyUp = false;
		}
		if (Input.GetKey("z") && timer == 0.0f && !gunOverHeating)
		{
			keyUp = true;
			hasShot = true;
			Rigidbody InstantiateProjectile = Instantiate(rb, shootPoint.position, transform.rotation) as Rigidbody;
			bulletShot++;
			InstantiateProjectile.velocity = transform.TransformDirection(Vector3.forward * speed);
			InstantiateProjectile.GetComponent<Bullet>().SetColor();
			Destroy(InstantiateProjectile.gameObject, 1f);
		}
		else 
		{
			bulletShot -= Time.deltaTime * 1.5f;

			if (bulletShot <= 0f)
			{
				bulletShot = 0f;
				gunOverHeating = false;
			}
		}

		if (bulletShot >= overHeatAmount)
		{
			bulletShot = overHeatAmount;
			gunOverHeating = true;
		}
		if (health <= 0)
		{
			PlayerDeath();
		}
		UpdateHealthUI();
		UpdateHeatBarUI();




	
	}
	public void Damage(int dmg)
	{
		if (invincibleTimer == 0.0f)
		{
			health -= dmg;
		}
		setInvincible = true;
		Camera.instance.Shake(0.2f,0.2f);
	}
	void UpdateHealthUI()
	{
		float currHealth = health / maxHealth;
		healthBar.rectTransform.sizeDelta = new Vector2(rectWidth * currHealth, 12);
	}
	void UpdateHeatBarUI()
	{
		float heatAmount = bulletShot/overHeatAmount;
		heatBar.rectTransform.sizeDelta = new Vector2(rectWidth * heatAmount, 12);
	}
	void PlayerDeath()
	{
		//restart the level
		Application.LoadLevel(Application.loadedLevel);
	}
}
