using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkPlayer : NetworkBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (isLocalPlayer)
            Camera.main.GetComponent<FPSCharacterController>().playerRB =
                GetComponent<Rigidbody>();
    }
}
