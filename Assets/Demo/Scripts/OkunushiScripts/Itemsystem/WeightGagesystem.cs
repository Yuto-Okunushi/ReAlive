using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeightGagesystem : MonoBehaviour
{
    [SerializeField] Slider weightSlider;
    int totalItem = 0;

    // Start is called before the first frame update
    void Start()
    {
        if (weightSlider == null)
        {
            Debug.LogError("Slider reference is not set in WeightGagesystem script!");
            return;
        }

        // Initialize the slider value
        UpdateSlider();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateSlider();
    }

    public void UpdateSlider()
    {
        totalItem = GameManager.GetItemTotal();
        weightSlider.value = totalItem;
        Debug.Log(totalItem);
    }
}
