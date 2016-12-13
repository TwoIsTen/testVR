using UnityEngine;
using System;
using System.Collections;
using UnityEngine.Scripting;

public class dress_texture : MonoBehaviour {

	public const int t_num = 500;//pic num?
	public int t_index = 0;
	[Range(1,40)]
	public int step = 10;

	GameObject dress;
	Material material;
	Texture[] tex;
	bool flashing = false;
	float flashingTime;
	long ticks;

	// Use this for initialization
	void Start () {
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

	public void moveRight()
	{
		t_index += step;
		if(t_index >= t_num)
			t_index = t_num - 1;
		material.mainTexture = tex[t_index];
	}

	public void moveLeft()
	{
		t_index -= step;
		if(t_index < 0)
			t_index = 0;
		material.mainTexture = tex[t_index];
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

	public void addIndex(int index)
	{
		t_index += index;
		if (t_index < 0) {
			t_index = 0;
		} else if (t_index >= t_num) {
			t_index = t_num - 1;
		}
		material.mainTexture = tex[t_index];
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
