using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogOfWar : MonoBehaviour
{
    Transform player;
    Vector2 refVel = Vector2.zero;
    PlayerYoung fogSize;
    ParticleSystem fog;
    public bool changed = false;       // 사이즈가 계속 바뀌는 것을 막기 위한 키


    private void Start()
    {
        fog = gameObject.GetComponent<ParticleSystem>();
    }

    void Update()
    {
        player = GameManager.Instance.currentplayer.transform;
        transform.position = Vector2.SmoothDamp(transform.position, player.position, ref refVel, 0.5f);
        if (fogSize == null)
        {
            GameManager.Instance.currentplayer.TryGetComponent(out PlayerYoung playerYoung);
            fogSize = playerYoung;
        }
        else if (fogSize != null)
        {
            SizeChange();
            ChangedCheck();
        }
    }

    private void SizeChange()
    {
        Vector2 sizeUp = new Vector2(fog.shape.scale.x + 2f, fog.shape.scale.y + 0.5f);
        Vector2 sizeDown = new Vector2(fog.shape.scale.x - 2f, fog.shape.scale.y - 0.5f);
        ParticleSystem.ShapeModule shapeModule = fog.shape;

        if (!changed)
        {
            if (fogSize.happyCount > 1 && fogSize.happyCount % 15 == 0)
            {
                shapeModule.scale = sizeUp;
                changed = true;
            }
            else if (fogSize.badCount > 1 && fogSize.badCount % 5 == 0)
            {
                shapeModule.scale = sizeDown;
                changed = true;
            }
        }
    }

    private void ChangedCheck()
    {
        if (fogSize.happyCount % 15 != 0 && fogSize.badCount % 5 != 0 && changed)
        {
            changed = false;
        }
    }
}
