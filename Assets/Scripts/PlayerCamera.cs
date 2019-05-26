using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour {
	[SerializeField] bool debug = false;
	[Space]

    [Header("Camera")]
    public GameObject followObject;

    [Header("Sway X")]
    [SerializeField] float swayXSpeed = 0.5f;
    [SerializeField] float swayXAmount = 0.5f;

    [Header("Sway Y")]
    [SerializeField] float swayYSpeed = 0.5f;
    [SerializeField] float swayYAmount = 0.5f;

    [Header("Movement")]
    [SerializeField] float moveSpeed = 1f;
    [SerializeField] float xMoveSpeed = 1f;
    [SerializeField] Vector3 movePosition = new Vector3();

    [Header("Rotation")]
    [SerializeField] float rotationSpeed = 1f;
    [SerializeField] Vector3 rotateOffset = new Vector3();

    private void Update() {

        Vector3 newRotate = new Vector3(followObject.transform.position.x + rotateOffset.x, followObject.transform.position.y + rotateOffset.y, followObject.transform.position.z + rotateOffset.z);
        Vector3 newMove = new Vector3(newRotate.x - movePosition.x, newRotate.y + movePosition.y, newRotate.z - movePosition.z);


        Vector3 position = transform.position;
        position.x = Mathf.Lerp(transform.position.x, newMove.x, xMoveSpeed * Time.deltaTime);
        position.y = Mathf.Lerp(transform.position.y, newMove.y, moveSpeed * Time.deltaTime);
        position.z = Mathf.Lerp(transform.position.z, newMove.z, moveSpeed * Time.deltaTime);
        transform.position = position;

        float swayX = (Mathf.PerlinNoise(0, Time.time * swayXSpeed) - 0.5f) * swayXAmount;
        float swayY = (Mathf.PerlinNoise(0, Time.time * swayYSpeed) - 0.5f) * swayYAmount;


        Vector3 newLookAt = newRotate;
        newLookAt.x += swayX;
        newLookAt.y += swayY;
        Quaternion desiredRotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(newLookAt - transform.position), rotationSpeed * Time.deltaTime);
        transform.rotation = desiredRotation;

		if (debug)
        	Debug.DrawLine(newRotate, newMove, Color.green);
    }
}