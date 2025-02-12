using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpData : MonoSingleton<PopUpData>
{
    [SerializeField]
    public List<BasePopUp> allPopUpList;
}
