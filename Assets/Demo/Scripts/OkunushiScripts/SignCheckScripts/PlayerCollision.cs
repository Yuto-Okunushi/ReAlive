using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Signs")
        {
            Debug.Log("•WŽ¯‚É‚Ô‚Â‚©‚è‚Ü‚µ‚½");
        }
        else
        {

        }
    }
}
