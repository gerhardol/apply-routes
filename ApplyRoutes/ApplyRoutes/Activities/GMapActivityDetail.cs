/***********************************************************************
    Copyright 2008-2009 Mark Williams

    Licensed under the Apache License, Version 2.0 (the "License");
    you may not use this file except in compliance with the License.
    You may obtain a copy of the License at

        http://www.apache.org/licenses/LICENSE-2.0

    Unless required by applicable law or agreed to in writing, software
    distributed under the License is distributed on an "AS IS" BASIS,
    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
    See the License for the specific language governing permissions and
    limitations under the License. 

    File: ApplyRoutes/Activities/GMapActivityDetail.cs
***********************************************************************/
using System;
using System.Collections.Generic;
using System.Text;

using ZoneFiveSoftware.Common.Visuals;
using ZoneFiveSoftware.Common.Visuals.Fitness;
#if !ST_2_1
using ZoneFiveSoftware.Common.Visuals.Util;
#endif
using ZoneFiveSoftware.Common.Data.Fitness;
using System.Windows.Forms;
using System.ComponentModel;

namespace ApplyRoutesPlugin.Activities
{
    class GMapActivityDetail : IView, IDisposable,
#if ST_2_1
     IActivityDetailPage
#else
     IDetailPage
#endif
    {
        #region IActivityDetailPage Members

        public static GMapActivityDetail Singleton
        {
            get
            {
                return new GMapActivityDetail(true);
            }
        }
        public GMapActivityDetail()
        {
        }
        public GMapActivityDetail(bool isView)
        {
            _isView = isView;
        }
#if !ST_2_1
        public GMapActivityDetail(IDailyActivityView view)
        {
            this.view = view;
            view.SelectionProvider.SelectedItemsChanged += new EventHandler(OnViewSelectedItemsChanged);
            Plugin.GetApplication().PropertyChanged += new PropertyChangedEventHandler(Application_PropertyChanged);
        }

        private void OnViewSelectedItemsChanged(object sender, EventArgs e)
        {
            activities = CollectionUtils.GetAllContainedItemsOfType<IActivity>(view.SelectionProvider.SelectedItems);
            if ((control != null))
            {
                control.Activities = activities;
            }
        }
        void Application_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            //Hide/Show the page if view is changing
            //Really not important here as SelectedItems will not change anyway
            //(and there are no other triggers)
            if (null != view && e.PropertyName == "ActiveView")
            {
                Guid viewId = Plugin.GetApplication().ActiveView.Id;
                if (viewId == view.Id)
                {
                    if (_showPage && control != null) { this.ShowPage(_bookmark); }
                }
                else
                {
                    if (!_showPage && control != null) { this.HidePage(); }
                }
            }
        }
#else
        public IActivity Activity
        {
             set
            {
                if (null == value) { activities = null; }
                else { activities = new List<IActivity> { value }; }
                if ((control != null))
                {
                    control.Activities = activities;
                }
            }
        }
#endif

        public IList<string> MenuPath
        {
            get { return menuPath; }
            set { menuPath = value; OnPropertyChanged("MenuPath"); }
        }

        public bool MenuEnabled
        {
            get { return menuEnabled; }
            set { menuEnabled = value; OnPropertyChanged("MenuEnabled"); }
        }

        public bool MenuVisible
        {
            get { return menuVisible; }
            set { menuVisible = value; OnPropertyChanged("MenuVisible"); }
        }

        public bool PageMaximized
        {
            get { return pageMaximized; }
            set { pageMaximized = value; OnPropertyChanged("PageMaximized"); }
        }
        public void RefreshPage()
        {
            if (control != null)
            {
                control.RefreshPage();
            }
        }

        #endregion

        #region IDialogPage Members

        public Control CreatePageControl()
        {
            if (control == null)
            {
#if ST_2_1
                control = new GMapRouteControl();
#else
                control = new GMapRouteControl(view);
#endif
                control.Activities = activities;
            }
            return control;
        }

        public bool HidePage()
        {
            if (control != null)
            {
                control.Hide();
            }
            return true;
        }

        public string PageName
        {
            get { return Properties.Resources.Route_Control_Title; }
        }

        public void ShowPage(string bookmark)
        {
            if (control != null)
            {
                control.ShowPage();
            }
        }

        public IPageStatus Status
        {
            get { return null; }
        }

        public void ThemeChanged(ITheme visualTheme)
        {
            if (control != null)
            {
                control.ThemeChanged(visualTheme);
            }
        }

        public string Title
        {
            get { return Properties.Resources.Route_Control_Title; }
        }

        public void UICultureChanged(System.Globalization.CultureInfo culture)
        {
            if (control != null)
            {
                control.UICultureChanged(culture);
            }
        }
        #endregion

        #region INotifyPropertyChanged Members

        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

        #endregion

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private IList<IActivity> activities = new List<IActivity>();
        private GMapRouteControl control = null;
#if !ST_2_1
        private IDailyActivityView view = null;
        private bool _showPage = false;
        private string _bookmark = null;
#endif
        private IList<string> menuPath = null;
        private bool menuEnabled = true;
        private bool menuVisible = true;
        private bool pageMaximized = false;
        private bool _isView = false;

        #region IView Members

        public IList<IAction> Actions
        {
            get { return null; }
        }

        public Guid Id
        {
            //Note: Used in both View and ActivityPage
            get
            {
                if (_isView)
                {
                    return GUIDs.ApplyRoutesView;
                }
                else
                { 
                    return GUIDs.ActivityPage;
                }
            }
        }

        public string SubTitle
        {
            get { return null; }
        }

        public void SubTitleClicked(System.Drawing.Rectangle subTitleRect)
        {
        }

        public bool SubTitleHyperlink
        {
            get { return false; }
        }

        public string TasksHeading
        {
            get { return Properties.Resources.Route_Control_Title; }
        }

        #endregion

        public void Dispose()
        {
            this.control.Dispose();
        }
    }
}
