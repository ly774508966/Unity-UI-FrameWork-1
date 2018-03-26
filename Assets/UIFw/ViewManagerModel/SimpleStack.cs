using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ViewManagerModel
{

    public class SimpleStack : BaseViewManager
    {
        
        private Stack<BaseView> mViewStack;


        public override BaseView PushView(ViewType.ViewTypes viewType)
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
        public override void PopView()
        {
            if (this.mViewStack == null)
            {
                this.mViewStack = new Stack<BaseView>();
                return;
            }
            BaseView baseView;
            if (this.mViewStack.Count > 0)
            {
                baseView = this.mViewStack.Pop();
                baseView.OnViewExit();
            }
            if (this.mViewStack.Count > 0)
            {
                baseView = this.mViewStack.Peek();
                baseView.OnViewResume();
            }
        }

    }

}
