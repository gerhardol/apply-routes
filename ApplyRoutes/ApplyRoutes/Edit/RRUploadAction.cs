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

    File: ApplyRoutes/Edit/RRUploadAction.cs
***********************************************************************/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using ZoneFiveSoftware.Common.Data.Fitness;
using ZoneFiveSoftware.Common.Visuals;
#if !ST_2_1
using ZoneFiveSoftware.Common.Visuals.Fitness;
using ZoneFiveSoftware.Common.Visuals.Util;
using ZoneFiveSoftware.Common.Data;
#endif
using ApplyRoutesPlugin.Activities;
using ApplyRoutesPlugin.MapProviders;
using System.Security.Permissions;
using System.Windows.Forms;
using System.Diagnostics;

namespace ApplyRoutesPlugin.Edit
{
    public class RRUploadAction : IAction
    {
#if !ST_2_1
        public RRUploadAction(IDailyActivityView aview, IActivityReportsView rview)
        {
            this.dailyView = aview;
            this.reportView = rview;
        }
#else
        public RRUploadAction(IList<IActivity> activities, IList<IRoute> dummy)
        {
            this.activities = activities;
        }
#endif
        #region IAction Members

        public bool Enabled
        {
            get { return activities != null; }
        }

        public bool HasMenuArrow
        {
            get { return false; }
        }

        public IList<string> MenuPath
        {
            get
            {
                return new List<string>();
            }
        }
        
        public System.Drawing.Image Image
        {
            get { return Properties.Resources.ApplyRoutes.ToBitmap(); }
        }

        public void Refresh()
        {

        }

        public void Run(System.Drawing.Rectangle rectButton)
        {
            GMapBrowser browser = new GMapBrowser(ExtendMapProviders.UploadURL);
            browser.Browser.ObjectForScripting = new RRObjectForScriptingClass(this, browser.Browser);
            browser.Browser.IsWebBrowserContextMenuEnabled = true;

            browser.ShowDialog();
            browser.Dispose();
        }

        public string Title
        {
            get
            {
                int num = activities.Count;
                return Plugin.NumberedActivityText(Properties.Resources.Edit_RRUpload_Text, num);
            }
        }

        public bool Visible
        {
            get
            {
                if (activities.Count > 0) return true;
                return false;
            }
        }
        #endregion

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public void ReadyForUpload(WebBrowser webBrowser)
        {
            String xml = ApplyRoutesPlugin.Activities.GMapRouteControl.GetXMLForActivities(activities, null, false);
            webBrowser.Document.InvokeScript("st_upload", new Object[] { xml });
        }
#if !ST_2_1
        private IDailyActivityView dailyView = null;
        private IActivityReportsView reportView = null;
#endif
        private IList<IActivity> _activities = null;
        private IList<IActivity> activities
        {
            get
            {
#if !ST_2_1
                //activities are set either directly or by selection,
                //not by more than one
                if (_activities == null)
                {
                    if (dailyView != null)
                    {
                        return CollectionUtils.GetAllContainedItemsOfType<IActivity>(dailyView.SelectionProvider.SelectedItems);
                    }
                    else if (reportView != null)
                    {
                        return CollectionUtils.GetAllContainedItemsOfType<IActivity>(reportView.SelectionProvider.SelectedItems);
                    }
                    else
                    {
                        return new List<IActivity>();
                    }
                }
#endif
                return _activities;
            }
            set
            {
                _activities = value;
            }
        }
    }
    
    [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
    [System.Runtime.InteropServices.ComVisibleAttribute(true)]
    public class RRObjectForScriptingClass
    {
        public RRObjectForScriptingClass(RRUploadAction o, WebBrowser b)
        {
            owner = o;
            browser = b;
        }

        public void ReadyForUpload()
        {
            owner.ReadyForUpload(browser);
        }

        public void visit_link(object address)
        {
            try
            {
                String loc = address.ToString();
                if (loc.StartsWith("http://"))
                {
                    ProcessStartInfo procStartInfo = new ProcessStartInfo(loc);
                    Process.Start(procStartInfo);
                }
            }
            catch // (Exception ex)
            {
                MessageBox.Show("Exception encountered launching browser", "Launching other application", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }

        public bool wantsPageLoaded = false;
        public bool wantsMapLoaded = false;
        public bool wantsReadyForUpload = true;
        private RRUploadAction owner;
        private WebBrowser browser;
    }

}

