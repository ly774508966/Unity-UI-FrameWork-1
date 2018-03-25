using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewRoot : MonoBehaviour {

    private void Awake()
    {
        ViewManager.Instance.PushView(ViewType.ViewTypes.MainView);
    }

}
