using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoonSlice : Bullet
{
    public Transform currentPlayer;
    private Vector2 playerAim;
    public float speed;
    Rigidbody2D rb;

    private void OnEnable()
    {
        rb = GetComponent<Rigidbody2D>();
        currentPlayer = GameManager.Instance.currentplayer.transform;
        rb.transform.position = currentPlayer.position;
        Vector2 mousePositionScreen = Input.mousePosition;
        Vector2 mousePositionWorld = Camera.main.ScreenToWorldPoint(new Vector2(mousePositionScreen.x, mousePositionScreen.y));
        playerAim = mousePositionWorld - (Vector2)currentPlayer.position;
        playerAim.Normalize();
    }

    void Update()
    {
        rb.velocity = playerAim * speed;
    }
}
