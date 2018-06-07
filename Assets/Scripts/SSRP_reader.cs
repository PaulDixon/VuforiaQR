using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using FullSerializer;

public class SSRP_reader : MonoBehaviour {
    
    private static readonly fsSerializer _serializer = new fsSerializer();

    // SSRP client / server connection.
    public SSRP_sensor_request  local_request   = new SSRP_sensor_request("0004a30b0022a677", "LORA_Sensor", "false") ;
    public SSRP_participant     participant_02  = new SSRP_participant("user02ssr@ssr.se", "Password");
    public SSRP_client          local_client    = new SSRP_client("1e11fb3249344c4a9e0adbb190bfa2e7", "f70e87088f334df4af185bf3ff6f4e98");
    public SSRP_entity          ssrp_room_entity= new SSRP_entity("Room62", "iot_sensor_2bd37a1cc7784f93b998ea6feff42915", "789a173df6944eb5829130b3ce447bce");

    // SSRP localFileBackup
    public string gameDataProjectFilePath = "/Resources/ssrp_response.txt";
    public string ssrp_response_string = "";

    // Use this for initialization
    void Start () {
       
        LoadSensorData();

      

    }

    private void LoadSensorData()
    {
        string filePath = Application.dataPath + gameDataProjectFilePath;
       

        string dataAsJson = File.ReadAllText(filePath);
        if (File.Exists(filePath))
        {


            string ssrp_response_string = File.ReadAllText(filePath);

            
           

            SSRP_response_raw response_raw = (SSRP_response_raw)Deserialize(typeof(SSRP_response_raw), ssrp_response_string);
           
            SSRP_contextResponse[]  loraSensors_unordered_List = response_raw.contextResponses;
            SSRP_contextResponse    loraSensor_0            = loraSensors_unordered_List[0];
           

            Debug.Log(loraSensor_0.description());
            Debug.Log(loraSensor_0.list_attributes());

            Debug.Log("Hello VS17 breakpointDebugger");

        }
    }

    public static string Serialize(Type type, object value)
    {
        // serialize the data
        fsData data;
        _serializer.TrySerialize(type, value, out data).AssertSuccessWithoutWarnings();

        // emit the data via JSON
        return fsJsonPrinter.CompressedJson(data);
    }

    public static object Deserialize(Type type, string serializedState)
    {
        // step 1: parse the JSON data
        fsData data = fsJsonParser.Parse(serializedState);

        // step 2: deserialize the data
        object deserialized = null;
        _serializer.TryDeserialize(data, type, ref deserialized).AssertSuccessWithoutWarnings();

        return deserialized;
    }

    // Update is called once per frame
    void Update () {
		
	}
}
