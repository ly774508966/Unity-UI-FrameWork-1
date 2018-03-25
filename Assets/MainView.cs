using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainView : BaseView{

    public override void OnViewEnter()
    {
        base.OnViewEnter();
        Debug.Log("on view enter");
    }
}
