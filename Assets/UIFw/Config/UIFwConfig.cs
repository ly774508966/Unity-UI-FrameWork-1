using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIFwConfig{

    public static Dictionary<string, object> Config = new Dictionary<string, object>
    {
        { "ViewManagerModel", ViewManagerModel.SimpleStack },
    };

    public enum ViewManagerModel
    {
        SimpleStack = 0,
        LinearStack = 1
    }

}