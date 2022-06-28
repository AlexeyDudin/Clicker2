using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using static Clicker.src.Model.SeleniumParams;

namespace Clicker2.src.Control
{
    #region Конвертер Ip-адреса
    public class IpToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is ToSerializeIP)
                return ((ToSerializeIP)value).IPAddress.ToString();
            return "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            ToSerializeIP result = new ToSerializeIP();
            IPAddress resIp = IPAddress.Loopback;
            if (IPAddress.TryParse(value.ToString(), out resIp))
            {
                result.IPAddress = resIp;
                return result;
            }
            return null;
        }
    }
    #endregion

    #region Конвертер Ip-port
    public class PortToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is ToSerializePort)
                return ((ToSerializePort)value).IPEndPoint.Port;
            return "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return new IPEndPoint(IPAddress.Loopback, Int32.Parse(value.ToString()));
        }
    }
    #endregion
}
