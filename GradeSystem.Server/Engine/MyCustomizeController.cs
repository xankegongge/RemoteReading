using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ESBasic;
using ESBasic.ObjectManagement.Managers;
namespace GradeSystem.Server
{
    public class MyCustomizeController:ICustomizeController
    {
        private HPSocketCS.TcpServer server;
         private ObjectManager<string, UserData> onLineList;
        public MyCustomizeController(HPSocketCS.TcpServer server,ObjectManager<string, UserData> onlist)
        {
            this.server = server;
            this.onLineList = onlist;
        }
        //
        // 摘要:
        //     向ID为userID的在线用户发送信息。如果用户不在线，则直接返回。
        //
        // 参数:
        //   userID:
        //     接收消息的用户ID
        //
        //   informationType:
        //     自定义信息类型
        //
        //   info:
        //     信息
        public void Send(string userID, int informationType, byte[] info)
        {
            SendBlob(userID, informationType, info, 1024);
        }
        

        //
        // 摘要:
        //     向ID为userID的在线用户发送大数据块信息。直到数据发送完毕，该方法才会返回。如果担心长时间阻塞调用线程，可考虑异步调用本方法。
        //
        // 参数:
        //   userID:
        //     接收消息的用户ID
        //
        //   informationType:
        //     自定义信息类型
        //
        //   blobInfo:
        //     大数据块信息
        //
        //   fragmentSize:
        //     分片传递时，片段的大小
        public void SendBlob(string userID, int informationType, byte[] info, int fragmentSize)
        {
           
            if (userID == null)
            {
                return;
            }
            UserData ud = onLineList.Get(userID);
            if (ud == null) return;
            try
            {
                ByteBuf write = ByteBuf.Allocate(1024);//写入内容;默认缓存区大小2M;
                write.AutoExpand = true;
                if (write.CanWrite)
                {
                    int len = 0;
                    write.WriteReverseInt(len);//数据总长度;
                    len += 4;

                    len += 4;
                    write.WriteReverseInt(informationType);//写入类型;
                    if (info == null)
                    {
                        len += 4;
                        write.WriteReverseInt(-1);
                    }
                    else
                    {
                        len += info.Length;//对象数据流长度;
                        write.Write(info, 0, info.Length);//附加对象字节流;
                    }
                    //写入tag为null;
                    write.WriteReverseInt(-1);

                    write.Rewind();//指针移植到最顶端;
                    write.WriteReverseInt(len);//重新写入总长度;
                    // 
                    byte[] des = write.ToArray();

                    //server.Send(ud.ConnID, des, des.Length);//发送字节流;
                    int sendtimes = 0;//发送次数;
                    int lastSize = 0;//最后一次发送字节数;
                    sendtimes = des.Length / fragmentSize;
                    lastSize = des.Length % fragmentSize;
                    if (lastSize > 0)
                    {
                        sendtimes += 1;
                    }
                    else
                        lastSize = fragmentSize;
                    if (sendtimes == 1)
                    {
                        server.Send(ud.ConnID, des, des.Length);//发送字节流;
                    }
                    else
                    {
                        for (int i = 0; i < sendtimes; i++)
                        {
                            byte[] array = (i == sendtimes - 1) ? new byte[lastSize] : new byte[fragmentSize];
                            Array.Copy(des, 0, array, 0, array.Length);//复制数据流;
                            server.Send(ud.ConnID, array, array.Length);
                        }
                    }

                }


            }
            catch (Exception ex)
            {
            }
        }
    }
}
