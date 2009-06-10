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
using ZoneFiveSoftware.Common.Data.Fitness;
using System.Windows.Forms;
using System.ComponentModel;

namespace ApplyRoutesPlugin.Activities
{
    class GMapActivityDetail : IActivityDetailPage
    {
        #region IActivityDetailPage Members

        public IActivity Activity
        {
            set
            {
                activity = value;
                if (control != null)
                {
                    control.Activities = activity != null ? new IActivity[] { activity } : null;
                }
            }
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
            control = new GMapRouteControl();
            if (activity != null)
            {
                control.Activities = new IActivity[] { activity };
            }
            return control;
        }

        public bool HidePage()
        {
            return true;
        }

        public string PageName
        {
            get { return "GMaps Route Control"; }
        }

        public void ShowPage(string bookmark)
        {
            
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
            get { return "GMaps Route Control"; }
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

        private IActivity activity = null;
        private GMapRouteControl control = null;
    }
}
