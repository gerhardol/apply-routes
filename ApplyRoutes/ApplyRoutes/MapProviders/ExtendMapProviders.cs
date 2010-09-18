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
#if ST_2_1
using ZoneFiveSoftware.Common.Visuals.Fitness.GPS;
#else
using ZoneFiveSoftware.Common.Visuals.Mapping;
#endif
using System.Xml;
using ApplyRoutesPlugin.Activities;

namespace ApplyRoutesPlugin.MapProviders
{
    class ExtendMapProviders : 
#if ST_2_1
        IExtendMapProviders
#else
        IExtendMapTileProviders
#endif
    {
        public static IList<GMapProvider> GetMapProviders()
        {
            if (mpiList == null)
            {
                ResetDefaults();
            }
 
           if (providers == null && ! Plugin.IsRunningOnMono())
            {
                providers = new List<GMapProvider>();
                //int i = 0;
                foreach (MapProviderInfo info in mpiList)
                {
                    Guid id = new Guid("{" + info.guid/*guids[i++]*/ + "}");
                    GMapProvider p = new GMapProvider(info.url, info.title, id);
                    p.Enabled = info.enabled;
                    providers.Add(p);
                }
            }
            return providers;
        }

        public class MapProviderInfo
        {
            public string guid = null;
            public string title = null;
            public string url = null;
            public bool enabled = false;
        }
        
        #region IExtendMapProviders Members
#if ST_2_1
        public IList<IMapProvider> MapProviders
#else
        public IList<IMapTileProvider> MapTileProviders
#endif
        {
            get
            {
                GetMapProviders();
#if ST_2_1
                List<IMapProvider> ready = new List<IMapProvider>();
#else
                List<IMapTileProvider> ready = new List<IMapTileProvider>();
#endif
                foreach (GMapProvider mp in providers)
                {
                    if (mp.Enabled && !mp.Url.EndsWith("&gmrc&"))
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

        private static void ResetDefaults()
        {
            mpiList = new List<MapProviderInfo>();
            string[] strings = new string[] {
            "gmaps:HYBRID", "Hybrid", "true", "313f8830-bde2-11df-851a-0800200c9a66",
            "gmaps:MARS_ELEVATION", "Mars Elevation", "false", "313f8831-bde2-11df-851a-0800200c9a66",
            "gmaps:MARS_INFRARED", "Mars Infrared", "false", "313f8832-bde2-11df-851a-0800200c9a66",
            "gmaps:MARS_VISIBLE", "Mars Visible", "false", "313f8833-bde2-11df-851a-0800200c9a66",
            "gmaps:MOON_ELEVATION", "Moon Elevation", "false", "313f8834-bde2-11df-851a-0800200c9a66",
            "gmaps:MOON_VISIBLE", "Moon Visible", "false", "313f8835-bde2-11df-851a-0800200c9a66",
            "gmaps:NORMAL", "Street", "false", "313f8836-bde2-11df-851a-0800200c9a66",
            "gmaps:PHYSICAL", "Terrain", "false", "313f8837-bde2-11df-851a-0800200c9a66",
            "gmaps:SATELLITE", "Satellite", "false", "313f8838-bde2-11df-851a-0800200c9a66",
            "gmaps:SATELLITE_3D&gmrc", "Earth", "true", "313f8839-bde2-11df-851a-0800200c9a66",
            "gmaps:SKY_VISIBLE", "Sky Visible", "false", "313f883a-bde2-11df-851a-0800200c9a66",
            "msmaps:Aerial", "Bing-Aerial", "true", "313f883b-bde2-11df-851a-0800200c9a66",
            "msmaps:Hybrid", "Bing-Hybrid", "true", "313f883c-bde2-11df-851a-0800200c9a66",
            "msmaps:Hybrid-3d&gmrc", "Bing-3D", "true", "313f883d-bde2-11df-851a-0800200c9a66",
            "msmaps:Road", "Bing-Road", "true", "313f883e-bde2-11df-851a-0800200c9a66",
            "geoportail.html:Hybrid", "Geoportail", "true", "313f883f-bde2-11df-851a-0800200c9a66",
            "openlayers:OpenStreetMapMapnik", "OpenLayers - Open Streetmap", "false", "313f8840-bde2-11df-851a-0800200c9a66"
            };

            for (int j = 0; j < strings.GetLength(0); j += 4)
            {
                MapProviderInfo info = new MapProviderInfo();
                string k = strings[j];
                string[] keys = k.Split(':');
                bool enabled = true;
                bool.TryParse(strings[j+2], out enabled);
#if ST_2_1
                if (k.Equals("gmaps:NORMAL") || k.Equals("gmaps:PHYSICAL") ||
                    k.Equals("gmaps:SATELLITE") || k.Equals("openlayers:OpenStreetMapMapnik"))
                {
                    //These maps are included in ST3, but may be needed in ST2
                    enabled = true;
                }
#endif
                info.title = "AR - " + strings[j+1];
                if (!keys[0].Contains("."))
                {
                    keys[0] += ".html";
                }
                info.url = "http://maps.myosotissp.com/" + keys[0] + "#t=" + keys[1] + "&";
                info.guid = strings[j + 3];//guids[i++];
                info.enabled = enabled;
                mpiList.Add(info);
            }
        }

        public static void ApplyDefaults()
        {
            ResetDefaults();
            for (int i = 0; i < mpiList.Count; i++) {
                GMapProvider p = providers[i];
                MapProviderInfo info = mpiList[i];
                p.Title = info.title;
                p.Url = info.url;
                p.Enabled = info.enabled;
            }
        }

        public static void ReadOptions(XmlDocument xmlDoc, XmlNamespaceManager nsmgr, XmlElement pluginNode)
        {
            ResetDefaults();
            if (pluginNode != null)
            {
                XmlNodeList mapProviderLists = pluginNode.GetElementsByTagName("MapProviders");
                if (mapProviderLists.Count == 1)
                {
                    XmlNode mapProviderList = mapProviderLists[0];
                    XmlAttribute att = mapProviderList.Attributes["cur_guid"];
                    if (att != null)
                    {
                        GMapRouteControl.SelectedGuid = att.Value;
                    }

                    XmlNodeList mapProviders = mapProviderList.ChildNodes;
                    SortedList<string, int> guidMap = new SortedList<string, int>();

                    int i = 0;
                    foreach (string guid in guids)
                    {
                        guidMap[guid] = i++;
                    }
                    i = 0;
                    foreach (XmlNode mapProvider in mapProviders)
                    {
                        if (i == mpiList.Count)
                        {
                            break;
                        }
                        MapProviderInfo info;
                        XmlAttribute guidAtt = mapProvider.Attributes["Guid"];
                        if (guidAtt != null && guidMap.ContainsKey(guidAtt.Value))
                        {
                            info = mpiList[guidMap[guidAtt.Value]];
                        }
                        else
                        {
                            info = mpiList[i];
                            //info.guid = guids[i];
                        }
                        XmlAttribute titleAtt = mapProvider.Attributes["Title"];
                        XmlAttribute urlAtt = mapProvider.Attributes["Url"];
                        XmlAttribute enabledAtt = mapProvider.Attributes["Enabled"];
                        if (titleAtt != null && urlAtt != null && enabledAtt != null)
                        {
                            info.enabled = enabledAtt.Value == "1";
                            info.title = titleAtt.Value;
                            info.url = urlAtt.Value.Replace("maps.myosotissp.com/index.html", "maps.myosotissp.com/gmaps.html");
                        }
                        else
                        {
                            info.enabled = false;
                        }
                        i++;
                    }
                }
                XmlNodeList uploadUrls = pluginNode.GetElementsByTagName("UploadUrl");
                if (uploadUrls.Count == 1)
                {
                    XmlNode uurl = uploadUrls[0];
                    XmlAttribute att = uurl.Attributes["Url"];
                    UploadURL = att.Value;
                }
            }
        }
                
        public static void WriteOptions(XmlDocument xmlDoc, XmlElement pluginNode)
        {
            XmlNode mapProviderList = xmlDoc.CreateElement("MapProviders");
            XmlAttribute att;
            
            att = xmlDoc.CreateAttribute("cur_guid");
            att.Value = GMapRouteControl.SelectedGuid;
            mapProviderList.Attributes.Append(att);

            pluginNode.AppendChild(mapProviderList);

            if (providers != null)
            {
                foreach (GMapProvider p in providers)
                {
                    XmlNode mp = xmlDoc.CreateElement("MapProvider");

                    att = xmlDoc.CreateAttribute("Guid");
                    att.Value = p.Id.ToString();
                    mp.Attributes.Append(att);

                    att = xmlDoc.CreateAttribute("Title");
                    att.Value = p.Title;
                    mp.Attributes.Append(att);

                    att = xmlDoc.CreateAttribute("Url");
                    att.Value = p.Url;
                    mp.Attributes.Append(att);

                    att = xmlDoc.CreateAttribute("Enabled");
                    att.Value = p.Enabled ? "1" : "0";
                    mp.Attributes.Append(att);
                    mapProviderList.AppendChild(mp);
                }
            }

            if (uploadUrl != null)
            {
                XmlNode uurl = xmlDoc.CreateElement("UploadUrl");

                att = xmlDoc.CreateAttribute("Url");
                att.Value = uploadUrl;
                uurl.Attributes.Append(att);
                pluginNode.AppendChild(uurl);
            }
        }

        private static IList<GMapProvider> providers = null;
        private static string[] guids = 
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
                "866e253c-a312-47a1-9064-78de1178e254",
                "9cc306e0-585e-11dd-ae16-0800200c9a66",
                "9cc306e1-585e-11dd-ae16-0800200c9a66",
                "9cc306e2-585e-11dd-ae16-0800200c9a66",
                "9cc306e3-585e-11dd-ae16-0800200c9a66",
                "9cc306e4-585e-11dd-ae16-0800200c9a66",
                "9cc306e5-585e-11dd-ae16-0800200c9a66",
                "9cc306e6-585e-11dd-ae16-0800200c9a66",
                "9cc306e7-585e-11dd-ae16-0800200c9a66",
                "9cc306e8-585e-11dd-ae16-0800200c9a66",
                "9cc306e9-585e-11dd-ae16-0800200c9a66"
            };
        private static List<MapProviderInfo> mpiList = null;

        private static String defaultUploadUrl = "http://replayroutes.com/upload.html";
        public static String UploadURL
        {
            get { return uploadUrl == null ? defaultUploadUrl : uploadUrl; }
            set { uploadUrl = value == null || value == defaultUploadUrl ? null : value; }
        }

        private static String uploadUrl = null;
    }
}
