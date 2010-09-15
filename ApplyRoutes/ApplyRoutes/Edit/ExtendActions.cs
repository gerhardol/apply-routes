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
    class ExtendActions : 
#if ST_2_1
    IExtendActivityEditActions, IExtendRouteEditActions
#else
    IExtendDailyActivityViewActions, IExtendActivityReportsViewActions, IExtendRouteViewActions
#endif
    {
#if ST_2_1
        #region IExtendActivityEditActions Members

        IList<IAction> IExtendActivityEditActions.GetActions(IList<IActivity> activities)
        {
            return MyEditActions(activities, null);
        }

        IList<IAction> IExtendActivityEditActions.GetActions(IActivity activity)
        {
            if (activity == null) return null;
            return MyEditActions(new IActivity[] { activity }, null);
        }

        #endregion

        #region IExtendRouteEditActions Members
        public IList<IAction> GetActions(IList<IRoute> routes)
        {
            return MyRouteEditActions(routes);
        }

        public IList<IAction> GetActions(IRoute route)
        {
            IList<IRoute> list = new List<IRoute>();
            if (route != null)
            {
                list.Add(route);
            }
            if (list.Count == 0)
            {
                list = null;
            }
            return MyRouteEditActions(list);
        }

        #endregion
#else
        #region IExtendDailyActivityViewActions Members
        public IList<IAction> GetActions(IDailyActivityView view,
                                                 ExtendViewActions.Location location)
        {
            if (location == ExtendViewActions.Location.EditMenu)
            {
                return MyEditActions(view, null);
            }
            else if (location == ExtendViewActions.Location.AnalyzeMenu)
            {
                return MyAnalyzeActions(view, null);
            }
            else if (location == ExtendViewActions.Location.ExportMenu)
            {
                return MyExportActions(view, null);
            }
            else return new IAction[0];
        }
        public IList<IAction> GetActions(IActivityReportsView view,
                                         ExtendViewActions.Location location)
        {
            if (location == ExtendViewActions.Location.EditMenu)
            {
                return MyEditActions(null, view);
            }
            else if (location == ExtendViewActions.Location.AnalyzeMenu)
            {
                return MyAnalyzeActions(null, view);
            }
            else if (location == ExtendViewActions.Location.ExportMenu)
            {
                return MyExportActions(null, view);
            }
            else return new IAction[0];
        }
        public IList<IAction> GetActions(IRouteView view,
                                        ExtendViewActions.Location location)
        {
            if (location == ExtendViewActions.Location.EditMenu)
            {
                return MyRouteEditActions(view);
            }
            else if (location == ExtendViewActions.Location.AnalyzeMenu)
            {
                return MyRouteAnalyzeActions(view);
            }
            //else if (location == ExtendViewActions.Location.ExportMenu)
            //{
            //    return MyRouteExportActions(view);
            //}
            else return new IAction[0];
        }
        #endregion
#endif

        //Note: "view" parameters has different meaning for ST2, to keep same codebase
        //Affects Edit implementation too
        //Generics cannot be used in a simple way
#if ST_2_1
        IList<IAction> MyEditActions(IList<IActivity> aview, IList<IRoute> rview)
#else
        IList<IAction> MyEditActions(IDailyActivityView aview, IActivityReportsView rview)
#endif  
        {
            IList<IAction> actions = new List<IAction>();
            EditMenuSettingsInfo info = EditMenuSettingsInfo.Get();

            if (info.showApplyRoutes)
            {
                actions.Add(new ApplyRouteAction(aview, rview));
            }
            if (info.showCreateRoutes)
            {
                actions.Add(new MakeRouteAction(aview, rview));
            }
            if (info.showUpdateEquipment)
            {
                actions.Add(new UpdateEquipmentAction(aview, rview));
            }
            if (info.showJoinRoutes)
            {
                actions.Add(new JoinActivitiesAction(aview, rview));
            }
#if ST_2_1
            IList<IAction> actions2 = MyAnalyzeActions(aview, rview);
            foreach (IAction act in actions2)
            {
                actions.Add(act);
            }
            actions2 = MyExportActions(aview, rview);
            foreach (IAction act in actions2)
            {
                actions.Add(act);
            }
#endif
            return actions;
        }

#if ST_2_1
        IList<IAction> MyAnalyzeActions(IList<IActivity> aview, IList<IRoute> rview)
#else
        IList<IAction> MyAnalyzeActions(IDailyActivityView aview, IActivityReportsView rview)
#endif
        {
            IList<IAction> actions = new List<IAction>();
            EditMenuSettingsInfo info = EditMenuSettingsInfo.Get();

            if (info.showSendToRoutes)
            {
                actions.Add(new SendToView(aview, rview));
            }

            return actions;
        }
#if ST_2_1
        IList<IAction> MyExportActions(IList<IActivity> aview, IList<IRoute> rview)
#else
        IList<IAction> MyExportActions(IDailyActivityView aview, IActivityReportsView rview)
#endif
        {
            IList<IAction> actions = new List<IAction>();
            EditMenuSettingsInfo info = EditMenuSettingsInfo.Get();

            if (info.showRRUpload)
            {
                actions.Add(new RRUploadAction(aview, rview));
            }

            return actions;
        }

#if ST_2_1
        IList<IAction> MyRouteEditActions(IList<IRoute> view)
#else
        IList<IAction> MyRouteEditActions(IRouteView view)
#endif
        {
            IList<IAction> actions = new List<IAction>();
            EditMenuSettingsInfo info = EditMenuSettingsInfo.Get();
            if (info.showUpdateEquipment)
            {
                actions.Add(new UpdateEquipmentAction(null, view));
            }
#if ST_2_1
            IList<IAction> actions2 = MyRouteAnalyzeActions(view);
            foreach (IAction act in actions2)
            {
                actions.Add(act);
            }
            //actions2 = MyExportActions(view);
            //foreach (IAction act in actions2)
            //{
            //    actions.Add(act);
            //}
#endif

            return actions;
        }
#if ST_2_1
        IList<IAction> MyRouteAnalyzeActions(IList<IRoute> view)
#else
        IList<IAction> MyRouteAnalyzeActions(IRouteView view)
#endif
        {
            IList<IAction> actions = new List<IAction>();
            EditMenuSettingsInfo info = EditMenuSettingsInfo.Get();
            if (info.showSendToRoutes)
            {
                actions.Add(new SendToView(null, view));
            }

            return actions;
        }
    }
}
