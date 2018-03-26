using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewRoot : MonoBehaviour {

    private void Awake()
    {
        BaseViewManager.Instance.PushView(ViewType.ViewTypes.MainView);
    }

}
