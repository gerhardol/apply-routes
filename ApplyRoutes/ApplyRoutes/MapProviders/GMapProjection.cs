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

    File: ApplyRoutes/MapProviders/GMapProvider.cs
***********************************************************************/
using System;
using System.Collections.Generic;
using System.Text;
#if ST_2_1
using ZoneFiveSoftware.Common.Visuals.Fitness.GPS;
#else
using ZoneFiveSoftware.Common.Visuals.Mapping;
#endif
using System.Drawing;
using ZoneFiveSoftware.Common.Data.GPS;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using mshtml;
using System.Text.RegularExpressions;
using System.Threading;
using System.ComponentModel;
using System.Security.Permissions;
using System.Globalization;


namespace ApplyRoutesPlugin.MapProviders
{
    class GMapProjection :
        IMapProjection
    {
        #region IMapProjection Members

        private delegate void InvokeDelegate();
        //private bool untrue = false;

        private readonly GMapProvider m_Prov; //TODO: Better split
        public GMapProjection(GMapProvider prov)
        {
            m_Prov = prov;
        }
        private Point GDALGPSToPixel(IGPSLocation where, double zoomLevel)
        {
            int zm = m_Prov.GMZoom(zoomLevel);
            double x, y;
            float res = m_Prov.proj_res[zm];
            double[] conv = new double[3];
#if ST_2_1
            GDAL111Wrapper.wrapper w1 = m_Prov.gdal_wrapper as GDAL111Wrapper.wrapper;
            GDAL11013Wrapper.wrapper w2 = m_Prov.gdal_wrapper as GDAL11013Wrapper.wrapper;
#else
            GDAL113Wrapper.wrapper w1 = m_Prov.gdal_wrapper as GDAL113Wrapper.wrapper;
#endif
            if (w1 != null)
            {
                w1.transform_to_map(conv, where.LongitudeDegrees, where.LatitudeDegrees);
            }
#if ST_2_1
            else if (w2 != null)
            {
                w2.transform_to_map(conv, where.LongitudeDegrees, where.LatitudeDegrees);
            } 
#endif
            else {
                conv[0] = where.LongitudeDegrees;
                conv[1] = where.LatitudeDegrees;
            }
            x = (conv[0] - m_Prov.proj_bounds[0]) / res;
            y = (m_Prov.proj_bounds[3] - conv[1]) / res;

            return new Point((int)Math.Round(x), (int)Math.Round(y));
        }

        public Point MercatorGPSToPixel(IGPSLocation where, double zoomLevel)
        {
            if (m_Prov.gdal_wrapper != null)
            {
                return GDALGPSToPixel(where, zoomLevel);
            }

            int zm = m_Prov.GMZoom(zoomLevel);
            double x, y;
            double z1 = Math.Pow(2, zm) * 64;
            double z2 = z1 * 2;

            x = where.LongitudeDegrees * z2 / 180 + z2;
            double s = Math.Sin(where.LatitudeDegrees * Math.PI / 180);
            s = Math.Max(s, -.9999);
            s = Math.Min(s, 0.9999);
            y = z2 - z1 * Math.Log((1 + s) / (1 - s)) / Math.PI;
            return new Point((int)Math.Round(x), (int)Math.Round(y));
        }

        public IGPSLocation GDALPixelToGPS(Point where, double zoomLevel)
        {
            int zm = m_Prov.GMZoom(zoomLevel);
            double x, y;
            float res = m_Prov.proj_res[zm];
            double[] conv = new double[3];

            double xin = where.X * res + m_Prov.proj_bounds[0];
            double yin = m_Prov.proj_bounds[3] - where.Y * res;
#if ST_2_1
            GDAL111Wrapper.wrapper w1 = m_Prov.gdal_wrapper as GDAL111Wrapper.wrapper;
            GDAL11013Wrapper.wrapper w2 = m_Prov.gdal_wrapper as GDAL11013Wrapper.wrapper;
#else
            GDAL113Wrapper.wrapper w1 = m_Prov.gdal_wrapper as GDAL113Wrapper.wrapper;
#endif
            if (w1 != null)
            {
                w1.transform_to_display(conv, xin, yin);
            }
#if ST_2_1
            else if (w2 != null)
            {
                w2.transform_to_display(conv, xin, yin);
            }
#endif
            else
            {
                conv[0] = xin;
                conv[1] = yin;
            }

            x = conv[0];
            y = conv[1];

            return new GPSLocation((float)y, (float)x);
        }

        public IGPSLocation MercatorPixelToGPS(Point where, double zoomLevel)
        {
            if (m_Prov.gdal_wrapper != null)
            {
                return GDALPixelToGPS(where, zoomLevel);
            }

            int zm = m_Prov.GMZoom(zoomLevel);
            double x, y;
            double z = 128 * Math.Pow(2, zm);

            x = ((where.X) / z - 1) * 180;
            y = Math.Min(Math.Max(1 - (where.Y) / z, -1), 1) * Math.PI;

            y = (2 * Math.Atan(Math.Exp(y)) - Math.PI / 2) * 180 / Math.PI;

            return new GPSLocation((float)y, (float)x);
        }

        public Point GPSToPixel(IGPSLocation origin, double zoomLevel, IGPSLocation gps)
        {
            Point orgP = MercatorGPSToPixel(origin, zoomLevel);
            Point gpsP = MercatorGPSToPixel(gps, zoomLevel);
            return new Point(gpsP.X - orgP.X, gpsP.Y - orgP.Y);
        }

        public IGPSLocation PixelToGPS(IGPSLocation origin, double zoomLevel, Point pixel)
        {
            if (origin == null) return origin;
            Point orgP = MercatorGPSToPixel(origin, zoomLevel);
            Point pos = new Point(orgP.X + pixel.X, orgP.Y + pixel.Y);
            IGPSLocation loc = MercatorPixelToGPS(pos, zoomLevel);
            Point p2 = GPSToPixel(origin, zoomLevel, loc);
            if (p2.X != pixel.X || p2.Y != pixel.Y)
            {
                p2.X = pixel.X;
            }
            return loc;
        }

        #endregion
    }
}
