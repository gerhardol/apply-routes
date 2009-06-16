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

    File: ApplyRoutes/MapProviders/GMapBrowser.cs
***********************************************************************/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ApplyRoutesPlugin.MapProviders
{
    public delegate void OnDoneHandler();
    public partial class GMapBrowser : Form
    {
        public GMapBrowser(string url)
        {
            InitializeComponent();
            webBrowser.DocumentCompleted += delegate(object sender, WebBrowserDocumentCompletedEventArgs e)
            {
                if (onDoneHandler != null)
                {
                    OnDoneHandler t = onDoneHandler;
                    onDoneHandler = null;
                    t();                    
                }
            };
            if (url != null)
            {
                Goto(url, null);
            }
        }

        private delegate void InvokeDelegate();
        public void Goto(string url, OnDoneHandler handler)
        {
            while (onDoneHandler != null)
            {
            }
            onDoneHandler = handler;

            this.webBrowser.Navigate(url);
            /*
            System.Delegate invoke = new InvokeDelegate(delegate() { this.webBrowser.Navigate(url); });
            this.webBrowser.Invoke(invoke);*/
        }

        public WebBrowser Browser
        {
            get { return webBrowser; }
        }

        private OnDoneHandler onDoneHandler = null;
    }
}
