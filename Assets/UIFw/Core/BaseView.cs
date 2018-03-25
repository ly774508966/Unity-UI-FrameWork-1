using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseView : MonoBehaviour{
    // view life cycle
    public virtual void OnViewEnter()
    {
    }
    public virtual void OnViewPause()
    {
    }
    public virtual void OnViewResume()
    {
    }
    public virtual void OnViewExit()
    {
    }
}
