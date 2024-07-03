using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMoves : MonoBehaviour
{
    [SerializeField] Canvas shopcanvs;
    [SerializeField] GameObject shopobject;
    [SerializeField] GameObject inventory;
    [SerializeField] Timer timer;
    [SerializeField] Moisturegauge moisturegauge;
    [SerializeField] Foodgauge foodgauge;

    public int playerinitialmony = 3000;        //プレイヤーの最初に所持している金額
    public float moveSpeed = 5f;
    public float turnSpeed = 10f;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;

        // X軸とZ軸の回転を固定
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
    }

    void Update()
    {
        ObjectOpen();

        GameManager.SetPlayerMony(playerinitialmony);

        // shopcanvsがアクティブでない場合にのみ移動と回転を許可
        if (!shopcanvs.gameObject.activeSelf && !inventory.gameObject.activeSelf)
        {
            Move();
            Turn();
            timer.TimerStartStop();
            moisturegauge.MoisturegaugeStartStop();
            foodgauge.FoodgaugeStartStop();
        }
        
    }

    void Move()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical).normalized * moveSpeed * Time.deltaTime;
        rb.MovePosition(transform.position + movement);
    }

    void Turn()
    {
        float turnHorizontal = Input.GetAxis("Mouse X");
        float turnVertical = -Input.GetAxis("Mouse Y");

        Vector3 rotation = new Vector3(turnVertical, turnHorizontal, 0.0f) * turnSpeed * Time.deltaTime;
        rb.MoveRotation(rb.rotation * Quaternion.Euler(rotation));
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == shopobject)
        {
            Openshopcanvs();
        }
    }

    public void Openshopcanvs()
    {
        shopcanvs.gameObject.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Debug.Log("Shop canvas opened");
    }

    void ObjectOpen()
    {
        // shopcanvsがアクティブでない場合にのみスペースキーの入力を受け付ける
        if (!shopcanvs.gameObject.activeSelf && Input.GetKeyDown(KeyCode.Space))
        {
            bool isActive = !inventory.gameObject.activeSelf;
            inventory.gameObject.SetActive(isActive);
            Debug.Log("スペースキーが押されました");
            if (isActive)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }
    }
}
