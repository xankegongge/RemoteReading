using System;
using System.Collections.Generic;
using System.Text;
using ESBasic;
using JustLib.Records;

namespace RemoteReading.Core
{
    /// <summary>
    /// 用于提供注册服务的Remoting接口。
    /// </summary>
    public interface IRemotingService :JustLib.Records.IChatRecordPersister
    {
        RegisterResult Register(GGUser user); 

        /// <summary>
        /// 根据ID或Name搜索用户【完全匹配】。
        /// </summary>   
        List<GGUser> SearchUser(string idOrName);
         List<Hospital> GetAllHospitals();

    }

    
}
