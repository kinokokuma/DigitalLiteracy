using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum SoundID
{
    touch,
    chatPop,
    newChat,
}

public class SoundManager : MonoSingleton<SoundManager>
{

    public AudioClip touch;
    public AudioClip chatPop;
    public AudioClip newChat;
    public AudioSource source;
    public Dictionary<SoundID, AudioClip> soundDic = new Dictionary<SoundID, AudioClip>();

    public void Start()
    {
        soundDic[SoundID.touch] = touch;
        soundDic[SoundID.chatPop] = chatPop;
        soundDic[SoundID.newChat] = newChat;
    }
    public void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            PlaySound(SoundID.touch);
        }
    }

    public void PlaySound(SoundID ID,float volume = 1)
    {

        source.PlayOneShot(soundDic[ID], volume);
        
    }
    
}
