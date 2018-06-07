using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON; // remove this, stick to Unity's own stuff.
using UnityEngine.UI;
using System.IO;
using FullSerializer;
using System;

public class HeatBeat : MonoBehaviour
{

    private static readonly fsSerializer _serializer = new fsSerializer();

    public int bpm = 30;
   // public BatchedSensorsFromFile batchedSensorsFromFile; 
    public SensorData[] sensorList_unordered;
    public SensorData[] sensorList;
    public GameObject testHudService;
    public GameObject testMarkerSensor;
    public GameObject servicePanel;
    public GameObject devicePanel;
    public bool isActive = true;

    public Vector2 userPosition;
    public Text userFieldBack_txt;
    public string debug_str;

    //private string gameDataProjectFilePath = "/StreamingAssets/data.json";
    private string gameDataProjectFilePath = "/Resources/sensors.txt";
    /*
     * readAdress
"create" devices.json
"group" devices.json Same-Adress 
LOD catergorisation
Direction to local GPS co'ord
spawn Sensor for Marker
spawn Sensor for Hud Panel
*/
    


void Start()
    {
        sensorList_unordered = new SensorData[0];
        createFakeSerializedJsonString();
        updateSensors();
    }

    

    void heartbeat()
    {
        //updateUserPosition();
        //updateSensors();
        showTestData();
    }

    void updateUserPosition()
    {
        // fetch FROM DEVICE
        userPosition = new Vector2(5.0F, 5.0F);
    }

    

    void updateSensors()
    {
        Debug.Log("updateSensors");
        LoadSensorData();
        
        filterSensorATTR();
        filterOnDistance();
        showTestData();
    }

    private void showTestData()
    {
        Debug.Log("Showing Dummy Data");
        debug_str = "Showing Dummy Data: ";
        SensorData sensor_0 = new SensorData();
        SensorData sensor_1 = new SensorData();
        Service hud_serviceData_01 = new Service();


        if (sensorList_unordered.Length >= 1 )
        {
            sensor_0 = sensorList_unordered[0];
            //hud_serviceData_01 
            //sensor_1 = sensorList_unordered[1]; 
        }
        else
        {
            debug_str += "; JSON parser Failure";
        }// */

        /*
        if (testHudService != null)
        {

            ServiceManager sm = testHudService.GetComponent<ServiceManager>();
            // sm.importData(s1);
            debug_str += "  HUD ";
            sm.ui_name.text = "hud";
        }
        */
        if (testMarkerSensor != null)
        {
            DeviceManager dm = testMarkerSensor.GetComponent<DeviceManager>();
            dm.importData(sensor_0);
            debug_str += "  Marker ";
        }

        debug_str +=  ", compiled: " + System.DateTime.Today.ToString();

    }
    private void LoadSensorData()
    {
        Debug.Log("LoadSensorData");
        string filePath = Application.dataPath + gameDataProjectFilePath;

        if (File.Exists(filePath))
        {
            debug_str = "SensorData Loading";
            Debug.Log("SensorData Loading");
            string dataAsJson = File.ReadAllText(filePath);

            //string dataAsJson = createFakeSerializedJsonString();
            Debug.Log(dataAsJson);
            // BulkSensorList batchedSensorsFromFile = JsonUtility.FromJson<BulkSensorList>(dataAsJson);

            SensorData[] SensorsFile = (SensorData[]) Deserialize(typeof(SensorData[]), dataAsJson);
            sensorList_unordered = SensorsFile;

           // sensorList_unordered = batchedSensorsFromFile;

            //Debug.Log(" batchedSensorsFromFile.theBatch =" + batchedSensorsFromFile.theBatch.ToString());
            Debug.Log(sensorList_unordered);
            debug_str = "SensorData loaded";
            Debug.Log("SensorData loaded");
        }
        else
        {
           // batchedSensorsFromFile = new BatchedSensorsFromFile();
            debug_str = "error in loading SensorData";
            Debug.Log("SensorData error");
        }
    }

   


    
    // some sensor data may be limited to just 
    void filterSensorATTR()
    {
        sensorList = sensorList_unordered;
    }

