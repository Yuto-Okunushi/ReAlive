using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    [SerializeField] SignDate[] signDate;

    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Sign1")
        {
            Debug.Log("•W¯1‚É‚Ô‚Â‚©‚è‚Ü‚µ‚½");
            SendSignDate(0);
        }
        else if (other.gameObject.tag == "Sign2")
        {
            Debug.Log("•W¯2‚É‚Ô‚Â‚©‚è‚Ü‚µ‚½");
            SendSignDate(1);
        }
        else if (other.gameObject.tag == "Sign3")
        {
            Debug.Log("•W¯2‚É‚Ô‚Â‚©‚è‚Ü‚µ‚½");
            SendSignDate(2);
        }
    }

    private void SendSignDate(int index)
    {
        if (index >= 0 && index < signDate.Length)
        {
            GameManager.SetSignDate(signDate[index]);
        }
        else
        {
            Debug.LogError("Invalid index for signDate array");
        }
    }
}
