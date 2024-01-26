using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{
    private CharacterController controller;
    public static float playerSpeed = 20.0f;

    private void Start()
    {
        controller = gameObject.AddComponent<CharacterController>();
    }

    void FixedUpdate()
    {
        // Oyuncu hareketi
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        controller.Move(move * Time.deltaTime * playerSpeed);
        Aim();
    }

    void Aim()
    {
        // Fare pozisyonunu al
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        // Ray ile t�klanan yere do�ru bir �izgi �iz ve �arp��ma kontrol� yap
        if (Physics.Raycast(ray, out hit))
        {
            // Ni�an alma y�n�n� belirle
            Vector3 targetDirection = hit.point - transform.position;
            targetDirection.y = 0f;

            // Topun ni�an alma y�n�ne d�nmesini sa�la
            transform.forward = targetDirection.normalized;
        }
    }
}