    // some sensor data may be limited to just 
    void filterOnDistance()
    {

    
        // read sensor position
        // compare to user position. 
        // add distance from user
        // return that object.

        Vector2 p = new Vector2(0, 0);
        float dist = 0.0f;
        dist = Vector2.Distance(userPosition, p);
        //update JSONObject with distance;
        sensorList = sensorList_unordered;
    }
    // Update View with Modeled information.
    private void FixedUpdate()
    {
        userFieldBack_txt.text = debug_str;
    }

    
    JSONObject readFiWareAdress()
    {
        /*
        { "_id":{ "id":"env_virt_5_humid","type":"test","servicePath":"/"},"attrNames":["humidity","position","owner","environment","visibility"],"attrs":{"humidity":{"value":114,"type":"Number","mdNames":[],"creDate":1522055622,"modDate":1522227222},"position":{"type":"geo:point","creDate":1522055622,"modDate":1522055622,"value":"18.677896423339842,64.59036829080296","mdNames":[]},"owner":{"type":"String","creDate":1522055622,"modDate":1522055622,"value":"testing","mdNames":[]},"environment":{"type":"String","creDate":1522055622,"modDate":1522055622,"value":"Indoor","mdNames":[]},"visibility":{"type":"String","creDate":1522055622,"modDate":1522055622,"value":"Public","mdNames":[]}},"creDate":1522055622,"modDate":1522227222,"location":{"attrName":"position","coords":{"type":"Point","coordinates":[64.59036829080296,18.677896423339842]}}};
        {"_id":{"id":"env_virt_4_humid","type":"test","servicePath":"/"},"attrNames":["humidity","position","owner","environment","visibility"],"attrs":{"humidity":{"value":75,"type":"Number","mdNames":[],"creDate":1522055616,"modDate":1522227214},"position":{"type":"geo:point","creDate":1522055616,"modDate":1522055616,"value":"18.664850158691404,64.59655483122222","mdNames":[]},"owner":{"type":"String","creDate":1522055616,"modDate":1522055616,"value":"testing","mdNames":[]},"environment":{"type":"String","creDate":1522055616,"modDate":1522055616,"value":"Indoor","mdNames":[]},"visibility":{"type":"String","creDate":1522055616,"modDate":1522055616,"value":"Public","mdNames":[]}},"creDate":1522055616,"modDate":1522227214,"location":{"attrName":"position","coords":{"type":"Point","coordinates":[64.59655483122222,18.664850158691404]}}};
        {"_id":{"id":"env_virt_3_temp","type":"test","servicePath":"/"},"attrNames":["temperature","position","owner","environment","visibility"],"attrs":{"temperature":{"value":6,"type":"Number","mdNames":[],"creDate":1522055519,"modDate":1522227126},"position":{"type":"geo:point","creDate":1522055519,"modDate":1522055519,"value":"18.677896423339842,64.59905850729791","mdNames":[]},"owner":{"type":"String","creDate":1522055519,"modDate":1522055519,"value":"testing","mdNames":[]},"environment":{"type":"String","creDate":1522055519,"modDate":1522055519,"value":"Indoor","mdNames":[]},"visibility":{"type":"String","creDate":1522055519,"modDate":1522055519,"value":"Public","mdNames":[]}},"creDate":1522055519,"modDate":1522227126,"location":{"attrName":"position","coords":{"type":"Point","coordinates":[64.59905850729791,18.677896423339842]}}};
        {"_id":{"id":"env_virt_6_multi","type":"test","servicePath":"/"},"attrNames":["humidity","temperature","position","owner","environment","visibility"],"attrs":{"humidity":{"value":88,"type":"Number","mdNames":[],"creDate":1522055723,"modDate":1522227310},"temperature":{"value":-10,"type":"Number","mdNames":[],"creDate":1522055723,"modDate":1522227310},"position":{"type":"geo:point","creDate":1522055723,"modDate":1522055723,"value":"18.677123947143553,64.59938985892686","mdNames":[]},"owner":{"type":"String","creDate":1522055723,"modDate":1522055723,"value":"testing","mdNames":[]},"environment":{"type":"String","creDate":1522055723,"modDate":1522055723,"value":"Indoor","mdNames":[]},"visibility":{"type":"String","creDate":1522055723,"modDate":1522055723,"value":"Public","mdNames":[]}},"creDate":1522055723,"modDate":1522227310,"location":{"attrName":"position","coords":{"type":"Point","coordinates":[64.59938985892686,18.677123947143553]}}};
        {"_id":{"id":"env_virt_7_multi","type":"test","servicePath":"/"},"attrNames":["humidity","temperature","position","owner","environment","visibility"],"attrs":{"humidity":{"value":42,"type":"Number","mdNames":[],"creDate":1522055731,"modDate":1522227318},"temperature":{"value":14,"type":"Number","mdNames":[],"creDate":1522055731,"modDate":1522227318},"position":{"type":"geo:point","creDate":1522055731,"modDate":1522055731,"value":"18.651374740600584,64.58686932696415","mdNames":[]},"owner":{"type":"String","creDate":1522055731,"modDate":1522055731,"value":"testing","mdNames":[]},"environment":{"type":"String","creDate":1522055731,"modDate":1522055731,"value":"Indoor","mdNames":[]},"visibility":{"type":"String","creDate":1522055731,"modDate":1522055731,"value":"Public","mdNames":[]}},"creDate":1522055731,"modDate":1522227318,"location":{"attrName":"position","coords":{"type":"Point","coordinates":[64.58686932696415,18.651374740600584]}}}
         // */
        /*
        using (WWW www = new WWW(WeatherURL))
        {

            yield return www;
            debug_str += "\n responseHeaders:" + www.responseHeaders;
            debug_str += "\n URL:" + www.url;
            debug_str += "\n toString :" + www.ToString();

            jsonString = www.text;
            var jObj = JSON.Parse(jsonString);
            debug_str += "\n*****************************\n" + jsonString + "\n *****************************\n";


            try
            {
                cur_temp = jObj["main"]["temp"].AsFloat - kelvin;
                min_temp = jObj["main"]["temp_min"].AsFloat - kelvin;
                max_temp = jObj["main"]["temp_max"].AsFloat - kelvin;

                humidity = jObj["main"]["humidity"].AsFloat;
                pressure = jObj["main"]["pressure"].AsFloat;

                min_temp -= 5 * Random.value;
                max_temp += 2 * Random.value;
                renderData();
            }
            catch
            {
                debug_str += "json parse ballsed up";
            }
            
        }
        */


        JSONObject bufferObject = new JSONObject();
        return bufferObject;
    }

