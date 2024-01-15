using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CollectionNotice : MonoBehaviour
{
    public TextMeshProUGUI[] text;
    PlayerYoung player;

    void Start()
    {
        text[0].text = "0";
        text[1].text = "0";
        player = GameManager.Instance.currentplayer.GetComponent<PlayerYoung>();
    }

    void Update()
    {
        text[0].text = player.happyCount.ToString();
        text[1].text = player.badCount.ToString();
    }
}
