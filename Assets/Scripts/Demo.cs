using UnityEngine;
using System.Collections;

public class Demo : MonoBehaviour
{
	public Material m_Mat = null;
	public float Xpos;
	public float Ypos;
	[Range(0.01f, 0.2f)] public float m_DarkRange = 0.1f;
	[Range(-2.5f, -1f)] public float m_Distortion = -2f;
	[Range(0.05f, 0.3f)] public float m_Form = 0.2f;
	
	void Start ()
	{
		if (!SystemInfo.supportsImageEffects)
			enabled = false;
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