using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine.Utility;
using Cinemachine;

public class OrbitCamControl : MonoBehaviour
{
   [SerializeField] private CinemachineFreeLook cam;
    private bool activateFreelook;
    private float zoom = 1f;
   private  float orbitZoomSpeed = 1f;

    void Start()
    {
        cam = GetComponent<CinemachineFreeLook>();
        //axis for the free look cam
        CinemachineCore.GetInputAxis = GetInputAxis;
    }

    void Update()
    {   //sets this to true if the right mouse button is held
        activateFreelook = Input.GetMouseButton(1);
        GetZoomAxis();

        // dynamicly changes freelook orbit rig to the size of what is focused ( followed )
        float d = UnitManager.UM.selectedStructures[0].GetComponent<CelestialBody>().GetDiameter();
        Debug.Log(d);
        cam.m_Orbits[0].m_Height =(float)( d * 1.5) * zoom;
        cam.m_Orbits[0].m_Radius = (float)(d * 5)* zoom;

        cam.m_Orbits[1].m_Height = 0;
        cam.m_Orbits[1].m_Radius = (float)(d * 6) * zoom;

        cam.m_Orbits[2].m_Height = (float)(-d * 1.5) * zoom;
        cam.m_Orbits[2].m_Radius = (float)(d * 5) *zoom;
    }
    private void GetZoomAxis()
    {
        float scrollInput = Input.GetAxis("Mouse ScrollWheel");
        zoom -= scrollInput* orbitZoomSpeed;
    }
    // activates freelook only if right button is held
    private float GetInputAxis(string axisName)
    {   //returns Mouse X and Y axis if activateFreelook is true if its not true it returns a 0 for the Axis
        return !activateFreelook ? 0 : Input.GetAxis(axisName == "Mouse Y" ? "Mouse Y" : "Mouse X");
    }
}
