using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GradeSystem.Server
{
        // 摘要:
        //     客户端设备的类型。
        public enum ClientType
        {
            DotNET = 0,
            Silverlight = 1,
            WindowsPhone = 2,
            WebSocket = 3,
            IOS = 4,
            Android = 5,
            Xamarin = 6,
            Others = 8,
        }
        public enum AppState
        {
            Starting, Started, Stoping, Stoped, Error
        }
        // 摘要:
        //     客户端连接断开的原因分类。
        public enum DisconnectedType
        {
            // 摘要:
            //     网络连接中断。
            NetworkInterrupted = 0,
            //
            // 摘要:
            //     无效的消息。
            InvalidMessage = 1,
            //
            // 摘要:
            //     消息中的UserID与当前连接的OwnerID不一致。
            MessageWithWrongUserID = 2,
            //
            // 摘要:
            //     心跳超时。
            HeartBeatTimeout = 3,
            //
            // 摘要:
            //     被同名用户挤掉线。（发生于RelogonMode为ReplaceOld）
            BeingPushedOut = 4,
            //
            // 摘要:
            //     当已经有同名用户在线时，新的连接被忽略。（发生于RelogonMode为IgnoreNew）
            NewConnectionIgnored = 5,
            //
            // 摘要:
            //     等待发送以及正在发送的消息个数超过了MaxChannelCacheSize的设定值。
            ChannelCacheOverflow = 6,
            //
            // 摘要:
            //     未授权的客户端类型
            UnauthorizedClientType = 7,
            //
            // 摘要:
            //     已达到最大连接数限制
            MaxConnectionCountLimitted = 8,
        }
        public enum RelogonMode
        {
            // 摘要:
            //     忽略新的连接。
            IgnoreNew = 0,
            //
            // 摘要:
            //     使用新的连接取代旧的连接。
            ReplaceOld = 1,
        }

        /// <summary>
        /// 专家与普通用户头衔
        /// </summary>
        public enum EProfessionTitle
        {
            助教 = 0,
            讲师,//
            副教授,
            教授
        }
        /// <summary>
        /// 注册结果
        /// </summary>
        public enum RegisterResult
        {
            /// <summary>
            /// 成功
            /// </summary>
            Succeed = 0,

            /// <summary>
            /// 帐号已经存在
            /// </summary>
            Existed,

            /// <summary>
            /// email已经存在
            /// </summary>
            EmailExisted,

            MobileExisted,
            /// <summary>
            /// 过程中出现错误
            /// </summary>

            Error
        }
        /// <summary>
        /// 更改密码结果
        /// </summary>
        public enum ChangePasswordResult
        {
            Succeed = 0,
            OldPasswordWrong,
            UserNotExist
        }
        /// <summary>
        /// 用户类型
        /// </summary>
        public enum EUserType
        {
            Administrator = 0,//管理员
            Judge,//客服人员
            Student,//普通客户
            Assistant//专家
        }
        public enum LogonResult
        {
            Succeed=0,
            Failed=1,
            HadLoggedOn=2,
            VersionMismatched=3
        }
}
