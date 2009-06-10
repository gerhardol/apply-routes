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
            byte[] data = Plugin.GetApplication().Logbook.GetExtensionData(Plugin.thePlugin.Id);
            if (data == null || data.Length == 0)
            {
                data = new byte[] { 1, 1, 0 };
                Plugin.GetApplication().Logbook.SetExtensionData(Plugin.thePlugin.Id, data);
            }
            if (data[0] != 0)
            {
                actions.Add(new ApplyRouteAction(activities));
            }
            if (data[1] != 0)
            {
                actions.Add(new MakeRouteAction(activities));
            }
            if (data[2] != 0)
            {
                actions.Add(new UpdateEquipmentAction(activities));
            }

            return actions;
        }
    }
}
