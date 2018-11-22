using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using ESBasic.ObjectManagement.Managers;
using ESBasic.Security;
using System.Configuration;
using ESBasic;
using System.Drawing;
using JustLib.Records;
using JustLib;
using System.Linq;
namespace GradeSystem.Server
{   
    public class GlobalCache 
    {
        private RealDB dbPersister ;
        private ObjectManager<string, LoginUser> userCache = new ObjectManager<string, LoginUser>(); // key:用户ID 。 Value：用户信息
        private ObjectManager<string, LoginUser> judgeCache = new ObjectManager<string, LoginUser>(); // key:用户ID 。 Value：用户信息
        private ObjectManager<string, LoginUser> studentCache = new ObjectManager<string, LoginUser>(); // key:用户ID 。 Value：用户信息
        private ObjectManager<string, LoginUser> assitantCache = new ObjectManager<string, LoginUser>(); // key:用户ID 。 Value：用户信息
       
        //private ObjectManager<int, SamplesType> sampleTypeCache = new ObjectManager<int, SamplesType>();//标本类型
        //private ObjectManager<int, DyedMethod> dyedmethodCache = new ObjectManager<int, DyedMethod>();//染色方法
        //private ObjectManager<int, Zoom> zoomCache = new ObjectManager<int, Zoom>();//标注放大倍数

        public GlobalCache(RealDB persister)
        {
            this.dbPersister = persister;
  
            foreach (LoginUser user in this.dbPersister.GetAllUser())//获取所有用户信息；
            {
                this.userCache.Add(user.UserID, user);
            }
            DictonarySortUser(userCache.ToDictionary());//按时间顺序排序用户；
            //从所有用户中挑选所有专家;
            foreach (LoginUser user in this.GetAllUser())//获取所有用户信息;
            {
                if(user.UserType==EUserType.Judge)
                    judgeCache.Add(user.UserID, user);
                else if (user.UserType == EUserType.Student)
                    studentCache.Add(user.UserID, user);
                else if (user.UserType == EUserType.Assistant)
                    assitantCache.Add(user.UserID, user);
            }
          
          //按时间顺序排序；
        }

        
        private void DictonarySortUser(Dictionary<string, LoginUser> dic)
        {
            var dicSort = from objDic in dic orderby objDic.Value.CreateTime descending select objDic;
            this.userCache.Clear();//清空
            foreach (KeyValuePair<string, LoginUser> kvp in dicSort)
            {
                userCache.Add(kvp.Key, kvp.Value);
            }

        }
        #region UserTable       

        /// <summary>
        /// 根据ID或Name搜索用户【完全匹配】。
        /// </summary>   
        public List<LoginUser> SearchUser(string idOrName)
        {
            List<LoginUser> list = new List<LoginUser>();
            foreach (LoginUser user in this.userCache.GetAllReadonly())
            {
                if (user.ID == idOrName || user.PersonName == idOrName)
                {
                    list.Add(user);
                }
            }
            return list;
        }

        /// <summary>
        /// 插入一个新用户。
        /// </summary>      
        public bool InsertUser(LoginUser user)
        {
            bool result = false;string friends=null;
            this.userCache.Add(user.UserID, user);
             DictonarySortUser(this.userCache.ToDictionary());
             result = true;
            
            return result;
        }
        

        public void UpdateUser(LoginUser user)
        {
            LoginUser old = this.userCache.Get(user.UserID);
            if (old == null)
            {
                return;
            }
            
          
            user.Version = old.Version + 1;//应该只是更新了自己的头像、昵称之类的
            user.activitedByUserID = old.activitedByUserID;//也更新下这个

            this.dbPersister.UpdateUser(user);
            this.userCache.Remove(old.UserID);//先移除，在添加
            this.userCache.Add(user.UserID, user);
            DictonarySortUser(this.userCache.ToDictionary());
        }       

        /// <summary>
        /// 目标帐号是否已经存在？
        /// </summary>    
        public bool IsUserExist(string userID)
        {
            return this.userCache.Contains(userID);
        }
        
        
        public bool IsExitMobilePhone(string mobilephone)
        {
            foreach (LoginUser user in userCache.GetAll())
            {
                if (user.UserContact.MobilePhone == mobilephone)
                {
                    return true;
                }

            }
            return false;
        }

        public bool IsExitEmailInCheckCache(string email)
        {
          
            foreach (LoginUser user in userCache.GetAll())
            {
                if (user.UserContact.Email == email)
                {
            
                    return true;
                }

            }
            return false;
        }
        public bool IsExitEmail(string email,out string userID)
        {
            userID = null;
            foreach (LoginUser user in userCache.GetAll())
            {
                if (user.UserContact.Email == email)
                {
                    
                    userID = user.UserID;
                    return true;
                }

            }
            return false;
        }
        /// <summary>
        /// 根据ID获取用户信息。
        /// </summary>        
        public LoginUser GetUser(string userID)
        {
            LoginUser onecache=this.userCache.Get(userID) ;
            if (onecache== null)
            {
                //通过向服务器查询看是否有这个用户
                LoginUser one=this.dbPersister.GetUser(userID);
                if (one != null)
                {
                    this.userCache.Add(one.UserID,one);
                    DictonarySortUser(this.userCache.ToDictionary());
                    return one;
                }
            }
            return onecache;
        } 

        public ChangePasswordResult ChangePassword(string userID, string oldPasswordMD5, string newPasswordMD5)
        {
            LoginUser user = this.userCache.Get(userID);
            if (user == null)
            {
                return ChangePasswordResult.UserNotExist;
            }

            if (user.PasswordMD5 != oldPasswordMD5)
            {
                return ChangePasswordResult.OldPasswordWrong;
            }
            
            user.PasswordMD5 = newPasswordMD5;

            this.dbPersister.UpdatePasswd(userID, newPasswordMD5);
            return ChangePasswordResult.Succeed;
        }
      
 
        
        #endregion

      

               
         public List<LoginUser> GetAllUser()
         {
             return this.userCache.GetAll();
         }

         public bool DeleteUser(string deluserid)
         {
             bool bresult = false;
             LoginUser user = this.userCache.Get(deluserid);
             
             if (user != null)
             {
                     if (this.dbPersister.DeleteUser(deluserid))//数据库删除成功后
                     {
                         userCache.Remove(deluserid);
                         bresult = true;
                     }

             }
             return bresult;
         }
       
         public  bool InsertAdvice(string userid, string advice)
         {
             return this.dbPersister.InsertAdvice(userid, advice);
         }
       
    }    
}
