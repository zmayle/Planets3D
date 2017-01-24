using UnityEngine;
using System.Collections;

public class BlackHole : MonoBehaviour {

	public Material m_Mat = null;
	public GameObject blackHole;
	[Range(0.01f, 0.2f)] public float m_DarkRange = 0.015f;
	[Range(-2.5f, -1f)] public float m_Distortion = -1.35f;
	[Range(0.05f, 0.3f)] public float m_Form = 0.05f;

	private float Xpos; // the black hole's x position on the screen (relative to the full width of the screen)
	private float Ypos; // the black hole's y position on the screen (relative to the full height of the screen)

	void Start ()
	{
		if (!SystemInfo.supportsImageEffects)
			enabled = false;
		float worldx = blackHole.transform.position.x;
		float worldz = blackHole.transform.position.z;
		Xpos = (worldx / 32.0f) + 0.5f;
		Ypos = (worldz / 24.0f) + 0.5f;
	}

	void OnRenderImage (RenderTexture sourceTexture, RenderTexture destTexture)
	{
		m_Mat.SetVector ("_Center", new Vector4 (Xpos, Ypos, 0f, 0f));
		m_Mat.SetFloat ("_DarkRange", m_DarkRange);
		m_Mat.SetFloat ("_Distortion", m_Distortion);
		m_Mat.SetFloat ("_Form", m_Form);
		Graphics.Blit (sourceTexture, destTexture, m_Mat);
	}
}
