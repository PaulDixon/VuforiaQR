using System;

//[System.Serializable]


public class IdData
{
    public Type idData_type;
    public string id;//	"env_virt_5_humid"
    public string type; //	"test"
    public string servicePath; //	"/"


}
[Serializable]
public class Coords
{
    public Type coord_type;
    public string type;
    public float[] coordinates;


}
[Serializable]
public class Location {
    public Type location_type;
    public string attrName;
    public Coords coords;
}


[Serializable]
public class Service
{
    public Type service_type;
    public int cursor;
    public string name;
    public string value;
    public string type;
    public int[] mdNames;
    public string creDate;
    public string modDate;


}

public class SensorData  {

    public Type sensorData_type;
    public IdData _id;
    public string[] attrNames;
    public Service[] attrs;
    public int credate;
    public int modDate;
    public Location location;
    


}