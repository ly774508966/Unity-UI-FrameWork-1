using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewType {

    public enum ViewTypes
    {
        MainView,        
        To
    }
    public static Dictionary<ViewTypes, string> viewPath = new Dictionary<ViewTypes, string>()
    {
        { ViewTypes.MainView, "ViewPrefabs/MainView"},
    };

}
