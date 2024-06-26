using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeightGagesystem : MonoBehaviour
{
    private int totalItem;
    [SerializeField] Slider weightslider;

    // Start is called before the first frame update
    void Start()
    {
        // Ensure the weightslider reference is set
        if (weightslider == null)
        {
            Debug.LogError("Slider reference is not set in WeightGagesystem script!");
            return;
        }
    }

    // Update is called once per frame
    void Update()
    {
        ItemCountsCheck();
    }

    private void ItemCountsCheck()
    {
        // Get total item count from GameManager and assign it to totalItem
        

        // Update weightslider value
        weightslider.value = totalItem;
    }
}
