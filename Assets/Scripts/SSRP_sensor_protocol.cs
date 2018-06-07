using System;
using System.Collections;

public class SSRP_participant
{

    public string Email;
    public string Password;

    public SSRP_participant()
    {
        Email = "";
        Password = "";
    }

    public SSRP_participant(string _email,string _pass)
    {
        Email = _email;
        Password = _pass;
    }

    public SSRP_participant(Boolean test)
    {
        Email = "";  Password = "";
        if (test)
        {
            Email = "user02ssr@ssr.se";
            Password = "Password";
        }
           
    }

}

public class SSRP_client
{
    public string Clientid;
    public string Clientpassword;

    public SSRP_client()
    {
        Clientid = "";
        Clientpassword = "";
    }

    public SSRP_client(string _id, string _pw)
    {
        Clientid = _id;
        Clientpassword = _pw;
    }

    public SSRP_client(Boolean test)
    {
        Clientid = ""; Clientpassword = "";
        if (test)
        {
            Clientid = "1e11fb3249344c4a9e0adbb190bfa2e7";
            Clientpassword = "f70e87088f334df4af185bf3ff6f4e98";
        }
        
    }

   
}

public class SSRP_entity
{
    public string Entity_id;
    public string Sensorid;
    public string SensorPassword;

    public SSRP_entity()
    {
        Entity_id = "";
        Sensorid = "";
        SensorPassword = "";
    }

    public SSRP_entity(string ent_id, string sen_id, string  sen_pw)
    {
        Entity_id = ent_id;
        Sensorid = sen_id;
        SensorPassword = sen_pw;
    }

    public SSRP_entity(Boolean test)
    {
        Entity_id = ""; Sensorid = "";  SensorPassword = "";
        if (test)
        {
            Entity_id = "Room62";
            Sensorid = "iot_sensor_2bd37a1cc7784f93b998ea6feff42915";
            SensorPassword = "789a173df6944eb5829130b3ce447bce";
        }  
    }
}


/* loraSensors_unordered_List
  { 
  contextResponses": [ ] 
  }
*/
public class SSRP_response_raw
{
    public Type ssrp_raw_response;
    public SSRP_contextResponse[] contextResponses;
}

/* an actual sensor
{ 
 "contextElement": { },
 "statusCode": { }
}
*/
public class SSRP_contextResponse
{
    public Type ssrp_contextResponse;
    public SSRP_StatusCode statusCode;
    public SSRP_ContextElement contextElement;

    public SSRP_StatusCode getStatus()
    {
        return statusCode;
    }

    public SSRP_ContextElement data_basic()
    {
        SSRP_ContextElement basicInfo = new SSRP_ContextElement();
        basicInfo.id = contextElement.id;
        basicInfo.type = contextElement.type;
        basicInfo.isPattern = contextElement.isPattern;
        return basicInfo;
    }

    public string description()
    {
        string ret = "ContextElement : {";
        ret += "id: " + contextElement.id + ", ";
        ret += "type: " + contextElement.type + ", ";
        ret += "isPattern: " + contextElement.isPattern + ", ";
        ret += contextElement.attributes.Length + "attribute(s) }";
        return ret;
    }

    public SSRP_attribute[] attributes()
    {
        return contextElement.attributes;
    }

    public string list_attributes()
    {
        string ret = "";
        foreach (SSRP_attribute att in contextElement.attributes)
        {
            ret += "(" + att.type + ") " + att.name + " = " + att.value;
            try {
                if (att.metadatas.Length > 0 )
                {   
                    ret += " [";
                    foreach (SSRP_Metadata met in att.metadatas)
                    {
                        ret += met.name + ", ";
                        ret += met.type +", ";
                        ret += met.value +", "; 

                    }
                    ret += " ]";
                }
            } 
            catch
            {

            }
            ret += "\n";
        }
        return ret;
    }

}

/* sensor.status
statusCode": {
   "code": "200",
   "reasonPhrase": "OK"
}
*/
public class SSRP_StatusCode
{
    public Type ssrp_statusCode;
    public String code;
    public String reasonPhrase;
}

/*
 *{"entities": [

{"id": "0004a30b0022a677",

"type": "LORA_Sensor",

"isPattern": false}]

}
*/
public class SSRP_sensor_request
{

    public SSRP_ContextElement[] entities;

    private SSRP_ContextElement sensorObj;

    public SSRP_sensor_request()
    {
        sensorObj = new SSRP_ContextElement();
        entities = new SSRP_ContextElement[1] { sensorObj };

    }

    public SSRP_sensor_request(Boolean test)
    {
        if (test)
        {
            sensorObj = new SSRP_ContextElement("0004a30b0022a677", "LORA_Sensor", "false");
        }
        else
        {
            sensorObj = new SSRP_ContextElement();
        }
        entities = new SSRP_ContextElement[1] { sensorObj };
    }

    public SSRP_sensor_request(String _id, String _type, String _isPattern)
    {
        sensorObj = new SSRP_ContextElement(_id, _type, _isPattern);
        entities = new SSRP_ContextElement[1] { sensorObj };
    }

}

/* sensor.info
contextElement:{ 
    "type": "LORA_Sensor",
    "isPattern": "false",
    "id": "0004a30b0022a677",
    "attributes": []
    }
 */

public class SSRP_ContextElement
{

    public Type ssrp_contextElement;
    public String id;
    public String type;
    public String isPattern;
    public SSRP_attribute[] attributes;

    public SSRP_ContextElement()
    {
        id = ""; type = ""; isPattern = "false";
        attributes = new SSRP_attribute[0];
    }

    public SSRP_ContextElement(String _id, String _type, String _isPattern)
    {
        id = _id;
        type = _type;
        isPattern = _isPattern;
        attributes = new SSRP_attribute[0];
        
    }

    public SSRP_ContextElement(String _id, String _type, String _isPattern, SSRP_attribute[] _att)
    {
        id = _id;
        type = _type;
        isPattern = _isPattern;
        attributes = _att;

    }
}




/* sensor.attributes
 "name": "PM25",
 "type": "float",
 "value": "1.484232", 
 "metadatas": []
 */
public class SSRP_attribute : SSRP_Metadata
{
    public Type ssrp_attribute;
    public SSRP_Metadata[] metadatas;

    public SSRP_attribute()
    {
        name = "";
        type = "";
        value = "";
        metadatas = new SSRP_Metadata[0]; 
    }

    public SSRP_attribute(SSRP_Metadata[] list) 
    {
        int i = 0;
        int m = list.Length;
        if (m > 1) { metadatas = new SSRP_Metadata[m - 1]; }
        for (i=0; i < m; i++)
        { 
            if (i == 0)
            {
                name = list[i].name;
                type = list[i].type;
                value = list[i].value;
            }
            else
            {

            }
        }
    }
}
   

/*
metadata : 
    "name": "unit",
    "type": "string",
    "value": "ug/m3"
*/
public class SSRP_Metadata
{
    public Type ssrp_Metadata;
    public String name;
    public String type;
    public String value;

    public  SSRP_Metadata()
    {
        name = "";
        type = "";
        value = "";
    }

    public  SSRP_Metadata(String _name, String _type, String _value)
    {
        name = _name;
        type = _type;
        value = _value;

    }

    public void import(String _name, String _type, String _value)
    {
        name = _name;
        type = _type;
        value = _value;

    }
}