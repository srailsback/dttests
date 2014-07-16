using System;

namespace dttests.Models
{
    public class Segment
    {

        public Guid SegmentKey { get; set; }
        public string FIPS { get; set; }
        public string ROUTE { get; set; }
        public int SEGMID { get; set; }
        public decimal LENGTH { get; set; }
        public string FIPSCOUNTY { get; set; }
        public string FUNCCLASSID { get; set; }
        public string GOVLEVEL { get; set; }
        public string ADMINCLASS { get; set; }
        public string POPULATION { get; set; }
        public string NHSDESIG { get; set; }
        public int? PRIIRI { get; set; }
        public decimal PRIPSI { get; set; }
        public string PROJYR { get; set; }
        public string BUILTYR { get; set; }
        public string INSPYR { get; set; }
        public decimal? PRITREATMENTDEPTH { get; set; } //NOTE: using decimal? because the model binder won't bind "" to decimal
        public string PRISURF { get; set; }
        public int PRISURFWD { get; set; }
        public int THRULNQTY { get; set; }
        public int PRITHRULNWD { get; set; }
        public string OPERATION { get; set; }
        public string RRXID { get; set; }
        public string STRID { get; set; }
        public string FORESTROUTE { get; set; }
        public string JURSPLIT { get; set; }
        public string ROUTENAME { get; set; }
        public string FROMFEATURE { get; set; }
        public string TOFEATURE { get; set; }
        public string SEGMDIR { get; set; }
        public string SEGMPREFIX { get; set; }
        public string SPECIALSYS { get; set; }
        public string NAAQSID { get; set; }
        public string URBAN { get; set; }
        public string ACCESS { get; set; }
        public string TRKRESTRICT { get; set; }
        public string COUNTSTATIONID { get; set; }
        public DateTime? PRIIRIDATE { get; set; }
        public string UPDATEYR { get; set; }
        public string REGION { get; set; }
        public string TPRID { get; set; }
        public string TERRAIN { get; set; }
        public string ROUTESIGN { get; set; }
        public string ROUTESIGNQUAL { get; set; }
        public string GISID { get; set; }
        public bool ELIGIBLE
        {
            get
            {
                return this.ADMINCLASS == "1" || this.ADMINCLASS == "2";
            }
        }
        public string Jurisdiction { get; set; }
        public bool IsPaved
        {
            get
            {
                int priSurf;
                if (int.TryParse(this.PRISURF, out priSurf))
                {
                    //return priSurf > 50;
                    // the following codes are Unpaved – 13, 14, 15, 16; everything else is Paved
                    return !(priSurf == 13
                            || priSurf == 14
                            || priSurf == 15
                            || priSurf == 16);
                }
                //if it could not be parsed to an int (ie it was null) we consider it unpaved
                return false;
            }
        }
        public string PRIYRREHAB { get; set; }
        public bool ISDIVIDED { get; set; }
        public string LRSROUTE { get; set; }
        public decimal FROMMEAS { get; set; }
        public decimal TOMEAS { get; set; }
        public DateTime? LastEditedAtUTC { get; set; }
        public Segment()
        {
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="Segment"/> class.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <remarks>Receives a dynamic object from GRDMS</remarks>
        public Segment(dynamic x)
        {
            this.SegmentKey = Guid.Parse(x.SegmentKey.ToString());
            this.FIPS = x.FIPS.Trim();
            this.ROUTE = x.ROUTE.Trim();
            this.SEGMID = int.Parse(x.SEGMID.ToString());
            this.LENGTH = decimal.Parse(x.LENGTH_.ToString());
            this.FIPSCOUNTY = x.FIPSCOUNTY.Trim();
            this.FUNCCLASSID = x.FUNCCLASSID.Trim();
            this.GOVLEVEL = x.GOVLEVEL.Trim();
            this.ADMINCLASS = x.ADMINCLASS.Trim();
            this.POPULATION = x.POPULATION.Trim();
            this.NHSDESIG = x.NHSDESIG;
            this.PRIIRI = x.PRIIRI != null ? int.Parse(x.PRIIRI.ToString()) : null;
            this.PRIPSI = decimal.Parse(x.PRIPSI.ToString());
            this.PROJYR = x.PROJYR;
            this.BUILTYR = x.BUILTYR;
            this.INSPYR = x.INSPYR;
            this.PRITREATMENTDEPTH = x.PRITREATMENTDEPTH != null ? decimal.Parse(x.PRITREATMENTDEPTH.ToString()) : 0;
            this.PRISURF = x.PRISURF;
            this.PRISURFWD = x.PRISURFWD != null ? int.Parse(x.PRISURFWD.ToString()) : 0;
            this.THRULNQTY = x.THRULNQTY != null ? int.Parse(x.THRULNQTY.ToString()) : 0;
            this.PRITHRULNWD = x.PRITHRULNWD != null ? int.Parse(x.PRITHRULNWD.ToString()) : 0;
            this.OPERATION = x.OPERATION;
            this.RRXID = x.RRXID;
            this.STRID = x.STRID;
            this.FORESTROUTE = x.FORESTROUTE;
            this.JURSPLIT = x.JURSPLIT;
            this.ROUTENAME = x.ROUTENAME;
            this.FROMFEATURE = x.FROMFEATURE;
            this.TOFEATURE = x.TOFEATURE;
            this.SEGMDIR = x.SEGMDIR;
            this.SEGMPREFIX = x.SEGMPREFIX;
            this.SPECIALSYS = x.SPECIALSYS;
            this.NAAQSID = x.NAAQSID;
            this.URBAN = x.URBAN;
            this.ACCESS = x.ACCESS_;
            this.TRKRESTRICT = x.TRKRESTRICT;
            this.COUNTSTATIONID = x.COUNTSTATIONID;
            this.UPDATEYR = x.UPDATEYR;
            this.REGION = x.REGION;
            this.TPRID = x.TPRID;
            this.TERRAIN = x.TERRAIN;
            this.ROUTESIGN = x.ROUTESIGN;
            this.ROUTESIGNQUAL = x.ROUTESIGNQUAL;
            this.GISID = x.GISID;
            this.PRIIRIDATE = x.PRIIRIDATE;

            // added 06/25/2014
            this.PRIYRREHAB = x.PRIYRREHAB != null ? x.PRIYRREHAB : "";
            this.ISDIVIDED = x.ISDIVIDED == true ? true : false;
            this.LRSROUTE = x.LRSROUTE != null ? x.LRSROUTE : "";
            this.FROMMEAS = x.FROMMEAS != null ? decimal.Parse(x.FROMMEAS.ToString()) : 0;
            this.TOMEAS = x.TOMEAS != null ? decimal.Parse(x.TOMEAS.ToString()) : 0;
            this.LastEditedAtUTC = x.LastEditedAtUTC != null ? DateTime.Parse(x.LastEditedAtUTC.ToString()) : new DateTime();

        }

        

    }
}