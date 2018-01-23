using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ETABS2016;

namespace MeshTool
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MT etabsModel;
        public List<EtabsAreaObject> Parents = new List<EtabsAreaObject>();
        public List<EtabsAreaObject> Children = new List<EtabsAreaObject>();
        public MainWindow()
        {
            InitializeComponent();
            try
            {
                this.etabsModel = new MT();
                this.label.Content = "Mesh Tool will run on " + etabsModel.modelName + " Model";
            }
            
            catch(Exception)
            {
                this.label.Content = "Please Open an ETABS2016 Model and re-start this application";
            }
            if( etabsModel != null)
            {
                int ret;
                int areaCount = new Int32();
                int[] objectType = null;
                string[] areaNames = null;
                ret = this.etabsModel.activeModel.AreaObj.SetSelected("ALL", true, ETABS2016.eItemType.Group);
                ret = this.etabsModel.activeModel.SelectObj.GetSelected(ref areaCount, ref objectType, ref areaNames);

                foreach (string name in areaNames)
                {
                    EtabsAreaObject parent = EatParent(name, this.etabsModel.activeModel);
                    if(Parents == null)
                    {
                        List<EtabsAreaObject> first = new List<EtabsAreaObject>();
                        first.Add(parent);
                        Parents = first;
                    }
                    else { Parents.Add(parent); }                    
                }

                foreach(EtabsAreaObject area in Parents)
                {
                    string statement = area.Name + " - ";
                    foreach(EtabsPoint point in area.Points)
                    {
                        statement += "( ";
                        statement += point.X.ToString() + ", ";
                        statement += point.Y.ToString() + ", ";
                        statement += point.Z.ToString() + ")";
                    }
                    listBox.Items.Add(statement);                        
                }

            }


        }

        private EtabsAreaObject EatParent(string name, cSapModel activeModel)
        {
            EtabsAreaObject area = new EtabsAreaObject();
            int pointCount = new Int32();
            string[] pointNames = null;
            activeModel.AreaObj.GetPoints(name, ref pointCount, ref pointNames);
            List<EtabsPoint> points = new List<EtabsPoint>();
            foreach(string pointName in pointNames)
            {
                double x = new double();
                double y = new double();
                double z = new double();
                activeModel.PointObj.GetCoordCartesian(pointName, ref x, ref y, ref z);
                EtabsPoint point = new EtabsPoint();
                point.X = x;
                point.Y = y;
                point.Z = z;
                points.Add(point);
            }
            area.Name = name;
            area.Points = points;
            return area;
        }
    }
}
