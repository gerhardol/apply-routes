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
using ApplyRoutesPlugin.UI;
using ApplyRoutesPlugin.Views;

namespace ApplyRoutesPlugin.Edit
{
    class ExtendActions : IExtendActivityEditActions, IExtendRouteEditActions
    {
        #region IExtendActivityEditActions Members

        IList<IAction> IExtendActivityEditActions.GetActions(IList<IActivity> activities)
        {
            return MyActions(activities);
        }

        IList<IAction> IExtendActivityEditActions.GetActions(IActivity activity)
        {
            if (activity == null) return null;
            return MyActions(new IActivity[] { activity });
        }

        #endregion

        IList<IAction> MyActions(IList<IActivity> activities)
        {
            if (activities == null || activities.Count == 0) return null;

            List<IAction> actions = new List<IAction>();
            EditMenuSettingsInfo info = EditMenuSettingsInfo.Get();
            
            if (info.showApplyRoutes)
            {
                actions.Add(new ApplyRouteAction(activities));
            }
            if (info.showCreateRoutes)
            {
                actions.Add(new MakeRouteAction(activities));
            }
            if (info.showUpdateEquipment)
            {
                actions.Add(new UpdateEquipmentAction(activities, null));
            }
            if (info.showSendToRoutes && !Plugin.IsRunningOnMono())
            {
                actions.Add(new SendToView(activities, null));
            }
            if (info.showJoinRoutes)
            {
                actions.Add(new JoinActivitiesAction(activities));
            }
            if (info.showRRUpload)
            {
                actions.Add(new RRUploadAction(activities));
            }

            if (actions.Count == 0)
            {
                actions = null;
            }
            return actions;
        }

        #region IExtendRouteEditActions Members

        IList<IAction> MyRouteActions(IList<IRoute> routes)
        {
            EditMenuSettingsInfo info = EditMenuSettingsInfo.Get();
            List<IAction> actions = new List<IAction>();
            if (info.showUpdateEquipment)
            {
                actions.Add(new UpdateEquipmentAction(null, routes));
            }
            if (info.showSendToRoutes)
            {
                actions.Add(new SendToView(null, routes));
            }

            if (actions.Count == 0)
            {
                actions = null;
            }
            return actions;
        }

        public IList<IAction> GetActions(IList<IRoute> routes)
        {
            return MyRouteActions(routes);
        }

        public IList<IAction> GetActions(IRoute route)
        {
            List<IRoute> list = new List<IRoute>();
            if (route != null)
            {
                list.Add(route);
            }
            if (list.Count == 0)
            {
                list = null;
            }
            return MyRouteActions(list);
        }

        #endregion
    }
}
