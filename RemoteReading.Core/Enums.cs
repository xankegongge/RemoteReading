using System;
using System.Collections.Generic;
using System.Text;

namespace RemoteReading.Core
{
    /// <summary>
    /// 加好友结果
    /// </summary>
    public enum AddFriendResult
    {
        Succeed = 0,
        FriendNotExist,
    }
    /// <summary>
    /// 加入群结果
    /// </summary>
    public enum JoinGroupResult
    {
        Succeed = 0,
        GroupNotExist,
    }
    /// <summary>
    /// 创建群返回结果
    /// </summary>
    public enum CreateGroupResult
    {
        Succeed = 0,
        GroupExisted,
    }
    /// <summary>
    /// 用户类型
    /// </summary>
    public enum EUserType
    {
        Administrator = 0,//管理员
        Servicer,//客服人员
        NormalClient,//普通客户
        Expert//专家
    }

    public enum CheckType//客服审核类型，
    {
        None=0,
        Register=1,//注册类型
        Update,//更新

    }
   
    /// <summary>
    /// 专家与普通用户头衔
    /// </summary>
    public enum EProfessionTitle
    {
        医师 = 0,
        主治医师,//
        副主任医师,
        主任医师
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
    /// 标记类型
    /// </summary>
    public enum EDrawingType
    {
        Circle = 0,
        Rectangle = 1
    }
    /// <summary>
    /// 阅片状态
    /// </summary>
    public enum EEReadingStatus
    {
        UnProcessed = 0,
        Processing = 1,
        Rejected = 2,
        Completed = 3
    }

}