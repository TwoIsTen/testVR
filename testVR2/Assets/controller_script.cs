using UnityEngine;
using System;
using System.IO;
using System.Collections;

[RequireComponent(typeof(SteamVR_TrackedObject))]
public class controller_script : MonoBehaviour {
	bool status;
	bool stop;

	SteamVR_TrackedObject trackedObj;
	SteamVR_Controller.Device device;
	dress_texture dressScript1;
	public GameObject text;
	bool trigger;
	bool touchpad;

	//dresshand_texture dressScript2;

	void Awake()
	{
		trackedObj = GetComponent<SteamVR_TrackedObject>();
	}

	// Use this for initialization
	void Start () {
		status = false;
		stop = false;
		device = SteamVR_Controller.Input((int)trackedObj.index);
		dressScript1 = GameObject.Find ("dress").GetComponent<dress_texture> ();
		//text = GameObject.Find ("Text");
		text.GetComponent<TextMesh> ().text = "按任意按钮开始实验";
		text.SetActive (true);	
	}
	
	// Update is called once per frame
	void Update () {
		touchpad = device.GetPressUp (SteamVR_Controller.ButtonMask.Touchpad);
		trigger = device.GetPressUp (SteamVR_Controller.ButtonMask.Trigger);
		if (stop) {
			return;
		}
		if (touchpad || trigger)
		{
			if(dressScript1.Flashing)
			{
				return;
			} else if (status == false) {
				status = true;
				text.SetActive (false);
				text.GetComponent<TextMesh> ().text = "实验结束";
				dressScript1.show ();
				return;
			} else {
				bool temp = true;
				if (trigger) {
					temp = dressScript1.changePic (true);
				} else if (touchpad){
					temp = dressScript1.changePic (false);
				}
				if (temp == false) {
					stop = true;
					status = false;
					text.SetActive (true);
				}
			}
		}
	}
}
