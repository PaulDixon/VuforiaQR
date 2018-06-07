using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hololensFlip : MonoBehaviour {

    Transform anchorPoint;
	// Use this for initialization
	void Start () {


        //flip the Y axis for the hololens to render "the right way"
#if WINDOWS_UWP
      anchorPoint = this.gameObject.transform.GetChild(0);
        anchorPoint.localScale = new Vector3(0.1f, -0.1f, 0.1f);   
#endif

    }

    // Update is called once per frame
    void Update () {
		
	}
}
