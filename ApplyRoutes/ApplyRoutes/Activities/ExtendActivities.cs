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

    File: ApplyRoutes/Activities/ExtendActivities.cs
***********************************************************************/
using System;
using System.Collections.Generic;
using System.Text;

using ZoneFiveSoftware.Common.Visuals;
using ZoneFiveSoftware.Common.Visuals.Fitness;
using ApplyRoutesPlugin.Activities;

namespace ApplyRoutesPlugin.Views
{
    class ExtendActivities : IExtendActivityDetailPages
    {

        #region IExtendActivityDetailPages Members

#if ST_2_1
        public IList<IActivityDetailPage> ActivityDetailPages
        {
            get { return new IActivityDetailPage[] { new GMapActivityDetail() }; }
        }

#else
        public IList<IDetailPage> GetDetailPages(IDailyActivityView view, ExtendViewDetailPages.Location location)
        {
            return new IDetailPage[] { new GMapActivityDetail(view) };
        }
#endif

        #endregion
    }
}
