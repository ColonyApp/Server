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
            //Context.Response.Clear();
            //Context.Response.ContentType = "application/json";
            //Context.Response.Write(js.Serialize(test));
        }

        [WebMethod]
        public Guid CreateUserId()
        {
            return Guid.NewGuid();
        }
        [WebMethod]
        public Guid CreateGroupId()
        {
            return Guid.NewGuid();
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

                        //join g02 in c.GroupTables on ug.GroupId02 equals g02.GroupId
                        //join g03 in c.GroupTables on ug.GroupId03 equals g03.GroupId
                        //join g04 in c.GroupTables on ug.GroupId04 equals g04.GroupId
                        //join g05 in c.GroupTables on ug.GroupId05 equals g05.GroupId
                        //join g06 in c.GroupTables on ug.GroupId06 equals g06.GroupId
                        //join g07 in c.GroupTables on ug.GroupId07 equals g07.GroupId
                        //join g08 in c.GroupTables on ug.GroupId08 equals g08.GroupId
                        //join g09 in c.GroupTables on ug.GroupId09 equals g09.GroupId
                        //join g10 in c.GroupTables on ug.GroupId10 equals g10.GroupId
                        //join g11 in c.GroupTables on ug.GroupId11 equals g11.GroupId
                        //join g12 in c.GroupTables on ug.GroupId12 equals g12.GroupId
                        //join g13 in c.GroupTables on ug.GroupId13 equals g13.GroupId
                        //join g14 in c.GroupTables on ug.GroupId14 equals g14.GroupId
                        //join g15 in c.GroupTables on ug.GroupId15 equals g15.GroupId
                        //join g16 in c.GroupTables on ug.GroupId16 equals g16.GroupId
                        //join g17 in c.GroupTables on ug.GroupId17 equals g17.GroupId
                        //join g18 in c.GroupTables on ug.GroupId18 equals g18.GroupId
                        //join g19 in c.GroupTables on ug.GroupId19 equals g19.GroupId
                        //join g20 in c.GroupTables on ug.GroupId20 equals g20.GroupId
                        //join g21 in c.GroupTables on ug.GroupId21 equals g21.GroupId
                        //join g22 in c.GroupTables on ug.GroupId22 equals g22.GroupId
                        //join g23 in c.GroupTables on ug.GroupId23 equals g23.GroupId
                        //join g24 in c.GroupTables on ug.GroupId24 equals g24.GroupId
                        //join g25 in c.GroupTables on ug.GroupId25 equals g25.GroupId
                        //join g26 in c.GroupTables on ug.GroupId26 equals g26.GroupId
                        //join g27 in c.GroupTables on ug.GroupId27 equals g27.GroupId
                        //join g28 in c.GroupTables on ug.GroupId28 equals g28.GroupId
                        //join g29 in c.GroupTables on ug.GroupId29 equals g29.GroupId
                        //join g30 in c.GroupTables on ug.GroupId30 equals g30.GroupId
                        //join g31 in c.GroupTables on ug.GroupId31 equals g31.GroupId
                        //join g32 in c.GroupTables on ug.GroupId32 equals g32.GroupId
                        //join g33 in c.GroupTables on ug.GroupId33 equals g33.GroupId
                        //join g34 in c.GroupTables on ug.GroupId34 equals g34.GroupId
                        //join g35 in c.GroupTables on ug.GroupId35 equals g35.GroupId
                        //join g36 in c.GroupTables on ug.GroupId36 equals g36.GroupId
                        //join g37 in c.GroupTables on ug.GroupId37 equals g37.GroupId
                        //join g38 in c.GroupTables on ug.GroupId38 equals g38.GroupId
                        //join g39 in c.GroupTables on ug.GroupId39 equals g39.GroupId
                        //join g40 in c.GroupTables on ug.GroupId40 equals g40.GroupId
                        //join g41 in c.GroupTables on ug.GroupId41 equals g41.GroupId
                        //join g42 in c.GroupTables on ug.GroupId42 equals g42.GroupId
                        //join g43 in c.GroupTables on ug.GroupId43 equals g43.GroupId
                        //join g44 in c.GroupTables on ug.GroupId44 equals g44.GroupId
                        //join g45 in c.GroupTables on ug.GroupId45 equals g45.GroupId
                        //join g46 in c.GroupTables on ug.GroupId46 equals g46.GroupId
                        //join g47 in c.GroupTables on ug.GroupId47 equals g47.GroupId
                        //join g48 in c.GroupTables on ug.GroupId48 equals g48.GroupId
                        //join g49 in c.GroupTables on ug.GroupId49 equals g49.GroupId
                        //join g50 in c.GroupTables on ug.GroupId50 equals g50.GroupId
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

                        //join g02 in c.GroupTables on ug.GroupId02 equals g02.GroupId
                        //join g03 in c.GroupTables on ug.GroupId03 equals g03.GroupId
                        //join g04 in c.GroupTables on ug.GroupId04 equals g04.GroupId
                        //join g05 in c.GroupTables on ug.GroupId05 equals g05.GroupId
                        //join g06 in c.GroupTables on ug.GroupId06 equals g06.GroupId
                        //join g07 in c.GroupTables on ug.GroupId07 equals g07.GroupId
                        //join g08 in c.GroupTables on ug.GroupId08 equals g08.GroupId
                        //join g09 in c.GroupTables on ug.GroupId09 equals g09.GroupId
                        //join g10 in c.GroupTables on ug.GroupId10 equals g10.GroupId
                        //join g11 in c.GroupTables on ug.GroupId11 equals g11.GroupId
                        //join g12 in c.GroupTables on ug.GroupId12 equals g12.GroupId
                        //join g13 in c.GroupTables on ug.GroupId13 equals g13.GroupId
                        //join g14 in c.GroupTables on ug.GroupId14 equals g14.GroupId
                        //join g15 in c.GroupTables on ug.GroupId15 equals g15.GroupId
                        //join g16 in c.GroupTables on ug.GroupId16 equals g16.GroupId
                        //join g17 in c.GroupTables on ug.GroupId17 equals g17.GroupId
                        //join g18 in c.GroupTables on ug.GroupId18 equals g18.GroupId
                        //join g19 in c.GroupTables on ug.GroupId19 equals g19.GroupId
                        //join g20 in c.GroupTables on ug.GroupId20 equals g20.GroupId
                        //join g21 in c.GroupTables on ug.GroupId21 equals g21.GroupId
                        //join g22 in c.GroupTables on ug.GroupId22 equals g22.GroupId
                        //join g23 in c.GroupTables on ug.GroupId23 equals g23.GroupId
                        //join g24 in c.GroupTables on ug.GroupId24 equals g24.GroupId
                        //join g25 in c.GroupTables on ug.GroupId25 equals g25.GroupId
                        //join g26 in c.GroupTables on ug.GroupId26 equals g26.GroupId
                        //join g27 in c.GroupTables on ug.GroupId27 equals g27.GroupId
                        //join g28 in c.GroupTables on ug.GroupId28 equals g28.GroupId
                        //join g29 in c.GroupTables on ug.GroupId29 equals g29.GroupId
                        //join g30 in c.GroupTables on ug.GroupId30 equals g30.GroupId
                        //join g31 in c.GroupTables on ug.GroupId31 equals g31.GroupId
                        //join g32 in c.GroupTables on ug.GroupId32 equals g32.GroupId
                        //join g33 in c.GroupTables on ug.GroupId33 equals g33.GroupId
                        //join g34 in c.GroupTables on ug.GroupId34 equals g34.GroupId
                        //join g35 in c.GroupTables on ug.GroupId35 equals g35.GroupId
                        //join g36 in c.GroupTables on ug.GroupId36 equals g36.GroupId
                        //join g37 in c.GroupTables on ug.GroupId37 equals g37.GroupId
                        //join g38 in c.GroupTables on ug.GroupId38 equals g38.GroupId
                        //join g39 in c.GroupTables on ug.GroupId39 equals g39.GroupId
                        //join g40 in c.GroupTables on ug.GroupId40 equals g40.GroupId
                        //join g41 in c.GroupTables on ug.GroupId41 equals g41.GroupId
                        //join g42 in c.GroupTables on ug.GroupId42 equals g42.GroupId
                        //join g43 in c.GroupTables on ug.GroupId43 equals g43.GroupId
                        //join g44 in c.GroupTables on ug.GroupId44 equals g44.GroupId
                        //join g45 in c.GroupTables on ug.GroupId45 equals g45.GroupId
                        //join g46 in c.GroupTables on ug.GroupId46 equals g46.GroupId
                        //join g47 in c.GroupTables on ug.GroupId47 equals g47.GroupId
                        //join g48 in c.GroupTables on ug.GroupId48 equals g48.GroupId
                        //join g49 in c.GroupTables on ug.GroupId49 equals g49.GroupId
                        //join g50 in c.GroupTables on ug.GroupId50 equals g50.GroupId
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

    }
}
