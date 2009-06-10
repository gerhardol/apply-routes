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

    File: ApplyRoutes/Plugin.cs
***********************************************************************/
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Windows.Forms;
using ZoneFiveSoftware.Common.Visuals.Fitness;
using ZoneFiveSoftware.Common.Visuals;

namespace ApplyRoutesPlugin
{
    class Plugin : IPlugin
    {
        public Plugin()
        {
            plugin = this;
        }

        #region IPlugin Members

        public IApplication Application
        {
            set { application = value; }
        }

        public Guid Id
        {
            get { return new Guid("{a2dcf469-3d83-4690-8702-a21cd18ff7b3}"); }
        }

        public string Name
        {
            get { return Properties.Resources.Edit_ApplyRoutesPlugin_Text; }
        }

        public void ReadOptions(XmlDocument xmlDoc, XmlNamespaceManager nsmgr, XmlElement pluginNode)
        {
            
        }

        public string Version
        {
            get { return GetType().Assembly.GetName().Version.ToString(3); }
        }

        public void WriteOptions(XmlDocument xmlDoc, XmlElement pluginNode)
        {
            
        }

        #endregion

        public static Plugin thePlugin
        {
            get { return plugin; }
        }
        public static IApplication GetApplication()
        {
            return application;
        }

        public static void dialogButton_Click(object sender, EventArgs e)
        {
            if (typeof(IButtonControl).IsInstanceOfType(sender) &&
                typeof(Control).IsInstanceOfType(sender))
            {
                IButtonControl ibtn = (IButtonControl)sender;
                Control ctrl = (Control)sender;
                if (typeof(Form).IsInstanceOfType(ctrl.Parent))
                {
                    if (ibtn.DialogResult != DialogResult.None)
                    {
                        Form form = (Form)ctrl.Parent;
                        form.DialogResult = ibtn.DialogResult;
                        form.Close();
                    }
                }
            }
        }

        public static void SetIButtonClick(IButtonControl button)
        {
            if (typeof(Control).IsInstanceOfType(button))
            {
                ((Control)button).Click += new System.EventHandler(dialogButton_Click);
            }
        }

        public static void registerDefaultBtns(Form form)
        {
            SetIButtonClick(form.AcceptButton);
            SetIButtonClick(form.CancelButton);
            SetIButtonClick(null);
        }

        static public void ThemeChanged(Control control, ITheme visualTheme)
        {
            control.ForeColor = visualTheme.ControlText;
            control.BackColor = visualTheme.Control;
        }

        #region Private members
        private static IApplication application;
        private static Plugin plugin;
        #endregion   
    }
}
