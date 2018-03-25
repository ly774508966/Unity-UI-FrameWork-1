using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewManager{

    // 保存ViewType对应的View实例
    private Dictionary<ViewType.ViewTypes, BaseView> mViewGoDict;

    // View的栈管理
    private Stack<BaseView> mViewStack;

    // 获取MainCanvas
    private Transform mCanvasTransform;
    private Transform GetCanvas
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

    // 创建ViewManager实例
    private static ViewManager mInstance;
    public static ViewManager Instance
    {
        get
        {
            if(mInstance == null)
            {
                mInstance = new ViewManager();
            }
            return mInstance;
        }
    }

    // 获取具体的View对象
    private BaseView GetView(ViewType.ViewTypes viewType)
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

    public BaseView PushView(ViewType.ViewTypes viewType)
    {
        if (this.mViewStack == null)
        {
            this.mViewStack = new Stack<BaseView>();
        }
        BaseView baseView;
        if (this.mViewStack.Count > 0)
        {
            baseView = this.mViewStack.Peek();
            baseView.OnViewPause();
        }
        baseView = this.GetView(viewType);
        this.mViewStack.Push(baseView);
        baseView = this.mViewStack.Peek();
        baseView.OnViewEnter();
        return baseView;
    }
    public void PopView()
    {
        if(this.mViewStack == null)
        {
            this.mViewStack = new Stack<BaseView>();
            return;
        }
        BaseView baseView;
        if(this.mViewStack.Count > 0)
        {
            baseView = this.mViewStack.Pop();
            baseView.OnViewExit();
        }
        if(this.mViewStack.Count > 0)
        {
            baseView = this.mViewStack.Peek();
            baseView.OnViewResume();
        }
    }
}
































































