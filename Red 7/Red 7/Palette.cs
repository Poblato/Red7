using System;
using System.Collections.Generic;
using System.Text;

namespace Red_7_v2._0
{
    class Palette : Hand
    {
        public Card GetCardByValue(int r, int c)
        {
            int index = -1;

            if (index == -1)
            {
                return -1;
            }
            else
            {
                return GetCardByIndex(index);
            }
        }
    }
}
