using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Modes { GOD, FOCUSED};

public class CameraController : MonoBehaviour
{
    [SerializeField] float bordersRatio = 0.1f;
    [SerializeField] float cameraSpeed = 15;
    [SerializeField] Vector3 followingOffset;
    [SerializeField] float followingHeight;

    float godHeight;

    Modes mode;

    GameObject selectedAgent;

    void Start()
    {
        godHeight = transform.position.y;
        mode = Modes.GOD;
    }

    void Update()
    {
        if (selectedAgent != null && !selectedAgent.activeInHierarchy)
        {
            mode = Modes.GOD;
            transform.position = new Vector3(transform.position.x, godHeight, transform.position.z);
            selectedAgent = null;
        }

        if (mode == Modes.GOD)
        {
            Vector3 vPos = Camera.main.ScreenToViewportPoint(Input.mousePosition);
            //Debug.Log("Mouse: " + Input.mousePosition.x + " - Width: " + Screen.currentResolution.width);
            if (vPos.x < bordersRatio && transform.position.x > -Globals.WorldSize/2)
            {
                transform.Translate(new Vector3(-1, 0, 0) * cameraSpeed * Time.deltaTime);
            }
            else if (vPos.x > 1 - bordersRatio && transform.position.x < Globals.WorldSize / 2)
            {
                transform.Translate(new Vector3(1, 0, 0) * cameraSpeed * Time.deltaTime);
            }

            if (vPos.y < bordersRatio && transform.position.z > -Globals.WorldSize * 0.75f)
            {
                transform.Translate(new Vector3(0, 0, -1) * cameraSpeed * Time.deltaTime);
            }
            else if (vPos.y > 1 - bordersRatio && transform.position.z < 0)
            {
                transform.Translate(new Vector3(0, 0, 1) * cameraSpeed * Time.deltaTime);
            }
        }
        else
        {
            transform.position = new Vector3(
                selectedAgent.transform.position.x + followingOffset.x,
                followingHeight,
                selectedAgent.transform.position.z + followingOffset.z);

            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (selectedAgent != null)
                    selectedAgent.GetComponent<AgentController>().LeaveBody();

                mode = Modes.GOD;
                transform.position = new Vector3(transform.position.x, godHeight, transform.position.z);
            }

            if (Input.GetMouseButtonDown(1))
            {
                RaycastHit hit;
                var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out hit, 200, Globals.GROUND_LAYER))
                {
                    selectedAgent.GetComponent<AgentController>().SetDestination(hit.point);
                }
            }
            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit hit;
                var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out hit, 200, 128))
                {
                    Food food = hit.collider.GetComponent<Food>();
                    if (!food.Selected)
                    {
                        food.Select();
                        selectedAgent.GetComponent<AgentController>().pickup(food);
                    }
                }
            }



        }

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            // Layer mask Glup+Blob --> 2^11 + 2^12
            if (Physics.Raycast(ray, out hit, 200, 6144))
            {
                if (selectedAgent != null)
                    selectedAgent.GetComponent<AgentController>().LeaveBody();

                selectedAgent = hit.collider.gameObject;
                selectedAgent.GetComponent<AgentController>().Posses();

                mode = Modes.FOCUSED;
            }
        }

    }
}
