using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OxyPlot;
using OxyPlot.Series;

namespace flightSimulator
{
    class TimeSeries
    {
        private List<KeyValuePair<string, List<float>>> arr2d_data;

        public int NumOfCols
        {
            get
            {
                return this.arr2d_data.Count;
            }
        }

        public int NumRows
        {
            get
            {
                return this.arr2d_data[0].Value.Count;
            }

        }
        private string ler;


        public TimeSeries(List<string> features)
        {
            this.arr2d_data = new List<KeyValuePair<string, List<float>>>();
            foreach (string name in features)
            {
                this.arr2d_data.Add(new KeyValuePair<string, List<float>>(name, new List<float>()));
            }
        }


        public void AddSingleRow(string row)
        {
            string[] tokens = row.Split(',');
            for (int i = 0; i < this.NumOfCols; i++)
            {
                this.arr2d_data[i].Value.Add(float.Parse(tokens[i]));
            }
        }


        public List<string> GetFeatures()
        {
            List<string> temp = new List<string>();
            foreach (var arr2 in this.arr2d_data)
            {
                temp.Add(arr2.Key);
            }
            return temp;
        }

        public List<float> GetColByName(string feature)
        {
            List<float> temp = new List<float>();
            for (int i = 0; i < NumOfCols; i++)
            {
                if (arr2d_data[i].Key.Equals(feature))
                {
                    temp = arr2d_data[i].Value;
                }
            }
            return temp;
        }
        // or:
        // return this.arr2d_data.Find(x=>x.Key.Equals(feature)).Value;

        public List<float> GetColByIndex(int index)
        {
            return this.arr2d_data[index].Value;
        }

        public string GetFeatureByIndex(int index)
        {
            return this.arr2d_data[index].Key;
        }
        float avg(float[] x)
        {
            float sum = 0;
            for (int i = 0; i < NumRows; sum += x[i], i++) ;
            return sum / NumRows;
        }

        // returns the variance of X and Y
        float var(float[] x)
        {
            float av = avg(x);
            float sum = 0;
            for (int i = 0; i < NumRows; i++)
            {
                sum += x[i] * x[i];
            }
            return sum / NumRows - av * av;
        }

        // returns the covariance of X and Y
        float cov(float[] x, float[] y)
        {
            float sum = 0;
            for (int i = 0; i < NumRows; i++)
            {
                sum += x[i] * y[i];
            }
            sum /= NumRows;

            return sum - avg(x) * avg(y);
        }


        // returns the Pearson correlation coefficient of X and Y
        float pearson(float[] x, float[] y)
        {

            float pe = (float)(Math.Sqrt(var(x)) * Math.Sqrt(var(y)));
            if (pe == 0)
                return 0;
            return (float)(cov(x, y) / pe);
        }

        public string learnNormal(string feature)
        {
            string corelative_name = "";
            float max = 0;
            List<float> val_i = GetColByName(feature);

            for (int j = 0; j < NumOfCols; j++)
            {
                if (!(arr2d_data[j].Key.Equals(feature)))
                {
                    List<float> val_j = this.arr2d_data[j].Value;

                    float corletive = Math.Abs(pearson(val_i.ToArray(), val_j.ToArray()));
                    if (corletive >= max)
                    {
                        corelative_name = arr2d_data[j].Key;
                        max = corletive;
                    }
                }
            }
            ler = corelative_name;
            return corelative_name;
        }

        public List<DataPoint> linearReg(List<float> f1, List<float> f2)
        {
            // performs a linear regression and returns the line equation
            float[] arr_x = f1.ToArray();
            float[] arr_y = f2.ToArray();
            float a;
            if (var(arr_x) != 0) { a = cov(arr_x, arr_y) / var(arr_x); }
            else { a = 0; }
            float b = avg(arr_y) - a * (avg(arr_x));
            float xMax = f1.Max();
            float xMin = f1.Min();
            List<DataPoint> tmp = new List<DataPoint>();
            tmp.Add(new DataPoint(xMin, (a * xMin + b)));
            tmp.Add(new DataPoint(xMax, (a * xMax + b)));
            return tmp;
        }
    }
}