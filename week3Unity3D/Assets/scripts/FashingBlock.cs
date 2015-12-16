using UnityEngine;
using System.Collections;

public class FashingBlock : MonoBehaviour {

	[SerializeField] Material material;
	[SerializeField] Player player;
	[SerializeField] bool isHorizontal;
	[SerializeField] bool isVertical;
	[SerializeField] Color blue;
	[SerializeField] Color red;
	[SerializeField] Color green;
	// Use this for initialization
	void Start () 
	{
		blue = new Color(0.0f,0.0f,255.0f);
		red = new Color(255.0f,0.0f,0.0f);
		green = new Color(0.0f,255.0f,0.0f);

		player = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Player>();

		if (isHorizontal)
		{
			material.color = red;
		}
		if (isVertical)
		{
			material.color = blue;
		}
	}
	
	// Update is called once per frame
	void Update () 
	{

	}

	void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player") && other.transform.position.y > transform.position.y)
		{
			other.gameObject.transform.parent = transform;
			material.color = new Color(0.0f,255.0f,0.0f);
		}
	}
	void OnTriggerExit(Collider other)
	{
		if (other.CompareTag("Player") && isHorizontal)
		{
			other.gameObject.transform.parent = null;
			material.color = red;
		}
		if (other.CompareTag("Player") && isVertical)
		{
			other.gameObject.transform.parent = null;
			material.color = blue;
		}
	}
}
