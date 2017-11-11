using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Data;
using System.Globalization;


namespace AigisTimer.Views.Converters
{
    class HexConverter
    {

        public object Convert(object value, Type type, object parameter, CultureInfo culture)
        {
            int v = (int)value;
            return string.Format("0x{0:X2}", v);
        }

        public object ConvertBack(object value, Type type, object parameter, CultureInfo culture)
        {
            string s = (string)value;
            int v;
            if (int.TryParse(s, NumberStyles.AllowHexSpecifier, null, out v))
            {
                return v;
            }
            return 0;
        }

    }
}
