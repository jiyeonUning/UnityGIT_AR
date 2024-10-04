using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class Shooter : MonoBehaviour
{
    [SerializeField] GameObject ballPrefab;
    [SerializeField] float shootSpeed;

    [SerializeField] ARRaycastManager raycastManager;

    public void Shoot()
    {
        //GameObject ball = Instantiate(ballPrefab, Camera.main.transform.position, Camera.main.transform.rotation);
        //Rigidbody rigidbody = ball.GetComponent<Rigidbody>();
        //rigidbody.velocity = shootSpeed * Camera.main.transform.forward;

        Ray ray = new Ray();
        ray.origin = Camera.main.transform.position;
        ray.direction = Camera.main.transform.forward;

        List<ARRaycastHit> hits = new List<ARRaycastHit>();

        if (raycastManager.Raycast(ray, hits))
        {
            Instantiate(ballPrefab, hits[0].pose.position, hits[0].pose.rotation);
        }
    }
}
