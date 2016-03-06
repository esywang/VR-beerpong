using UnityEngine;
using System.Collections;

using Pose = Thalmic.Myo.Pose;

public class MessageManager : MonoBehaviour {
	Vector3 newEulerAngles = Vector3.zero;
	GameObject myo;
	ThalmicMyo thalmicMyo;
	string curPose;

	// Use this for initialization
	void Start () {
		myo = GameObject.FindWithTag ("Myo");
		thalmicMyo = myo.GetComponent<ThalmicMyo> ();
	}

	public void MyoRotation(string param)
	{
		string[] arr = param.Split (' ');
		myo.transform.eulerAngles = new Vector3 (float.Parse (arr [1]), float.Parse (arr [2]), float.Parse (arr [0]));
	}

	public void MyoPose(string param)
	{
		curPose = param;
		if( param != null)
		{
			switch(param)
			{
			case "Rest":
				thalmicMyo.pose = Pose.Rest;
				break;
			case "Tap":
				thalmicMyo.pose = Pose.DoubleTap;
				break;
			case "Fist":
				thalmicMyo.pose = Pose.Fist;
				break;
			case "Wave_In":
				thalmicMyo.pose = Pose.WaveIn;
				break;
			case "Wave_Out":
				thalmicMyo.pose = Pose.WaveOut;
				break;
			case "Spread":
				thalmicMyo.pose = Pose.FingersSpread;
				break;
			}
		}
	}

	public string GetMyoPose() {return curPose;}
}