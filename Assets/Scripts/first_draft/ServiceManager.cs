using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ServiceManager : MonoBehaviour {

    public Service _service;
    public Text ui_name;
    public Text ui_value;

    // Use this for initialization
    void Start()
    {
        
        //importData();

    }

    public void importData()
    {
        _service = new Service();
        Debug.Log("EmptyServiceCreation");

        _service.name = "Humidity";
        _service.cursor = 0;
        _service.value = "42";
        _service.mdNames = new int[0];
        _service.creDate = "1522055731";
        _service.modDate = "1522055731";
    }

    public void importData(Service service)
    {
        Debug.Log("importData ServiceCreation");
        _service = new Service();
        _service = service;
        
    }

    // Update is called once per frame
    void Update () {
		if (ui_name != null )
        {
            if (_service != null && _service.name != null)
            {
                ui_name.text = _service.name;
            }
        }
        if (ui_value != null )
        {
            if (_service != null && _service.value != null)
            {
                ui_value.text = _service.value;
            }
        }
        
	}
}
