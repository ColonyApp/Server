using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Transactions;

namespace ColonyWebApplication
{
    /// <summary>
    /// Summary description for WebService1
    /// </summary>
    [WebService(Namespace = "http://colonywebservices-test.ap-northeast-1.elasticbeanstalk.com")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class WebService1 : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
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
        [WebMethod]
        public bool CreateUser(string nickName, string mailAddress, string groupName)
        {
            try
            {
                /* 入力値チェック */
                if (string.IsNullOrEmpty(nickName)) { return false; }
                if (string.IsNullOrWhiteSpace(nickName)) { return false; }
                if (string.IsNullOrEmpty(mailAddress)) { return false; }
                if (string.IsNullOrWhiteSpace(mailAddress)) { return false; }
                if (string.IsNullOrEmpty(groupName)) { return false; }
                if (string.IsNullOrWhiteSpace(groupName)) { return false; }

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
            //int count = (from )
            return false;
        }
        [WebMethod]
        public bool IsExistsGroup(string groupName)
        {
            return false;
        }
        [WebMethod]
        public bool IsExistsUserGroupChain(Guid userId, Guid groupId)
        {
            return false;
        }
        [WebMethod]
        public bool ModifyNickName(Guid userId, string oldNickname, string newNickname)
        {
            try
            {
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
