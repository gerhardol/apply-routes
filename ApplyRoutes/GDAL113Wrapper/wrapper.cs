using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace GDAL113Wrapper
{
    public class wrapper
    {
        public static wrapper make_wrapper(string displayProjection, string projection)
        {
            try
            {
                return make_wrapper1(displayProjection, projection);
            }
            catch
            {
                return null;
            }
        }

        public void transform_to_map(double[] conv, double lng, double lat)
        {
            try
            {
                toMap.TransformPoint(conv, lng, lat, 0);
            }
            catch
            {
                conv[0] = lng;
                conv[1] = lat;
            }
        }

        public void transform_to_display(double[] conv, double lng, double lat)
        {
            try
            {
                toDisplay.TransformPoint(conv, lng, lat, 0);
            }
            catch
            {
                conv[0] = lng;
                conv[1] = lat;
            }
        }

        private wrapper(OSGeo.OSR.CoordinateTransformation toMap, OSGeo.OSR.CoordinateTransformation toDisplay)
        {
            this.toMap = toMap;
            this.toDisplay = toDisplay;
        }

        private static wrapper make_wrapper1(string displayProjection, string projection)
        {
            OSGeo.OSR.SpatialReference disp = MakeSR(displayProjection);
            OSGeo.OSR.SpatialReference map = MakeSR(projection);

            if (disp != null && map != null)
            {
                OSGeo.OSR.CoordinateTransformation toMap = MakeCT(disp, map);
                OSGeo.OSR.CoordinateTransformation toDisplay = MakeCT(map, disp);

                if (toMap != null && toDisplay != null)
                {
                    return new wrapper(toMap, toDisplay);
                }
            }
            return null;
        }
        
        private static OSGeo.OSR.SpatialReference MakeSR(string proj)
        {
            OSGeo.OSR.SpatialReference sr = null;
            try
            {
                sr = new OSGeo.OSR.SpatialReference("");
                Regex r = new Regex(@"EPSG:(\d+)");
                Match m = r.Match(proj);
                if (m.Success)
                {
                    sr.ImportFromEPSG(Convert.ToInt32(m.Groups[1].Value));
                }
                else
                {
                    sr.ImportFromProj4(proj);
                }
                if (sr.IsProjected() != 0)
                {
                    sr.ExportToProj4(out proj);
                    proj = proj.Replace(" +wktext ", " ");
                    sr = new OSGeo.OSR.SpatialReference("");
                    sr.ImportFromProj4(proj);
                }
            }
            catch
            {
                sr = null;
            }

            return sr;
        }
        
        private static OSGeo.OSR.CoordinateTransformation MakeCT(OSGeo.OSR.SpatialReference sr1, OSGeo.OSR.SpatialReference sr2)
        {
            OSGeo.OSR.CoordinateTransformation ct = null;
            try
            {
                ct = new OSGeo.OSR.CoordinateTransformation(sr1, sr2);
            }
            catch
            {
                ct = null;
            }
            return ct;
        }

        private OSGeo.OSR.CoordinateTransformation toMap = null;
        private OSGeo.OSR.CoordinateTransformation toDisplay = null;
    }
}
