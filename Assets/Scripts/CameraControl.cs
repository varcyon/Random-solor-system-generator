using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraControl : MonoBehaviour
{
    [SerializeField] private float zoomSpeed = 10f;
    [SerializeField] private float minZoomDist = 10f;
    [SerializeField] private float maxZoomDist = 50f;
    [SerializeField] private float panSpeed = 1000f;
    [SerializeField] private float panEdgeBoarder =25f;

    [SerializeField] private CinemachineVirtualCamera panCam;
    [SerializeField] private CinemachineFreeLook orbitCam;

    [SerializeField] public static bool focused;

    Vector3 pcam;
    private void Start()
    {
    }
    private void Update()
    {
        if (UnitManager.UM.selectedStructures.Count > 0)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {   
                if (panCam.gameObject.activeInHierarchy)
                {
                    focused = true;
                    panCam.gameObject.SetActive(false);
                    orbitCam.GetComponent<CinemachineFreeLook>().Follow = UnitManager.UM.selectedStructures[0].transform.GetChild(0).transform;
                    orbitCam.GetComponent<CinemachineFreeLook>().LookAt = UnitManager.UM.selectedStructures[0].transform.GetChild(0).transform;
                    orbitCam.gameObject.SetActive(true);
                    UnitManager.UM.selectedStructures[0].GetComponent<CelestialBody>().DeSelect();
                } else
                {
                    focused = false;
                    UnitManager.UM.selectedStructures.RemoveAt(0);
                    orbitCam.GetComponent<CinemachineFreeLook>().Follow = null;
                    orbitCam.GetComponent<CinemachineFreeLook>().LookAt = null;

                    panCam.gameObject.SetActive(true);
                    orbitCam.gameObject.SetActive(false);
                }
            }
        }
        PanCamera();
       // EdgePan();
        Zoom();
        //FocusCamra();
    }
    public void PanCamera()
    {
        //pans camera
        if (Input.GetMouseButton(2))
        {
            panCam.transform.position -= new Vector3(Input.GetAxis("Mouse X"), 0, Input.GetAxis("Mouse Y")) * panSpeed * Time.deltaTime;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 dir = transform.forward * z + transform.right * x;
        panCam.transform.position += dir * panSpeed * Time.deltaTime;
    }

    public void Zoom()
    {   //zooms the camera on its forward vector
        float scrollInput = Input.GetAxis("Mouse ScrollWheel");
        // finds the distance from the center of everything to the camera
        float zoomDist = Vector3.Distance(new Vector3(0, 0, 0), panCam.transform.position);
        //returns ( stops ) zooming if its the min distance or max distance
        if (zoomDist < minZoomDist && scrollInput > 0.0f)
            return;
        else if (zoomDist > maxZoomDist && scrollInput < 0.0f)
            return;
        // actuall moves the camera
        panCam.transform.position += panCam.transform.forward * scrollInput * zoomSpeed;
    }

   

    public void EdgePan()
    {   // if mouse position is panEdgeBoarder( 25 ) from the top , pans up, down, left, right
        if (Input.mousePosition.y <= Screen.height - panEdgeBoarder)
        {
            panCam.transform.position += new Vector3(0,0, panSpeed * Time.deltaTime);
        }

        if (Input.mousePosition.y >= panEdgeBoarder)
        {
            panCam.transform.position -= new Vector3(0, 0, panSpeed * Time.deltaTime);
        }

        if (Input.mousePosition.x <= Screen.width - panEdgeBoarder)
        {
            panCam.transform.position -= new Vector3(panSpeed * Time.deltaTime, 0,0 );
        }

        if (Input.mousePosition.x >= panEdgeBoarder)
        {
            panCam.transform.position += new Vector3(panSpeed * Time.deltaTime, 0, 0);
        }

    }
     public void FocusCamra()
    {
        if (Input.GetKey(KeyCode.Space) && UnitManager.UM.selectedStructures.Count != 0)
        {
            Vector3 selectedPos = UnitManager.UM.selectedStructures[0].transform.position;
            GameObject selected = UnitManager.UM.selectedStructures[0];
            CelestialBody celestialBody = selected.GetComponent<CelestialBody>();
            float dia = celestialBody.GetDiameter();
            selectedPos = new Vector3(selectedPos.x, dia *3, -dia * 6);
            panCam.transform.position = selectedPos;
            
        }
    }


}
