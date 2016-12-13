using UnityEngine;
using System;
using System.Collections;
using System.IO;
using UnityEngine.Scripting;

public class dress_texture : MonoBehaviour {

	public const int t_num = 500;//pic?->?
	public int t_index;
	public const int timesPerPicture = 10;
	[Range(0,t_num - 1)]
	public int left;
	[Range(0,t_num - 1)]
	public int right;

	GameObject dress;
	Material material;
	Texture[] tex;
	bool flashing = false;
	float flashingTime;
	long ticks;

	int range;
	int picNum;
	public int[] index;
	public int[] times;
	public int[] data;
	int currentIndex;
	public int count;
	public int testNum;
	public int maxPicNum;
	System.Random rand;

	// Use this for initialization
	void Start () {
		range = right - left + 1;
		if (range < maxPicNum) {
			picNum = range;
			index = new int[picNum];
			times = new int[picNum];
			data = new int[picNum];
			for (int i = 0; i < picNum; ++i) {
				index [i] = left + i;
				times [i] = 10;
				data [i] = 0;
			}
		} else {
			picNum = maxPicNum;
			index = new int[picNum];
			times = new int[picNum];
			data = new int[picNum];
			for (int i = 0; i < picNum; ++i) {
				index [i] = left + (int)((float)i * (float)range / (float)(maxPicNum - 1) + 0.5f);
				times [i] = 10;
				data [i] = 0;
			}
			--index [picNum - 1];
		}

		testNum = picNum * 10;
		count = 0;
		rand = new System.Random ();
		currentIndex = rand.Next (0, picNum);
		times [currentIndex]--;
		t_index = index [currentIndex];
		dress = GameObject.Find ("pDress1");
		//dress = GameObject.Find ("dress");
		material = dress.GetComponent<MeshRenderer> ().materials [0];
		tex = new Texture[t_num];
		for (int i = 1; i <= t_num; ++i) {
			tex [i - 1] = (Texture)Resources.Load ("d" + (char)(48 + i / 100)
				+ (char)(48 + (i % 100) / 10)
				+ (char)(48 + i % 10));
		}
		material.mainTexture = tex [t_index];
		hide ();
	}
	
	// Update is called once per frame
	void Update () {
		if (flashing) {
			TimeSpan ts = new TimeSpan (DateTime.Now.Ticks - ticks);
			if (ts.TotalSeconds > flashingTime) {
				show ();
				flashing = false;
			}
		}
	
	}

	public bool changePic(bool blue)
	{
		count++;
		if(blue)
		{
			data [currentIndex]++;
		}
		if(count >= testNum)
		{
			hide ();
			string filename = "./data/" + DateTime.Now.ToString ("yyyy-MM-dd-HH-mm") + ".txt";
			FileStream fs = new FileStream (filename, FileMode.Create);
			fs.Close ();
			StreamWriter sw = new StreamWriter (filename, false);
			sw.Write ("编号 :\t");
			for (int i = 0; i < picNum; ++i) {
				sw.Write (string.Format ("{0}\t", index [i]));
			}
			sw.WriteLine ("");
			sw.Write ("次数\t");
			for (int i = 0; i < picNum; ++i) {
				sw.Write (string.Format ("{0}\t", data [i]));
			}
			sw.WriteLine ("");
			sw.Close ();
			return false;
		}

		bool exit = true;
		while (exit) {
			currentIndex = rand.Next (0, picNum);
			if (times [currentIndex] > 0) {
				exit = false;
			}
		}

		hideForSeconds (0.5f);
		t_index = index [currentIndex];
		times [currentIndex]--;
		material.mainTexture = tex [t_index];

		return true;
	}

		

	public void show()
	{
		dress.SetActive (true);
	}

	public void hide()
	{
		dress.SetActive (false);
	}

	public void hideForSeconds(float seconds)
	{
		hide ();
		flashing = true;
		ticks = DateTime.Now.Ticks;
		flashingTime = seconds;
	}

	public bool Flashing {
		get{ return flashing; }
	}

	public int T_num {
		get { return t_num; }
	}

	public int Index {
		get { return t_index; }
	}
}
