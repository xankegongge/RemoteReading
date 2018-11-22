using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using _CUSTOM_CONTROLS._ChatListBox;
using System.Drawing;
namespace RemoteReading
{
    
    class MedicalReadingComparer : IComparer<ChatListSubItem>
    {
        public int Compare(ChatListSubItem x,ChatListSubItem y)
        {
            DateTime mydate = DateTime.Parse(x.Time); DateTime otherdate = DateTime.Parse(y.Time);
            if (x.Status > y.Status)
            {
                return 1;

            }
            else if (x.Status < y.Status)
            {
                return -1;
            }
            else
            {
                return DateTime.Compare(otherdate,mydate );
            }
            
        }
    }
}
