using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class Player : MonoBehaviour {

	public Camera cam;
	public float bulletSpeed;
	public Rigidbody rb;
	[SerializeField] Transform shootPoint;
	private float timer;
	[SerializeField] float shotDelay;
	private bool hasShot;
	[SerializeField] GameObject bullet;
	[SerializeField] Material material;
	[SerializeField] ParticleSystem ps;
	public float health;
	public float maxHealth;
	[SerializeField] Text healthText;
	[SerializeField] Text bulletColor;
	[SerializeField] float bulletShot;
	[SerializeField] Image healthBar;
	[SerializeField] Image heatBar;
	[SerializeField] Image PowerupBar;
	[SerializeField] float overHeatAmount;
	public Bullet bullets;
	[SerializeField] bool gunOverHeating;
	[SerializeField] bool setInvincible;
	float rectWidth = 200f;
	bool switchColor = false;
	public bool keyUp = true;
	[SerializeField] float coolDown;
	[SerializeField] float invincibleTimer;
	[SerializeField] Texture crosshair;
	public PowerUps powerUp;
	[SerializeField] float powerUpTimer;
	[SerializeField] float powerUpDuration;
	public bool checkForPowerUps;
	public bool hasRapidFire;
	public bool hasInfiniteRun;
	public FirstPersonController FPC;
	public Bullet b;
	public EnemyAI enemy;

	// Use this for initialization
	void Start () 
	{
		health = maxHealth;
		cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
		hasRapidFire = false;
		hasInfiniteRun = false;
		//PowerupBar.color = new Color(230.0f,0.0f,255.0f,0.0f);

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
//		if (checkForPowerUps)
//		{
//			powerUp = GameObject.FindGameObjectWithTag("PowerUp").GetComponent<PowerUps>();
//
//		}
		if (hasInfiniteRun || hasRapidFire)
		{

			DisplayPowerUpTimer();

			powerUpTimer += Time.deltaTime;

			if (powerUpTimer > powerUpDuration)
			{
				checkForPowerUps = false;
				powerUpTimer = 0.0f;
				hasRapidFire = false;
				hasInfiniteRun = false;
			}
			if (powerUpTimer > powerUpDuration - 0.5)
			{
				PowerupBar.CrossFadeAlpha(0.0f,0.5f,false);
			}
		}
		if (hasRapidFire)
		{
			shotDelay = 0.1f;
		}
		else 
		{
			shotDelay = 0.3f;
		}

		if (hasInfiniteRun)
		{
			FPC.stamina = -1.0f;
		}

		if (Input.GetKeyUp("z") && keyUp)
		{
			//Debug.Log("prev: " + b.prevColor + " current: " + b.currentColor + " next: " + b.nextColor);
			//index doesnt increase until the key is pressed up 
			Bullet.SetNextColor();
			keyUp = false;
		}
		if (Input.GetKey("z") && timer == 0.0f && !gunOverHeating)
		{
			//Debug.Log("prev: " + b.prevColor + " current: " + b.currentColor + " next: " + b.nextColor);
			keyUp = true;
			hasShot = true;
			Rigidbody InstantiateProjectile = Instantiate(rb, shootPoint.position, transform.rotation) as Rigidbody;
			bulletShot++;
			InstantiateProjectile.velocity = transform.TransformDirection(Vector3.forward * bulletSpeed);
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

		if (bulletShot >= overHeatAmount && !hasRapidFire)
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
			Camera.instance.Shake(0.2f,0.2f);
		}
		setInvincible = true;
	}
	void UpdateHealthUI()
	{
		float currHealth = health / maxHealth;
		healthBar.rectTransform.sizeDelta = new Vector2(rectWidth * currHealth, 12);


	}
	void DisplayPowerUpTimer()
	{
		PowerupBar.CrossFadeAlpha(1.0f,0.2f,false);
		PowerupBar.color = new Color(230.0f,0.0f,255.0f,255.0f);
		float time = (powerUpDuration - powerUpTimer) / powerUpDuration;
		PowerupBar.rectTransform.sizeDelta = new Vector2(rectWidth * time, 12);
	}
	void UpdateHeatBarUI()
	{
		float heatAmount = bulletShot/overHeatAmount;
		if (hasRapidFire && heatAmount >= 1.0f)
		{
			heatAmount = 1.0f;
		}
		heatBar.rectTransform.sizeDelta = new Vector2(rectWidth * heatAmount, 12);

	}
	void OnGUI()
	{
		GUI.Label(new Rect(Screen.width / 1.95f, Screen.height / 1.8f, 64,64), crosshair);
	}
	void PlayerDeath()
	{
		//restart the level
		Application.LoadLevel(Application.loadedLevel);
	}

}