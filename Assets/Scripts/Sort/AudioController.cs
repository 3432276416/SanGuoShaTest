using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour {
	public bool gender;//True等于男 //False 等于女
	public List<AudioClip> audioLiatMan;//男音效
	public List<AudioClip> audioLiatGirl;//女音效

	public AudioSource audioSource;

	// Use this for initialization
	void Start () {
		audioSource = GetComponent<AudioSource>();
	}
	/// <summary>
	/// 得到男声音
	/// </summary>
	/// <param name="name"></param>
	/// <returns></returns>
	public AudioClip GetManAudioClip(string name)
    {
		for(int i = 0; i < audioLiatMan.Count; i++)
        {
			if(name == audioLiatMan[i].name)
            {
				return audioLiatMan[i];
            }
        }
		return null;
	}
	/// <summary>
	/// 得到女声音
	/// </summary>
	/// <param name="name"></param>
	/// <returns></returns>
	public AudioClip GetGirlAudioClip(string name)
	{
		for (int i = 0; i < audioLiatGirl.Count; i++)
		{
			if (name == audioLiatGirl[i].name)
			{
				return audioLiatGirl[i];
			}
		}
		return null;
	}
	/// <summary>
	/// 播放音效
	/// </summary>
	/// <param name="name"></param>
	public void PlayAudio(string name)
    {
        if (gender)
        {
			audioSource.clip = GetManAudioClip(name);
        }
        else
        {
			audioSource.clip = GetGirlAudioClip(name);
		}
		audioSource.Play();
    }
}
