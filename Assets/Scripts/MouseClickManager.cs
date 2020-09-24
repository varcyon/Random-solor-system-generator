using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class MouseClickManager : MonoBehaviour { 

    [SerializeField] private RectTransform selectionBox;
   [SerializeField] private Vector2 startPos;

    private void Start()
    {
    }
    void Update()
    {
        LeftClick();
        MultiSelect();
    }

   
    public void MultiSelect()
    {   //starts with getting start position of the mouse
        if (Input.GetMouseButtonDown(0))
        {
            startPos = Input.mousePosition;
        }
        //updates selection box while left click is held down
        if (Input.GetMouseButton(0))
        {
            UpdateSelectionBox(Input.mousePosition);
        }
        //when left click is released it disables the selection box, gets the screen position for all the selectables
        //and checks to see if they're within the bounds. if so it selects them.
        if (Input.GetMouseButtonUp(0))
        {
            selectionBox.gameObject.SetActive(false);
            Vector2 min = selectionBox.anchoredPosition - (selectionBox.sizeDelta / 2);
            Vector2 max = selectionBox.anchoredPosition + (selectionBox.sizeDelta / 2);

            foreach (GameObject go in UnitManager.UM.selectables)
            {
                Vector3 screenPose = Camera.main.WorldToScreenPoint(go.transform.position);

                if(screenPose.x >= min.x && screenPose.x <= max.x && screenPose.y >= min.y && screenPose.y <= max.y)
                {
                    if (!UnitManager.UM.selectedStructures.Contains(go))
                    {
                        UnitManager.UM.selectedStructures.Add(go);
                        go.GetComponent<CelestialBody>().Select();
                    }
                }
            } 
        }
    }

    public void LeftClick()
    {
        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            Physics.Raycast(ray, out hit);
            /// hold shift to add to selection
            if (hit.collider.GetComponent<ISelectable>() != null && Input.GetKey(KeyCode.LeftShift))
            {
                if (!UnitManager.UM.selectedStructures.Contains(hit.collider.gameObject))
                {
                    UnitManager.UM.selectedStructures.Add(hit.collider.gameObject);
                    hit.collider.GetComponent<ISelectable>().Select();
                }
            }
            else /// hold control to remove from selection
            if (hit.collider.GetComponent<ISelectable>() != null && Input.GetKey(KeyCode.LeftControl))
            {   // checks to see if the gameobject is in the list
                if (UnitManager.UM.selectedStructures.Contains(hit.collider.gameObject))
                {
                    UnitManager.UM.selectedStructures.Remove(hit.collider.gameObject);
                    hit.collider.GetComponent<ISelectable>().DeSelect();
                }
            }
            else if (hit.collider.GetComponent<ISelectable>() != null && UnitManager.UM.selectedStructures.Count <= 1)
            {       // checks to see if gameobject is not in the list
                    if (!UnitManager.UM.selectedStructures.Contains(hit.collider.gameObject))
                    {   // checks to see if there is something in the selection in order to deselect it 
                        // before selecting the click on gameobject
                        if(UnitManager.UM.selectedStructures.Count > 0)
                            {
                        UnitManager.UM.selectedStructures[0].GetComponent<CelestialBody>().DeSelect();
                        UnitManager.UM.selectedStructures.Clear();
                            }
                    UnitManager.UM.selectedStructures.Add(hit.collider.gameObject);
                        hit.collider.GetComponent<ISelectable>().Select();
                    }
            } 
            else
            {   
                // Cycles through selection to deselect and then clears the list when you dont left click on a selectable
                foreach (GameObject go in UnitManager.UM.selectedStructures)
                {
                    go.GetComponent<ISelectable>().DeSelect();
                }
                UnitManager.UM.selectedStructures.Clear();
            }
        }
    }
    // for the selection box. updating the size
    void UpdateSelectionBox ( Vector2 currentMousepos)
    {
        if (!selectionBox.gameObject.activeInHierarchy)
        {
            selectionBox.gameObject.SetActive(true);
        }

        float width = currentMousepos.x - startPos.x;
        float height = currentMousepos.y - startPos.y;

        selectionBox.sizeDelta = new Vector2(Mathf.Abs(width),Mathf.Abs( height));
        selectionBox.anchoredPosition = startPos + new Vector2(width / 2, height / 2);
    }



}
