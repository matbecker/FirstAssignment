using UnityEngine;
using System.Collections;

public class Camera : MonoBehaviour {

	public static Camera instance;
	public Vector3 currentPos;
	public Quaternion originalRot;
	public float shakeDecay;
	public float shakeIntensity;
	public float shakeDuration;
	private bool shaking;
	// Use this for initialization
	void Start () 
	{
		instance = this;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (shaking)
		{
			transform.localPosition = currentPos + Random.insideUnitSphere * shakeIntensity;
		}
	}
	
	public void Shake(float intensity, float duration)
	{
		currentPos = transform.localPosition;
		shakeIntensity = intensity;
		shaking = true;
		CancelInvoke();
		Invoke("StopShaking", duration);
	}
	public void StopShaking()
	{
		transform.localPosition = currentPos;
		shaking = false;
	}
}
