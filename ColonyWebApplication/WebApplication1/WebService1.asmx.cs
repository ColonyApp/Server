using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Transactions;
using System.Web.Script.Serialization;
using System.Web.Script.Services;

namespace WebApplication1
{
    public class test
    {
        public string name { get; set; }
    }
    /// <summary>
    /// Summary description for WebService1
    /// </summary>
    [WebService(Namespace = "http://colonywebappdb.azurewebsites.net/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class WebService1 : System.Web.Services.WebService
    {

        [WebMethod]
        [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
        public string HelloWorld()
        {
            test[] test = new test[]{ new test() { name = "Atsushi1"}, new test() { name = "Atsushi2"} };
            JavaScriptSerializer js = new JavaScriptSerializer();
            return js.Serialize(test);
        }

        [WebMethod]
        public String CreateUserId()
        {
            Guid userId = Guid.NewGuid();
            JavaScriptSerializer js = new JavaScriptSerializer();
            return js.Serialize(userId);
        }
        [WebMethod]
        public String CreateGroupId()
        {
            Guid groupId = Guid.NewGuid();
            JavaScriptSerializer js = new JavaScriptSerializer();
            return js.Serialize(groupId);
        }
        private bool CanInput(string targetData, int targetDataStingLenght)
        {
            if (string.IsNullOrEmpty(targetData))
            {
                return false;
            }
            if (string.IsNullOrWhiteSpace(targetData))
            {
                return false;
            }
            if(targetData.Length >= targetDataStingLenght)
            {
                return false;
            }
            return true;
        }
        [WebMethod]
        public bool CreateUser(string nickName, string mailAddress, string groupName)
        {
            try
            {
                /* 入力値チェック */
                if(!CanInput(nickName, 301)) { return false; }
                if(!CanInput(mailAddress, 301)) { return false; }
                if(!CanInput(groupName, 351)) { return false; }

                /* ユーザー作成時必須項目値生成 */
                var userId = Guid.NewGuid();
                var groupId = Guid.NewGuid();
                var insertDateTime = DateTime.Now;

                /* 新規作成対象データセット */
                UserTable ut = new UserTable
                {
                    Id = userId,
                    Nickname = nickName,
                    MailAddress = mailAddress,
                    IsLogicalDelete = false,
                    CreateUser = userId,
                    CreateDate = insertDateTime
                };
                GroupTable gt = new GroupTable
                {
                    GroupId = groupId,
                    GroupName = groupName,
                    IsLogicalDelete = false,
                    CreateUser = userId,
                    CreateDate = insertDateTime
                };
                UserGroupTable ugt = new UserGroupTable
                {
                    UserId = userId,
                    GroupId01 = groupId,
                    IsLogicalDelete = false,
                    CreateUser = userId,
                    CreateDate = insertDateTime
                };
                /* ユーザー新規追加 */
                using (TransactionScope t = new TransactionScope())
                {
                    using (DataClasses1DataContext c = new DataClasses1DataContext())
                    {
                        c.UserTables.InsertOnSubmit(ut);
                        c.GroupTables.InsertOnSubmit(gt);
                        c.UserGroupTables.InsertOnSubmit(ugt);
                        c.SubmitChanges();
                    }
                    t.Complete();
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
        [WebMethod]
        public bool IsExistsUser(string nickName, string mailAddress)
        {
            try
            {
                /* 入力値チェック */
                if (!CanInput(nickName, 301)) { return false; }
                if (!CanInput(mailAddress, 301)) { return false; }

                using (var transaction = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions
                {
                    IsolationLevel = IsolationLevel.ReadCommitted,
                    Timeout = TransactionManager.DefaultTimeout,
                })) {
                    using (DataClasses1DataContext c = new DataClasses1DataContext())
                    {
                        var query = from ut in c.UserTables
                                    where ut.Nickname == nickName
                                    where ut.MailAddress == mailAddress
                                    select ut.Id;
                        if (query.Count() == 1)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
        [WebMethod]
        public bool IsExistsGroup(string groupName)
        {
            try
            {
                /* 入力値チェック */
                if (!CanInput(groupName, 351)) { return false; }

                using (var transaction = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions
                {
                    IsolationLevel = IsolationLevel.ReadCommitted,
                    Timeout = TransactionManager.DefaultTimeout,
                }))
                {
                    using (DataClasses1DataContext c = new DataClasses1DataContext())
                    {
                        var query = from g in c.GroupTables
                                    where g.GroupName == groupName
                                    select g.GroupId;
                        if (query.Count() == 1)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
        [WebMethod]
        public bool IsExistsUserGroupChainByName(string nickname, string groupName01)
        {
            try
            {
                /* 入力値チェック */
                if (!CanInput(nickname, 301)) { return false; }
                if (!CanInput(groupName01, 351)) { return false; }

                using (var transaction = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions
                {
                    IsolationLevel = IsolationLevel.ReadCommitted,
                    Timeout = TransactionManager.DefaultTimeout,
                }))
                {
                    using (DataClasses1DataContext c = new DataClasses1DataContext())
                    {
                        var query = from u in c.UserTables
                                    join ug in c.UserGroupTables on u.Id equals ug.UserId
                                    join g01 in c.GroupTables on ug.GroupId01 equals g01.GroupId
                                    where u.Nickname == nickname
                                    where g01.GroupName == groupName01
                                    select u.Id;
                        if (query.Count() == 1)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
        [WebMethod]
        public bool IsExistsUserGroupChainById(Guid userId, Guid groupId)
        {
            try
            {
                using (var transaction = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions
                {
                    IsolationLevel = IsolationLevel.ReadCommitted,
                    Timeout = TransactionManager.DefaultTimeout,
                }))
                {
                    using (DataClasses1DataContext c = new DataClasses1DataContext())
                    {
                        var query = from u in c.UserTables
                                    join ug in c.UserGroupTables on u.Id equals ug.UserId
                                    join g01 in c.GroupTables on ug.GroupId01 equals g01.GroupId
                                    where u.Id == userId
                                    where g01.GroupId == groupId
                                    select u.Id;
                        if (query.Count() == 1)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
        [WebMethod]
        public bool ModifyNickName(Guid userId, string oldNickname, string newNickname)
        {
            try
            {
                /* 入力値チェック */
                if (!CanInput(oldNickname, 301)) { return false; }
                if (!CanInput(newNickname, 301)) { return false; }

                /* ユーザー修正 */
                using (TransactionScope t = new TransactionScope())
                {
                    using (DataClasses1DataContext c = new DataClasses1DataContext())
                    {
                        var query = from ut in c.UserTables
                                    where ut.Nickname == oldNickname
                                    where ut.Id == userId
                                    select ut;

                        if (query.Count() != 1) { return false; }

                        foreach (UserTable u in query)
                        {
                            u.Nickname = newNickname;
                        }
                        c.SubmitChanges();
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return false;
        }
        //* ・メアドからUserIDを取得
        [WebMethod]
        public String GetUserIdByMailAddress(String mailAddress)
        {
            try
            {
                string returnValue = string.Empty;
                using (var transaction = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions
                {
                    IsolationLevel = IsolationLevel.ReadCommitted,
                    Timeout = TransactionManager.DefaultTimeout,
                }))
                {
                    using (DataClasses1DataContext c = new DataClasses1DataContext())
                    {
                        var userTables = from u in c.UserTables
                                    where u.MailAddress == mailAddress
                                    select u;
                        if (userTables.Count() != 1) { return String.Empty; }
                        foreach(UserTable userTable in userTables)
                        {
                            returnValue = userTable.Id.ToString();
                        }
                    }
                }
                return returnValue;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return String.Empty;
            }

        }
        //* ・ニックネームからUserIdを取得
        [WebMethod]
        public String GetUserIdByNickName(String nickName)
        {
            try
            {
                string returnValue = string.Empty;
                using (var transaction = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions
                {
                    IsolationLevel = IsolationLevel.ReadCommitted,
                    Timeout = TransactionManager.DefaultTimeout,
                }))
                {
                    using (DataClasses1DataContext c = new DataClasses1DataContext())
                    {
                        var userTables = from u in c.UserTables
                                         where u.Nickname == nickName
                                         select u;
                        if (userTables.Count() != 1) { return String.Empty; }
                        foreach (UserTable userTable in userTables)
                        {
                            returnValue = userTable.Id.ToString();
                        }
                    }
                }
                return returnValue;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return String.Empty;
            }

        }
        //* ・グループ名からGroupIDを取得
        //* ・UserIDとGroupIDからそれらの関係性が正しいか判断
        //* ・TargetIDを取得（GUID）
        //* ・Want情報作成
        //* ・Want情報変更
        //* ・Want情報削除
        //* ・Want情報検索
        //* ・Get情報作成
        //* ・Get情報更新
        //* ・Get情報削除
        //* ・Get情報検索
        //* ・Give情報作成
        //* ・Give情報更新
        //* ・Give情報削除
        //* ・Give情報検索

    }
}
