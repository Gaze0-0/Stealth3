using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class Player : NetworkBehaviour
{
    [SerializeField] private float moveSpeed;

    [Client]
    private void Update()
    {
        if(!hasAuthority) { return; } //checks to see if client has authority to control player
        
    }

    private void Movement()
    {
        Debug.Log("test0");
        if (!Input.GetKey(KeyCode.W)) { return; }
        transform.Translate(0, moveSpeed, 0);
        Debug.Log("move up");
        if (!Input.GetKey(KeyCode.S)) { return; }
        transform.Translate(0, -1 * moveSpeed, 0);
        Debug.Log("move down");
        if (!Input.GetKey(KeyCode.D)) { return; }
        transform.Translate(moveSpeed, 0, 0);
        Debug.Log("move left");
        if (!Input.GetKey(KeyCode.A)) { return; }
        transform.Translate(-1 * moveSpeed, 0, 0);
        Debug.Log("move right");
        Debug.Log("test1");

        Debug.Log("test2");
    }


}
