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

    File: ApplyRoutes/Edit/ExtendActions.cs
***********************************************************************/
using System;
using System.Collections.Generic;

using ZoneFiveSoftware.Common.Data.Fitness;
using ZoneFiveSoftware.Common.Data.GPS;
using ZoneFiveSoftware.Common.Visuals;
using ZoneFiveSoftware.Common.Visuals.Fitness;

namespace ApplyRoutesPlugin.Edit
{
    class ExtendActions : IExtendActivityEditActions
    {
        #region IExtendActivityEditActions Members

        IList<IAction> IExtendActivityEditActions.GetActions(IList<IActivity> activities)
        {
            if (activities == null || activities.Count == 0) return null;

            return new IAction[]
            {
                //new RouteManipulator(activities),
                new ApplyRouteAction(activities),
                new MakeRouteAction(activities)
            };
        }

        IList<IAction> IExtendActivityEditActions.GetActions(IActivity activity)
        {
            if (activity == null) return null;
            IList<IActivity> activities = new IActivity[] { activity };
            return new IAction[] {
                //new RouteManipulator(activities),
                new ApplyRouteAction(activities),
                new MakeRouteAction(activities)
            };
        }

        #endregion
    }
}
