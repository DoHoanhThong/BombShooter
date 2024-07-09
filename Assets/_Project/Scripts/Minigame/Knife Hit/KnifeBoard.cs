using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeBoard : MonoBehaviour
{
    private KnifeLevelSetting levelSetting;

	[SerializeField]
	public GameObject spikePrefab;
	[SerializeField]
	private GameObject heartPrefab;
	[SerializeField]
	private GameObject coinPrefab;
	//[SerializeField]
	//public Transform pointInCircle;
	
	private enum State
	{
		Rotating,
		SlowDown,
		Delay,
		SpeedUp,
	}

	private State currentState;

	[SerializeField]
	private float radius = 4;

	[Header("Shake")]
	[SerializeField]
	private float _shakeDuration = 0.1f;
	[SerializeField]
	private float _shakeStrength = 0.1f;
	[SerializeField]
	private int _shakeVibrato = 1;
	[SerializeField]
	private float _shakeRandomness = 90;

	
	private float _timeCounter = 0;

	private float speed;

	public Vector3 rotate = Vector3.forward;
	
	public bool canRotate = true;

	private int followClockwise;

	private int waveIndex = 0;

	List<float> occupiedAngles = new List<float>();

	public void Initialize(KnifeLevelSetting levelSetting)
    {
		this.levelSetting = levelSetting;
		occupiedAngles.Clear();
		SpawnSpikes();
		SpawnHeartItems();
		SpawnCoinItems();
		waveIndex = 0;
		transform.rotation = Quaternion.Euler(0, 0, 0);
		speed = levelSetting.maxSpeed * levelSetting.waves[waveIndex].speedMultiplier;
		followClockwise = levelSetting.waves[waveIndex].isReverse ? -1 : 1;
		currentState = State.Rotating;
		_timeCounter = 0;
		//canRotate = true;
	}
	void SpawnSpikes()
	{
		//radius = Vector2.Distance(transform.position, pointInCircle.position);
		for (int i = 0; i < levelSetting.numObstacle; i++)
		{
			float angle = i * 360f / levelSetting.numObstacle;
			Vector2 position = CirclePoint(radius, angle);
			GameObject spike = Instantiate(spikePrefab, transform); 
			//spike.transform.SetParent(transform);
			spike.transform.localPosition = position;
			spike.transform.localRotation = Quaternion.Euler(0, 0, angle + 90);
			occupiedAngles.Add(angle);
		}
	}

	Vector2 CirclePoint(float radius, float angle)
	{
		float x = radius * Mathf.Cos(angle * Mathf.Deg2Rad);
		float y = radius * Mathf.Sin(angle * Mathf.Deg2Rad);
		return new Vector2(x, y);
	}
	public virtual void Update()
	{
		if (levelSetting == null || !canRotate)
			return;
		
		switch (currentState)
        {
			case State.Rotating:
				Rotating();
				break;
			case State.SlowDown:
				SlowDown();
				break;
			case State.Delay:
				Delay();
				break;
			case State.SpeedUp:
				SpeedUp();
				break;
        }
	}
	public virtual void Shake()
	{
		//transform.DOShakePosition(_shakeDuration, _shakeStrength, _shakeVibrato, _shakeRandomness);
		transform.DOShakePosition(_shakeDuration, _shakeStrength, _shakeVibrato, _shakeRandomness);
	}
	public void Reset()
	{
		foreach (var knife in transform.GetComponentsInChildren<KnifeScript>())
		{
            Destroy(knife.gameObject);
		}
		foreach (var item in transform.GetComponentsInChildren<KnifeCollectItem>())
		{
			Destroy(item.gameObject);
		}
		//canRotate = false;
	}
	public void Rotating()
    {
		_timeCounter += Time.deltaTime;		
		if (_timeCounter > levelSetting.waves[waveIndex].rotateTime)
		{
			_timeCounter = 0;
			NextState();
		}
		transform.Rotate(speed * rotate * Time.deltaTime * followClockwise);
	}
	public void SlowDown()
    {		
		speed -= levelSetting.waves[waveIndex].slowDownSpeed * Time.deltaTime;
		if (speed <= 0)
		{
			speed = 0;
			NextState();
		}
		transform.Rotate(speed * rotate * Time.deltaTime * followClockwise);
	}
	public void Delay()
    {
		_timeCounter += Time.deltaTime;
		if (_timeCounter > levelSetting.waves[waveIndex].delayTime)
		{
			_timeCounter = 0;
			NextState();

			followClockwise *= levelSetting.waves[waveIndex].isReverse ? -1 : 1;
			waveIndex = waveIndex + 1 >= levelSetting.waves.Length ? 0 : waveIndex + 1;			
		}

	}
	public void SpeedUp()
    {		
		speed += levelSetting.waves[waveIndex].accelerationSpeed * Time.deltaTime;
		if (speed >= levelSetting.maxSpeed * levelSetting.waves[waveIndex].speedMultiplier)
		{
			speed = levelSetting.maxSpeed * levelSetting.waves[waveIndex].speedMultiplier;
			NextState();
		}
		transform.Rotate(speed * rotate * Time.deltaTime * followClockwise);
	}
	public void NextState()
    {
		currentState = (State)(((int)currentState + 1) % System.Enum.GetValues(typeof(State)).Length);
	}



	void SpawnHeartItems()
	{
		float minAngle = 15f; // Set the minimum angle separation to 15 degrees

		for (int i = 0; i < levelSetting.numHeart; i++)
		{
			float angle = FindSafeAngle(minAngle, 5);
			if (angle < 0) continue; // No safe angle found

			Vector2 position = CirclePoint(radius, angle);
			GameObject item = Instantiate(heartPrefab, transform);
			item.transform.localPosition = position;
			item.transform.localRotation = Quaternion.Euler(0, 0, angle + 90);

			occupiedAngles.Add(angle);
		}
	}
	void SpawnCoinItems()
	{
		float minAngle = 15f; // Set the minimum angle separation to 15 degrees

		for (int i = 0; i < levelSetting.numCoin; i++)
		{
			float angle = FindSafeAngle(minAngle, 5);
			if (angle < 0) continue; // No safe angle found

			Vector2 position = CirclePoint(radius, angle);
			GameObject item = Instantiate(coinPrefab, transform);
			item.transform.localPosition = position;
			item.transform.localRotation = Quaternion.Euler(0, 0, angle + 90);

			occupiedAngles.Add(angle);
		}
	}

	float FindSafeAngle(float minAngle, int maxAttempts)
	{
		List<float> testedAngles = new List<float>();

		while (testedAngles.Count < maxAttempts)
		{
			float angle = Random.Range(0f, 360f);
			if (testedAngles.Contains(angle)) continue;

			testedAngles.Add(angle);
			bool isSafe = true;

			foreach (float occupiedAngle in occupiedAngles)
			{
				if (Mathf.Abs(angle - occupiedAngle) < minAngle)
				{
					isSafe = false;
					break;
				}
			}
			if (isSafe) return angle;
		}
		return -1;
	}

}
