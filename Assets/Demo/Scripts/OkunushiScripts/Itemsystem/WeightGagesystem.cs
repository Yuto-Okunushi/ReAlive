using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeightGagesystem : MonoBehaviour
{
    [SerializeField] Slider weightSlider;

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
        int totalItem = GameManager.GetItemTotal();
        weightSlider.value = totalItem;
    }

    public void UpdateSlider()
    {
        int totalItem = GameManager.GetItemTotal();
        weightSlider.value = totalItem;
    }
}
