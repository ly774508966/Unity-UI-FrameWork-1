using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class BaseViewManager {

    // 创建一个BaseVeiwManager实例，根据ViewManagerModel，通过反射创建相应类
    private static BaseViewManager GetInstance()
    {
        object model;
        UIFwConfig.Config.TryGetValue("ViewManagerModel", out model);
        if (model == null)
            throw new System.Exception(string.Format("UIFwConfig's ViewManagerModel: {0} is null", model));

        string className = model.ToString();
        object obj = Assembly.GetExecutingAssembly().CreateInstance(string.Format("ViewManagerModel.{0}", className));
        if(obj == null)
            throw new System.Exception(string.Format("class {0} is not find", className));
        BaseViewManager bvmObj;
        try
        {
            bvmObj = (BaseViewManager)obj;
        }
        catch (Exception e)
        {
            throw new System.Exception(string.Format("class {0} haven't extend BaseViewManager", className));
        }

        return bvmObj;
    }

    // 创建ViewManager实例
    private static BaseViewManager mInstance;
    public static BaseViewManager Instance
    {
        get
        {
            if (mInstance == null)
            {
                mInstance = GetInstance();
            }
            return mInstance;
        }
    }

    // 保存ViewType对应的View实例
    protected Dictionary<ViewType.ViewTypes, BaseView> mViewGoDict;

    // 获取MainCanvas
    protected Transform mCanvasTransform;
    protected Transform GetCanvas
    {
        get
        {
            if(this.mCanvasTransform == null)
            {
                this.mCanvasTransform = GameObject.FindGameObjectWithTag("MainCanvas").transform;
            }
            return this.mCanvasTransform;
        }
    }

    // 获取具体的View对象
    protected BaseView GetView(ViewType.ViewTypes viewType)
    {
        if(this.mViewGoDict == null)
        {
            this.mViewGoDict = new Dictionary<ViewType.ViewTypes, BaseView>();
        }
        BaseView baseView = null;
        if (!this.mViewGoDict.ContainsKey(viewType))
        {
            string viewPath;
            ViewType.viewPath.TryGetValue(viewType, out viewPath);
            if(viewPath == null)
                throw new System.Exception(string.Format("ViewType.ViewTypes haven't {0}", viewType));

            GameObject baseViewGo = GameObject.Instantiate(Resources.Load(viewPath)) as GameObject;
            baseViewGo.transform.SetParent(this.GetCanvas, false);
            baseView = baseViewGo.GetComponent<BaseView>();

            if(baseView == null)
                throw new System.Exception(string.Format("{0} game object haven't component BaseView", viewType));

            mViewGoDict.Add(viewType, baseView);
        }
        else
        {
            baseView = this.mViewGoDict[viewType];
        }
        return baseView;
    }

    // 需要重写的方法
    public virtual BaseView PushView(ViewType.ViewTypes viewTypes) { return null; }
    public virtual void PopView() { }

}










