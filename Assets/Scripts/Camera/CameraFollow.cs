using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform target;
    Vector3 targetPosition;
    [SerializeField] float smooth; //camera chuyển động mượt đến target
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("Car").GetComponent<Transform>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        targetPosition = new Vector3(target.transform.position.x, target.transform.position.y,-10f);
        this.transform.position = Vector3.Lerp(this.transform.position,targetPosition,smooth * Time.fixedDeltaTime);
        
    }
}
