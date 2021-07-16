using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace flightSimulator
{
    interface IClient
    {
        bool Connect(string IP, int port);
        void closeConnection();

        //void sendCSV(string path);
        List<String> parseXML(string path);
    }
}
