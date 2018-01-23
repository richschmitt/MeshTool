using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ETABS2016;

namespace MeshTool
{
    public class MT
    {
        public cOAPI applicationObject;   
        public cSapModel activeModel;
        public cHelper helper;
        public string modelName;

        public void Helper()
        {
            this.helper = new Helper();
        }

        public void ApplicationObject(cHelper helper)
        {
            this.applicationObject = (cOAPI)helper.GetObject("CSI.ETABS.API.ETABSObject");
        }

        public void ModelObject(cOAPI applicationObject)
        {
            this.activeModel = applicationObject.SapModel;
        }
        public MT()
        {
            this.Helper();
            this.ApplicationObject(this.helper);
            this.ModelObject(this.applicationObject);
            this.modelName = this.activeModel.GetModelFilename(false);
        }
    }

    public class EtabsAreaObject
    {
        public string Name { get; set; }
        public string Label { get; set; }
        public List<EtabsPoint> Points { get; set; }        


    }

    public class EtabsPoint
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }

    }

}
