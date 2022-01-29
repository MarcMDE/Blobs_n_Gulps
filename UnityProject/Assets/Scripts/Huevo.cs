using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Huevo : MonoBehaviour
{
    private GameObject newHuevo;
    private Rigidbody rb;

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space")){
            StartCoroutine(GenerateHUEVO());
        }
    }
    IEnumerator GenerateHUEVO(){
        newHuevo = Instantiate(transform.GetChild(0).gameObject, transform.GetChild(0).position, transform.GetChild(0).rotation, transform);
        yield return moveHUEVO(newHuevo, 0.5f, 4);
        yield return throwHUEVO(newHuevo,30,15,1000,10);
    }
    IEnumerator moveHUEVO(GameObject newHuevo, float time, float dist){
        float elapsedTime = 0f;
        Vector3 ogPos = newHuevo.transform.position;
        Vector3 newPos = new Vector3(ogPos.x, ogPos.y + dist, ogPos.z);

        while(elapsedTime < time){
            newHuevo.transform.position = Vector3.Lerp(ogPos, newPos, elapsedTime/time );

            elapsedTime += Time.deltaTime;
            yield return null;
        }
        newHuevo.transform.position = newPos;
        yield return null;
    }
    IEnumerator throwHUEVO(GameObject newHuevo, float maxAngle, float minAngle, float maxForce, float minForce){
        float angle = Random.Range(maxAngle, minAngle);
        float angle2 = Random.Range(0f, 360f);

        Vector3 upwardVec = new Vector3(Random.Range(1f, -1f),0,Random.Range(1f, -1f));
        Vector3 DirectionVector = Vector3.up;
        Quaternion rot = Quaternion.AngleAxis(angle2, DirectionVector);
        newHuevo.transform.rotation = rot;

        Quaternion rot2 = Quaternion.AngleAxis(angle, newHuevo.transform.right);
        Vector3 finalVector = rot * rot2 * DirectionVector;

        //Debug.DrawLine(newHuevo.transform.position, newHuevo.transform.position + finalVector*100, Color.red, 100f);
        rb = newHuevo.GetComponent<Rigidbody>();
        rb.isKinematic = false;
        rb.AddForce(finalVector.normalized * maxForce);
        rb.useGravity = true;
        yield return null;
    }
}
