using UnityEngine;
using System.Collections;

//add this component to camera
public class RotateObject : MonoBehaviour {

    public static Transform selected;

    void Update() {
        if(Input.GetMouseButtonDown(0)) {
            
            Vector2 origin =new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);

            RaycastHit2D hit = Physics2D.Raycast(origin, Vector2.zero, 0f);

            if (hit)
            {

                if (hit.collider.tag != "MainCamera")
                {
                    selected = hit.transform;
                }
            }

        }

        if(Input.GetKey(KeyCode.A) && selected) {
                selected.Rotate(Vector3.forward*40*Time.deltaTime);
        }

        if(Input.GetKey(KeyCode.D) && selected) {
                selected.Rotate(-Vector3.forward*40*Time.deltaTime);
        }
        
    }


    

}
