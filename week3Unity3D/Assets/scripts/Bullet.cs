using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

	[SerializeField] Material material;
	[SerializeField] Player player;
	[SerializeField] Color[] bulletColors;
	public static int index = 0;
	public string nextColor;
	public string currentColor;
	[SerializeField] bool isGun;

	// Use this for initialization
	void Start () 
	{

	}
	
	// Update is called once per frame
	void Update () 
	{
		GetNextColor();

		if (isGun)
		{
			SetColor();
		}
	}
	public void SetColor()
	{
		if (index >= bulletColors.Length)
		{
			index = 0;
		}
		GetComponent<Renderer>().material.color = bulletColors[index];
	}
	void OnCollisionEnter(Collision other)
	{
//		if (other.collider.CompareTag("Blue") || other.collider.CompareTag("Yellow") || other.collider.CompareTag("Red"))
//		{
//			Destroy(gameObject);
//		}
	}
	public static void SetNextColor(){
		index++;
	}
	public void GetNextColor()
	{
		if (index == 1)
		{
			nextColor = "Blue";

			currentColor = "Red";
		}
		else if (index == 2)
		{
			nextColor = "Purple";

			currentColor = "Blue";
		}
		else if (index == 3)
		{
			nextColor = "Orange";

			currentColor = "Purple";
		}
		else if (index == 4)
		{
			nextColor = "Yellow";

			currentColor = "Orange";
		}
		else if (index == 5)
		{
			nextColor = "Green";

			currentColor = "Yellow";
		}
		else 
		{
			nextColor = "Red";
			
			currentColor = "Green";
		}
	}
}
