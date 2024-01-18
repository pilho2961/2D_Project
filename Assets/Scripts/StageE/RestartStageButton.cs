using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class RestartStageButton : MonoBehaviour
{
    Button button;
    public Transform playerInitPosition;
    public GameObject player;
    public ParticleSystem fog;

    private void Awake()
    {
        button = GetComponentInChildren<Button>();
        player = GameManager.Instance.currentplayer;
    }

    public void ResetPlayerPosition()
    {
        player.transform.position = playerInitPosition.position;
        ParticleSystem.ShapeModule shapeModule = fog.shape;
        shapeModule.scale = new Vector2(10f, 9f);
    }
}
