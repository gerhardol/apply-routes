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

    File: ApplyRoutes/Settings/ExtendSettingsPages.cs
***********************************************************************/
using System;
using System.Collections.Generic;
using System.Text;

using ZoneFiveSoftware.Common.Visuals;
using ZoneFiveSoftware.Common.Visuals.Fitness;
using ApplyRoutesPlugin.UI;
using System.ComponentModel;

namespace ApplyRoutesPlugin.Settings
{
    class ExtendSettingsPages : IExtendSettingsPages
    {
        #region IExtendSettingsPages Members

        public IList<ISettingsPage> SettingsPages
        {
            get
            {
                return new ISettingsPage[] { new ApplyRoutesSettings() }; 
            }
        }

        #endregion
    }

    public class ApplyRoutesSettings : ISettingsPage
    {
        #region ISettingsPage Members

        public Guid Id
        {
            get { return Plugin.thePlugin.Id; }
        }

        public IList<ISettingsPage> SubPages
        {
            get { return null; }
        }

        #endregion

        #region IDialogPage Members

        public System.Windows.Forms.Control CreatePageControl()
        {
            theControl = new SettingsControl();
            return theControl;
        }

        public bool HidePage()
        {
            return true;
        }

        public string PageName
        {
            get { return Plugin.thePlugin.Name; }
        }

        public void ShowPage(string bookmark)
        {
            if (theControl != null)
            {
                theControl.RefreshPage();
            }
        }

        public IPageStatus Status
        {
            get { return null; }
        }

        public void ThemeChanged(ITheme visualTheme)
        {
            if (theControl != null)
            {
                theControl.ThemeChanged(visualTheme);
            }
        }

        public string Title
        {
            get { return Plugin.thePlugin.Name; }
        }

        public void UICultureChanged(System.Globalization.CultureInfo culture)
        {
            if (theControl != null)
            {
                theControl.UICultureChanged(culture);
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

        private SettingsControl theControl;
    }
}
