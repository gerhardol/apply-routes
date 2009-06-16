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

    File: ApplyRoutes/Views/ExtendViews.cs
***********************************************************************/
using System;
using System.Collections.Generic;
using System.Text;
using ApplyRoutesPlugin.Activities;
using ZoneFiveSoftware.Common.Visuals;
using ZoneFiveSoftware.Common.Visuals.Fitness;

namespace ApplyRoutesPlugin.Views
{
    class ExtendViews : IExtendViews
    {
        #region IExtendViews Members

        public IList<IView> Views
        {
            get { return new IView[] { GMapActivityDetail.Singleton }; }
        }

        #endregion
    }
}
