using UnityEngine;
using System;
using System.IO;
using System.Collections;

[RequireComponent(typeof(SteamVR_TrackedObject))]
public class controller_script : MonoBehaviour {
	/*public enum Controller
	{
		Left,
		Right
	}

	public Controller WhichController;*/

	public enum Direction
	{
		ToBlueBlack,
		ToWhiteGold
	}
	public static Direction WhichDirection = Direction.ToWhiteGold;
	public static bool status = false;

	SteamVR_TrackedObject trackedObj;
	SteamVR_Controller.Device device;
	dress_texture dressScript1;
	public GameObject text;
	public static int count;
	public static int testCount;
	public static int[][] data;
	bool frist;
	bool trigger;
	bool touchpad;
	public static int testperGroup = 6;//yi gong ji zu
	public static int testNum = 2;//mei zu ji ci

	//dresshand_texture dressScript2;

	void Awake()
	{
		trackedObj = GetComponent<SteamVR_TrackedObject>();
	}

	// Use this for initialization
	void Start () {
		data = new int[testNum] [];
		for (int i = 0; i < testNum; ++i) {
			data [i] = new int[testperGroup];
		}
		count = 0;
		testCount = 0;
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
		if (count > testperGroup) {
			return;
		}
		if (count == testperGroup) {
			if (testCount == testNum - 1) {
				text.GetComponent<TextMesh> ().text = "实验结束";
				++count;
				string filename = "./data/" + DateTime.Now.ToString ("yyyy-MM-dd-HH-mm") + ".txt";
				FileStream fs = new FileStream (filename, FileMode.Create);
				fs.Close ();
				StreamWriter sw = new StreamWriter (filename, false);
				for (int i = 0; i < testNum; ++i) {
					sw.Write (string.Format ("第{0}组 :", i + 1));
					for (int j = 0; j < testperGroup; ++j) {
						sw.Write (string.Format (" {0}", data [i] [j]));
					}
					sw.WriteLine ("");
				}		
				sw.Close ();
				return;
			} else {
				++testCount;
				text.GetComponent<TextMesh> ().text = string.Format ("第{0}组已完成,按任意按钮开始下一组", testCount);
				count = 0;
				if (testCount % 2 == 1) {
					dressScript1.addIndex (dressScript1.T_num);
					WhichDirection = Direction.ToBlueBlack;
				} else {
					dressScript1.addIndex (-dressScript1.T_num);
					WhichDirection = Direction.ToWhiteGold	;
				}
			}
		}
		if (touchpad || trigger)
		{
			if(dressScript1.Flashing)
			{
				return;
			} else if (status == false) {
				status = true;
				frist = true;
				text.SetActive (false);
				text.GetComponent<TextMesh> ().text = "按任意按钮继续实验";
				dressScript1.show ();
				return;
			} else {
				if (trigger) {
					if (WhichDirection == Direction.ToBlueBlack) {
						frist = false;
						dressScript1.moveLeft ();
						dressScript1.hideForSeconds (0.5f);
					} else {
						if (frist) {
							return;
						}
						data [testCount] [count] = dressScript1.Index;
						Debug.Log (string.Format ("{0}", dressScript1.Index));
						++count;
						dressScript1.hide ();
						dressScript1.addIndex (60);//2-1=?hier,unter+1
						WhichDirection = Direction.ToBlueBlack;
						status = false;
						text.SetActive (true);
					}
				} else if (touchpad){
					if (WhichDirection == Direction.ToWhiteGold) {
						frist = false;
						dressScript1.moveRight ();
						dressScript1.hideForSeconds (0.5f);
					} else {
						if (frist) {
							return;
						}
						data [testCount] [count] = dressScript1.Index;
						Debug.Log (string.Format ("{0}", dressScript1.Index));
						++count;
						dressScript1.hide ();
						dressScript1.addIndex (-60);////2-1=?hier,negative
						WhichDirection = Direction.ToWhiteGold;
						status = false;
						text.SetActive (true);
					}
				}
			}
		}
	}
}
