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

    File: ApplyRoutes/MapProviders/ExtendMapProviders.cs
***********************************************************************/
using System;
using System.Collections.Generic;
using System.Text;
using ZoneFiveSoftware.Common.Visuals.Fitness;
using ZoneFiveSoftware.Common.Visuals.Fitness.GPS;

namespace ApplyRoutesPlugin.MapProviders
{
    class ExtendMapProviders : IExtendMapProviders
    {
        #region IExtendMapProviders Members

        public IList<IMapProvider> MapProviders
        {
            get
            {
                if (titles == null)
                {
                    titles = new SortedList<string, string>();
                    titles["NORMAL"] = "Street";
                    titles["SATELLITE"] = "Satellite";
                    titles["HYBRID"] = "Hybrid";
                    titles["MOON_ELEVATION"] = "Moon Elevation";
                    titles["MOON_VISIBLE"] = "Moon Visible";
                    titles["MARS_ELEVATION"] = "Mars Elevation";
                    titles["MARS_VISIBLE"] = "Mars Visible";
                    titles["MARS_INFRARED"] = "Mars Infrared";
                    titles["SKY_VISIBLE"] = "Sky Visible";
                    // titles["SATELLITE_3D"] = "Earth";
                    titles["PHYSICAL"] = "Terrain";
                }

                if (providers == null)
                {
                    providers = new List<GMapProvider>();
                    int i = 0;
                    foreach (string k in titles.Keys)
                    //string k = "NORMAL";
                    {
                        string v = titles[k];
                        Guid id = new Guid("{" + guids[i++] + "}");
                        providers.Add(new GMapProvider(k,v,id));
                    }
                }

                List<IMapProvider> ready = new List<IMapProvider>();
                foreach (GMapProvider mp in providers)
                {
                    if (mp.Ready)
                    {
                        ready.Add(mp);
                    }
                }
                if (ready.Count == 0)
                {
                    ready = null;
                }

                return ready;
            }
        }

        #endregion

        private IList<GMapProvider> providers = null;
        private string[] guids = 
            { 
                "1e6dc0f1-6dca-4d13-8c33-de6e2d200e5b",
                "9b4a949b-0605-477f-83f2-0b3b868d9430",
                "803a66e5-64d5-4c2f-a564-5146ed895cfa",
                "9207f188-3b77-44ec-8686-81c137953ce5",
                "4e168491-9a85-459e-8147-7d87ebf097fb",
                "19c701d4-fafe-490b-ba8c-b06314974fec",
                "098c028b-2284-4e44-8f3a-ba45e91c8fc1",
                "456dd429-f27a-4e8b-87e8-a78028829409",
                "9becc678-2982-4b9d-a297-a080dde9f910",
                "f294c1f3-e272-4de6-8638-c0ce1f6d8abe",
                "39b2a8e5-ef1c-4929-b058-dd3809f838d8",
                "866e253c-a312-47a1-9064-78de1178e254"
            };
        private SortedList<string, string> titles = null;
    }
}
