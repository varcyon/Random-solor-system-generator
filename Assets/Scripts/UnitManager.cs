using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
    // selection control
    public static UnitManager UM;
    public List<GameObject> selectedStructures = new List<GameObject>();
    [SerializeField] public  List<GameObject> selectables = new List<GameObject>();
    [SerializeField] private List<GameObject> selectablesDisplay = new List<GameObject>();

    private void Start()
    {
        if(UM == null)
        {
            UM = this;
        } else if ( UM != this)
        {
            Destroy(this);
        }

    }
    private void Update()
    {
        selectablesDisplay = selectables;

    }
}
