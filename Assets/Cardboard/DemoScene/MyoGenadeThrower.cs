using UnityEngine;  
using System.Collections;

public class MyoGenadeThrower : MonoBehaviour  
{
	public GameObject myo;
	private ThalmicMyo _myo = null;

	public GameObject jointBehaviour;
	private JointOrientation _jointBehaviour;

	public GameObject GrenadePrefab = null;

	public int rollAMin;
	public int rollAMax;
	public int rollBMin;
	public int rollBMax;
	public int yawAMin;
	public int yawAMax;
	public int yawBMin;
	public int yawBMax;
	public int pitchAMin;
	public int pitchAMax;
	public int pitchBMin;
	public int pitchBMax;

	private bool _inPosition = false;
	private float _inPositionTime;
	private bool _startedThrow = false;
	private float _startThrowTime;

	public float minThrowStrength;
	public float maxThrowStrength;
	public float baseThrowStrength;
	public float normalThrowTime;
	public float maxThrowTime = 1f;

	void Start()
	{
		_myo = myo.GetComponent<ThalmicMyo>();
		_jointBehaviour = (JointOrientation)jointBehaviour.GetComponent(typeof(JointOrientation));
	}

	void Update()
	{
		int armYawAMin = yawAMin;
		int armYawAMax = yawAMax;
		int armYawBMin = yawBMin;
		int armYawBMax = yawBMax;

		// Adjust yaw A and B zones for left arm.
		if (_myo.arm == Thalmic.Myo.Arm.Left) {
			armYawAMin = 360 - yawAMax;
			armYawAMax = 360 - yawAMin;
			armYawBMin = 360 - yawBMax;
			armYawBMax = 360 - yawBMin;
		}

		bool zoneA = inZone(rollAMin, rollAMax, armYawAMin, armYawAMax, pitchAMin, pitchAMax);

		if (!_inPosition && zoneA) {
			_inPosition = true;
			_startedThrow = false;
			_inPositionTime = Time.time;

			Debug.Log(_jointBehaviour.transform.localRotation.eulerAngles);
			Debug.Log("_inPosition=" + _inPosition);
			Debug.Log ("IN POSITION");
		}

		if (_inPosition && !_startedThrow) {
			_startedThrow = !zoneA;

			if (_startedThrow) {
				Debug.Log(_jointBehaviour.transform.localRotation.eulerAngles);
				Debug.Log("_startedThrow=" + _startedThrow);
				Debug.Log ("STARTED THROW");

				_inPosition = false;
				_startThrowTime = Time.time;
			}
		} else if (_startedThrow && (Time.time - _startThrowTime) > maxThrowTime) {
			// Didn't move to zoneB in time so reset throw state.
			_inPosition = false;
			_startedThrow = false;
			Debug.Log("throw timed out");
		} else if (_startedThrow) {
			bool zoneB = inZone(rollBMin, rollBMax, armYawBMin, armYawBMax, pitchBMin, pitchBMax);

			if (zoneB) {
				Debug.Log(_jointBehaviour.transform.localRotation.eulerAngles);
				Debug.Log("zoneB=" + zoneB);
				Debug.Log ("THROWN OBJECT");
				_startedThrow = false;

				throwGrenade();
			}
		}
	}

	private bool inZone(int rollAMin, int rollAMax, int yawAMin, int yawAMax, int pitchAMin, int pitchAMax)
	{
		Vector3 rotation = _jointBehaviour.transform.localRotation.eulerAngles;
		float roll = rotation.z;
		float pitch = rotation.x;
		float yaw = rotation.y;
//		Debug.Log("roll" + roll + "pitch" + pitch + "yaw" + yaw);
		return (rollAMin <= roll && rollAMax >= roll) &&
			(yawAMin <= yaw && yawAMax >= yaw) &&
			(pitchAMin <= pitch && pitchAMax >= pitch);
	}

	private void throwGrenade()
	{
		GameObject grenade = (GameObject)Object.Instantiate(GrenadePrefab);
		grenade.transform.position = transform.position;
		grenade.transform.rotation = transform.rotation;
		Rigidbody rigid = grenade.AddComponent<Rigidbody> ();
		rigid.mass = 5;

		Vector3 direction = transform.root.forward;
		direction.y += 1;

		float timeDiff = Time.time - _startThrowTime;
		float multiplier = normalThrowTime <= 0 ? 1f : (normalThrowTime / timeDiff);
		float throwStrength = baseThrowStrength * multiplier;

		Debug.Log("timeDiff= " + timeDiff + " throwStrength=" + throwStrength);
		throwStrength = Mathf.Clamp(throwStrength, minThrowStrength, maxThrowStrength);

		grenade.GetComponent<Rigidbody>().AddForce(direction * throwStrength, ForceMode.Impulse);
		grenade.GetComponent<Rigidbody>().AddTorque(Random.insideUnitSphere, ForceMode.Impulse);
	}
}