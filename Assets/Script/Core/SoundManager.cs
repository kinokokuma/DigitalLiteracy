using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum SoundID
{
    None,
    touch,
    chatPop,
    newChat,
    suppy,
    clock,
    bell,
    drop,
    openBox,

}

public class SoundManager : MonoSingleton<SoundManager>
{

    public AudioClip touch;
    public AudioClip chatPop;
    public AudioClip newChat;
    public AudioClip suppy;
    public AudioClip clock;
    public AudioClip bell;
    public AudioClip drop;
    public AudioClip openBox;

    public AudioSource source;
    public AudioSource BGMsource;
    
    public Dictionary<SoundID, AudioClip> soundDic = new Dictionary<SoundID, AudioClip>();

    public void Start()
    {
        soundDic[SoundID.touch] = touch;
        soundDic[SoundID.chatPop] = chatPop;
        soundDic[SoundID.newChat] = newChat;
        soundDic[SoundID.bell] = bell;
        soundDic[SoundID.openBox] = openBox;
        soundDic[SoundID.drop] = drop;
        soundDic[SoundID.clock] = clock;
        soundDic[SoundID.suppy] = suppy;

        BGMsource.Play();
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
