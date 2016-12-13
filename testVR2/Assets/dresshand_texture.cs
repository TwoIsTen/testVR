using UnityEngine;
using System.Collections;

public class dresshand_texture : MonoBehaviour {

	public const int t_num = 200;
	public int t_index;
	[Range(1,10)]
	public int step = 3;

	int m_step;
	GameObject dress;
	Material material;
	Texture[] tex;

	// Use this for initialization
	void Start () {
		m_step = 0;
		dress = GameObject.Find ("pDress_hand1");
		material = dress.GetComponent<MeshRenderer> ().materials [0];
		tex = new Texture[t_num];
		for (int i = 0; i < 200; ++i) {
			tex [i] = (Texture)Resources.Load ("output" + (char)(48 + i / 100)
				+ (char)(48 + (i % 100) / 10)
				+ (char)(48 + i % 10));
		}
		material.mainTexture = tex [t_index];
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void moveRight()
	{
		++m_step;
		if (m_step < step) {
			return;
		} else {
			m_step = 0;
		}
		++t_index;
		if(t_index > 199)
			t_index = 199;
		material.mainTexture = tex[t_index];
	}

	public void moveLeft()
	{
		++m_step;
		if (m_step < step) {
			return;
		} else {
			m_step = 0;
		}
		--t_index;
		if(t_index < 0)
			t_index = 0;
		material.mainTexture = tex[t_index];
	}
}
