using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField]
    private float cameraSpeed = 0;
    private float xMax;
    private float yMin;

    // Update is called once per frame
    void Update()
    {
        GetInput();
    }

            // deltaTime -> taking into account how fast the update method is called
            // in order to have the same speed for every device
    private void GetInput(){
        if (Input.GetKey(KeyCode.W)){
            transform.Translate(Vector3.up * cameraSpeed * Time.deltaTime);
        }
                if (Input.GetKey(KeyCode.A)){
            transform.Translate(Vector3.left * cameraSpeed * Time.deltaTime);
        }
                if (Input.GetKey(KeyCode.S)){
            transform.Translate(Vector3.down * cameraSpeed * Time.deltaTime);
        }
                if (Input.GetKey(KeyCode.D)){
            transform.Translate(Vector3.right * cameraSpeed * Time.deltaTime);
        }

        transform.position = new Vector3(Mathf.Clamp(transform.position.x, 0, xMax), Mathf.Clamp(transform.position.y, yMin, 0), -1);
    }
    
    public void SetLimits(Vector3 maxTile){
        Vector3 worldPoint = Camera.main.ViewportToWorldPoint(new Vector3(1,0));
        xMax = maxTile.x - worldPoint.x;
        yMin = maxTile.y - worldPoint.y;
    }
}
