using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhiteBall : MonoBehaviour
{
    protected GameManager gameManager;
    public Vector3 respawnPosition;
    private new Rigidbody rigidbody;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        rigidbody = GetComponent<Rigidbody>();
    }

    public void RespawnWhiteBall()
    {
        if (gameManager.canRespawn)
        {
            gameObject.transform.position = respawnPosition;
            rigidbody.velocity = Vector3.zero;
            rigidbody.angularVelocity = Vector3.zero;
            gameManager.isPlayer1Turn = !gameManager.isPlayer1Turn;
        }
        
    }
}
