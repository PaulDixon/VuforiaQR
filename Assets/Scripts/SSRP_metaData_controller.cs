using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SSRP_metaData_controller : MonoBehaviour {


    public Text ui_name;
    public Text ui_type;
    public Text ui_value;

    private SSRP_Metadata _data = new SSRP_Metadata();

    // Use this for initialization
    void Start()
    {

        importData(new SSRP_Metadata("foo", "ooh", "bar"));
    }

    void importData(SSRP_Metadata meta)
    {
        _data = meta;
        refresh();
    }
    // Update is called once per frame
    void refresh()
    {
        if (_data != null)
        {
            if (ui_name != null && ui_type != null && ui_value != null)
            {
                ui_name.text = _data.name;
                ui_value.text = _data.value;
                ui_type.text = _data.type;
            }
            else
            {
                ui_name.text = "UI_404";

            }


        }
    }

    void fetchGameObjectsFromParent()
    {
        ui_name = gameObject.GetComponent("name") as Text;
        ui_value = gameObject.GetComponent("value") as Text;
        ui_type = gameObject.GetComponent("type") as Text;


    }
}
