using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obelisk : MonoBehaviour
{
    [SerializeField] GameObject strikePrefab;

    Animator obeliskAnim;
    Camera mainCamera;

    Vector2 aimPoint;

    void Awake()
    {
        obeliskAnim = GetComponent<Animator>();
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    void Update()
    {
        Attack();
    }

    void Attack()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            obeliskAnim.SetTrigger("Attack");

            aimPoint = mainCamera.ScreenToWorldPoint(Input.mousePosition);

            Instantiate(strikePrefab, aimPoint, Quaternion.identity);
        }
    }
}
