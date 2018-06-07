using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//NK vuforia key
//Qmqzd0VhfkVBCxZ1b1U0kQw4lZ6t6x0JZmGl5vg1waRIsoRohzXgdi6vgN2AM4MwDn9dhL1dkF3iYoPXzPIkwGSQJOjz/IhPgLCaqn3bqVZ/ratqZXQkNoIJLdr9Ocykhwj9/8QIGAXlOUCFbEDJ8ixMQXw1cP3DVAtMp2C1NxjD3A4h1FpQZwDvem7OLfS1OQmqzd0VhfkVBCxZ1b1U0kQw4lZ6t6x0JZmGl5vg1waRIsoRohzXgdi6vgN2AM4MwDn9dhL1dkF3iYoPXzPIkwGSQJOjz/IhPgLCaqn3bqVZ/ratqZXQkNoIJLdr9Ocykhwj9/8QIGAXlOUCFbEDJ8ixMQXw1cP3DVAtMp2C1NxjD3A4h1FpQZwDvem7

//ltu vuforia key u:sensesmartregion@outlook.com, p:Ltu1234!
//AWbv2Br/////AAADmfP5GXalbk/Un7Konr4YTlYJuhC11t5wmTGsILPJ5GP9vVPER2KxzmBAq7Pa+sNgfbsby96rYWi2U7Yue8d4SLdjmxtiB+P19oapvxndRHHnVcTyn1Z6/PVc9apPLs6AdRFy7PR/orE4oEoilC2wKe+lO7tpyL49zP5rSW5sQtCUYINMZdQOkm5kNqPIrXtsG3e7LO7b8Yt8e0BIK4okkZnjPV+ioRlrSpu/9MXeB6Vds8A8bDmV99ISBpRolr/cpskPJikHaCeV5NE5GGfdLOA1CFG9FX0/9Iv6YddqoC0wmG07LJGrz49e/17j5vHcNfR56/7UkhMZ4Dqf1XUTzImeCVIezGaj1JXdmq7fmR6m

public class DeviceManager : MonoBehaviour {

    
    public GameObject servicePrefab;
    public Text ui_id;
    public Text ui_type;
    public Text ui_path;
    public Text ui_creDate;
    public Text ui_modDate;
    public Text ui_loc;
    public SensorData _sensorData;
    public GameObject contentPanel;


    public void importData()
    {
        Debug.Log("importData EmptySensorCreation");

        //emptyDataCreation();
        _sensorData = new SensorData();


    }

    

    public void importData(SensorData sensorData )
    {
        _sensorData = new SensorData();
        Debug.Log(sensorData);
        try
        {
            _sensorData._id = sensorData._id;
            _sensorData.attrNames = sensorData.attrNames;
            _sensorData.attrs = sensorData.attrs;
            _sensorData.credate = sensorData.credate;
            _sensorData.modDate = sensorData.modDate;
            _sensorData.location = sensorData.location;
        }
        catch
        {
            Debug.Log("Device importData failed");
        }
        populateServices();

    }

   
    private void populateServices()
    {
        Debug.Log("PopulateServices");
        if (
        _sensorData.attrNames.Length == _sensorData.attrs.Length &&
        _sensorData.attrNames.Length != 0)
        {
            // fill in the sensors
            int i = 0;
            int m = _sensorData.attrNames.Length;
            
            for (i = 0; i < m; i++)
            {
                float num = Random.Range(-10.0f, 10.0f);
                
                _sensorData.attrs[i].name = _sensorData.attrNames[i];
                _sensorData.attrs[i].cursor = i;
                
                //_sensorData.attrs[i] = s;
            }

            addServicePanels();
        }
        
        Debug.Log("PopulateServices Done");
    }


    private void addServicePanels( )
    {
        int spacing = 110;
        int startOffset = 10;
        Transform ct = contentPanel.transform;
        _sensorData._id.servicePath = "AddPanels ";

        if (_sensorData != null && _sensorData.attrs.Length != 0)
        {
            int i = 0;
            int m = _sensorData.attrs.Length;
            
            for (i=0;i<m;i++)
            {
                Service  serv  = _sensorData.attrs[i];
                float x = startOffset + spacing * i;
                _sensorData._id.servicePath += i.ToString() + ",";
                Vector3 pos = new Vector3(x, -33f, 0);
                GameObject obj;
                obj = (GameObject) Instantiate(servicePrefab, pos, Quaternion.identity);
                obj.transform.SetParent(contentPanel.transform,false);

               
#if WINDOWS_UWP

                //obj.transform.localScale = new Vector3(1,-1,1);   
#endif
               
                ServiceManager sm = obj.GetComponent<ServiceManager>();
                sm.importData(serv);



            }
        }
    }
    // Use this for initialization
    void Start()
    {
        // importData();
        

    }

    private void Update()
    {
        if (_sensorData != null)
        {
            if (ui_id != null) { ui_id.text = "id :" + _sensorData._id.id; }
            if (ui_type != null) { ui_type.text = _sensorData._id.type + ":Type"; }
            if (ui_path != null) { ui_path.text = "Service Path : \"" + _sensorData._id.servicePath + "\""; }
            if (ui_creDate != null) { ui_creDate.text = "CreaDate : " + _sensorData.credate.ToString(); }
            if (ui_modDate != null) { ui_modDate.text = _sensorData.modDate.ToString() + ": ModData"; }
            //if (ui_loc != null) { ui_loc.text = "loc : " + _sensorData.location.coords.coordinates[0].ToString() + ", " + _sensorData.location.coords.coordinates[1].ToString(); }
        }
        
        
        
         
    }
}