    private string createFakeSerializedJsonString()
    {
        Debug.Log("emptyDataCreation");


        Service humidity = new Service();
        humidity.type = "Number";
        humidity.value = "114";
        humidity.mdNames = new int[0];
        humidity.creDate = "1522055622";
        humidity.modDate = "1522227222";

        Service position = new Service();
        position.type = "position";
        position.value = "18.677896423339842, 64.59036829080296";
        position.mdNames = new int[0];
        position.creDate = "1522055731";
        position.modDate = "1522055731";

        Service owner = new Service();
        owner.type = "String";
        owner.value = "Paul";
        owner.mdNames = new int[0];
        owner.creDate = "1522055731";
        owner.modDate = "1522055731";

        Service environment = new Service();
        environment.type = "String";
        environment.value = "Indoor";
        environment.mdNames = new int[0];
        environment.creDate = "1522055731";
        environment.modDate = "1522055731";

        Service visibility = new Service();
        visibility.type = "String";
        visibility.value = "Public";
        visibility.mdNames = new int[0];
        visibility.creDate = "1522055731";
        visibility.modDate = "1522055731";

        SensorData _sensorData = new SensorData();
        IdData idData = new IdData();
        idData.id = "env_virt_5_humid";
        idData.type = "test";
        idData.servicePath = "/";
        _sensorData._id = idData;
        _sensorData.attrNames = new string[] { "humidity", "position", "owner", "environment", "visibility" };
        _sensorData.attrs = new Service[] { humidity, position, owner, environment, visibility };
        _sensorData.credate = 1522055622;
        _sensorData.modDate = 1522227222;
        _sensorData.location = new Location();
        _sensorData.location.attrName = "Position";
        _sensorData.location.coords = new Coords();
        _sensorData.location.coords.coordinates = new float[] { 64.58686932696415f, 18.651374740600584f };


        //Unities standard json solution, is built for small data files and can not cope with lists of objects.
        //https://github.com/SaladLab/Json.Net.Unity3D is a popular solution and even has a link on the asset store
        // got advice to go with https://github.com/jacobdufault/fullserializer

        //http://json2csharp.com/

        SensorData[] batchList = new SensorData[] { _sensorData };
        //string jsonSerializedString = JsonUtility.ToJson(batchList);
        //populateServices();



       //  



        string jsonSerializedString = Serialize(batchList.GetType(), batchList); ;

        Debug.Log("jsonSerializedString = " + jsonSerializedString);
        return jsonSerializedString;

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
    /*
     *  private static readonly fsSerializer _serializer = new fsSerializer();

        public static string Serialize(Type type, object value) {
            // serialize the data
            fsData data;
            _serializer.TrySerialize(type, value, out data).AssertSuccessWithoutWarnings();

            // emit the data via JSON
            return fsJsonPrinter.CompressedJson(data);
        }

        public static object Deserialize(Type type, string serializedState) {
            // step 1: parse the JSON data
            fsData data = fsJsonParser.Parse(serializedState);

            // step 2: deserialize the data
            object deserialized = null;
            _serializer.TryDeserialize(data, type, ref deserialized).AssertSuccessWithoutWarnings();

            return deserialized;
        }
        */

}

