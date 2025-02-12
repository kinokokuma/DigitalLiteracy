using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectShopPopup : BasePopUp
{
    public string route = "Route1";
    public Button go1;
    public Button go2;
    public GameObject[] next;
    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
        go1.onClick.AddListener(() => {
            save("Select BagShop",true);
            back($"{route}/story1-10-A");
        });
        go2.onClick.AddListener(() => {
            
            save("Select ForyouBag", true);
            back($"{route}/story1-10-B");
        });
    }

    public void back(string name)
    {
        manager.OpenChat(name);
        gameObject.SetActive(false);
    }
}
