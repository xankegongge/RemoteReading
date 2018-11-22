using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GradeSystem.Server
{
    class SerializeHelper
    {
         public static string ReadStrIntLen(ByteBuf buffer)  
         {
             byte[] arrayOfByte;
             try
             {
                 int len = buffer.ReadReverseInt();
                 if (len <= 0)
                     return "";
               //  arrayOfByte = new byte[len];
                arrayOfByte= buffer.ReadBytes(len);
             }
             catch (Exception ex)
             {
                 return null;
             }
            return System.Text.Encoding.UTF8.GetString(arrayOfByte);
        }
    }
}
