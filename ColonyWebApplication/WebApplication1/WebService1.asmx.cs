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
    #region For HelloWorld class(sample)
    /// <summary>
    /// For HelloWorld class(sample)
    /// </summary>
    public class test
    {
        public string name { get; set; }
    }
    #endregion

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
        #region HelloWord
        /// <summary>
        /// HelloWord
        /// </summary>
        [WebMethod]
        [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
        public void HelloWorld()
        {
            test[] test = new test[]{ new test() { name = "HelloWord1" }, new test() { name = "HelloWord2" } };
            JavaScriptSerializer js = new JavaScriptSerializer();
            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            Context.Response.Write(js.Serialize(test));
        }
        #endregion

        #region UserId 生成
        /// <summary>
        /// UserId 生成
        /// </summary>
        [WebMethod]
        [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
        public void CreateUserId()
        {
            Guid userId = Guid.NewGuid();
            JavaScriptSerializer js = new JavaScriptSerializer();
            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            Context.Response.Write(js.Serialize(userId));
        }
        #endregion

        #region グループID生成
        /// <summary>
        /// グループID生成
        /// </summary>
        [WebMethod]
        [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
        public void CreateGroupId()
        {
            Guid groupId = Guid.NewGuid();
            JavaScriptSerializer js = new JavaScriptSerializer();
            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            Context.Response.Write(js.Serialize(groupId));
        }
        #endregion

        #region ユーザー新規作成
        /// <summary>
        /// ユーザー新規作成（初めの１回のみ使用する）
        /// </summary>
        /// <param name="nickName">ニックネーム</param>
        /// <param name="mailAddress">メールアドレス</param>
        /// <param name="groupName">グループ名</param>
        [WebMethod]
        [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
        public void CreateUser(string nickName, string mailAddress, string groupName)
        {
            bool judgement = false;
            try
            {
                /* 入力値チェック（必須チェック） */
                judgement = CanInput(nickName, 301);
                if (judgement)
                {
                    judgement = CanInput(mailAddress, 301);
                    if (judgement)
                    {
                        judgement = CanInput(groupName, 351);
                    }
                }
                if (judgement)
                {
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
                    judgement = true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                judgement = false;
            }
            finally
            {
                /* 最終的には judgement の値を返す */
                JavaScriptSerializer js = new JavaScriptSerializer();
                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                Context.Response.Write(js.Serialize(judgement));
            }
        }
        #endregion

        #region ユーザーが存在するか確認
        /// <summary>
        /// ユーザーが存在するか確認
        /// </summary>
        /// <param name="nickName">ニックネーム</param>
        /// <param name="mailAddress">メールアドレス</param>
        [WebMethod]
        [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
        public void IsExistsUser(string nickName, string mailAddress)
        {
            bool judgement = false;
            try
            {
                /* 入力値チェック */
                judgement = CanInput(nickName, 301);
                if (judgement)
                {
                    judgement = CanInput(mailAddress, 301);
                }
                /* ユーザー存在チェック */
                if (judgement)
                {
                    //検索操作はダーティーリードOKay
                    using (var transaction = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions
                    {
                        IsolationLevel = IsolationLevel.ReadUncommitted,
                        Timeout = TransactionManager.DefaultTimeout,
                    }))
                    {
                        using (DataClasses1DataContext c = new DataClasses1DataContext())
                        {
                            //ニックネームとメールアドレスはAnd条件と論理削除されていないデータ
                            var query = from ut in c.UserTables
                                        where ut.Nickname == nickName
                                        where ut.MailAddress == mailAddress
                                        where ut.IsLogicalDelete == false
                                        select ut.Id;
                            //１件でなければならない
                            if (query.Count() != 1)
                            {
                                judgement = false;
                            }
                        }
                    }
                    judgement = true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                judgement = false;
            }
            finally
            {
                /* 最終的には judgement の値を返す */
                JavaScriptSerializer js = new JavaScriptSerializer();
                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                Context.Response.Write(js.Serialize(judgement));
            }
        }
        #endregion

        #region グループが存在するか確認
        /// <summary>
        /// グループが存在するか確認
        /// </summary>
        /// <param name="groupName">グループ名</param>
        [WebMethod]
        [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
        public void IsExistsGroup(string groupName)
        {
            bool judgement = false;
            try
            {
                /* 入力値チェック */
                judgement = CanInput(groupName, 351);
                if (judgement)
                {
                    using (var transaction = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions
                    {
                        IsolationLevel = IsolationLevel.ReadUncommitted,
                        Timeout = TransactionManager.DefaultTimeout,
                    }))
                    {
                        using (DataClasses1DataContext c = new DataClasses1DataContext())
                        {
                            var query = from g in c.GroupTables
                                              where g.GroupName == groupName
                                              where g.IsLogicalDelete == false
                                              select g.GroupId;
                            if (query.Count() != 1) { judgement = false; }
                        }
                    }
                    judgement = true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                judgement = false;
            }
            finally
            {
                /* 最終的には judgement の値を返す */
                JavaScriptSerializer js = new JavaScriptSerializer();
                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                Context.Response.Write(js.Serialize(judgement));
            }
        }
        #endregion

        #region ニックネームとグループ名が紐づくか確認する
        /// <summary>
        /// ニックネームとグループ名が紐づくか確認する
        /// </summary>
        /// <param name="nickname">ニックネーム</param>
        /// <param name="groupName01">グループ１</param>
        [WebMethod]
        [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
        public void IsExistsUserGroupChainByName(string nickname, string groupName01)
        {
            bool judgement = false;
            try
            {
                /* 入力値チェック */
                judgement = CanInput(nickname, 301);
                if (judgement)
                {
                    judgement = CanInput(groupName01, 351);
                }
                if (judgement)
                {
                    using (var transaction = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions
                    {
                        IsolationLevel = IsolationLevel.ReadUncommitted,
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
                                        where u.IsLogicalDelete == false
                                        where ug.IsLogicalDelete == false
                                        where g01.IsLogicalDelete == false
                                        select u.Id;
                            if (query.Count() != 1)
                            {
                                judgement = false;
                            }
                        }
                    }
                    judgement = true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                judgement = false;
            }
            finally
            {
                /* 最終的には judgement の値を返す */
                JavaScriptSerializer js = new JavaScriptSerializer();
                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                Context.Response.Write(js.Serialize(judgement));
            }
        }
        #endregion

        #region ユーザーIDとグループIDが紐づくか確認する
        /// <summary>
        /// ユーザーIDとグループIDが紐づくか確認する 
        /// </summary>
        /// <param name="userId">ユーザーID</param>
        /// <param name="groupId">グループID</param>
        [WebMethod]
        [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
        public void IsExistsUserGroupChainById(Guid userId, Guid groupId)
        {
            bool judgement = true;
            try
            {
                Guid p = new Guid();
                if (!Guid.TryParse(userId.ToString(), out p)) { judgement = false; }
                p = new Guid();
                if (!Guid.TryParse(groupId.ToString(), out p) & judgement == true) { judgement = false; }
                if (judgement)
                {
                    using (var transaction = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions
                    {
                        IsolationLevel = IsolationLevel.ReadUncommitted,
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
                                        where u.IsLogicalDelete == false
                                        where ug.IsLogicalDelete == false
                                        where g01.IsLogicalDelete == false
                                        select u.Id;
                            if (query.Count() != 1)
                            {
                                judgement = false;
                            }
                            judgement = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                judgement = false;
            }
            finally
            {
                /* 最終的には judgement の値を返す */
                JavaScriptSerializer js = new JavaScriptSerializer();
                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                Context.Response.Write(js.Serialize(judgement));
            }
        }
        #endregion

        #region ニックネーム変更
        /// <summary>
        /// ニックネーム変更
        /// </summary>
        /// <param name="userId">ユーザーID</param>
        /// <param name="oldNickname">修正前のニックネーム</param>
        /// <param name="newNickname">修正後のニックネーム</param>
        [WebMethod]
        [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
        public void ModifyNickName(Guid userId, string oldNickname, string newNickname)
        {
            bool judgement = false;
            try
            {
                /* 入力値チェック */
                Guid p = new Guid();
                if (!Guid.TryParse(userId.ToString(), out p)) { judgement = false; }
                if (judgement)
                {
                    judgement = CanInput(oldNickname, 301);
                    if (judgement)
                    {
                        judgement = CanInput(newNickname, 301);
                    }
                }
                if (judgement)
                {
                    /* ユーザー修正 */
                    using (TransactionScope t = new TransactionScope())
                    {
                        using (DataClasses1DataContext c = new DataClasses1DataContext())
                        {
                            var query = from ut in c.UserTables
                                        where ut.Nickname == oldNickname
                                        where ut.Id == userId
                                        where ut.IsLogicalDelete == false
                                        select ut;

                            if (query.Count() != 1) { judgement = false; }

                            foreach (UserTable u in query)
                            {
                                u.Nickname = newNickname;
                            }
                            c.SubmitChanges();
                            judgement = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                judgement = false;
            }
            finally
            {
                /* 最終的には judgement の値を返す */
                JavaScriptSerializer js = new JavaScriptSerializer();
                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                Context.Response.Write(js.Serialize(judgement));
            }
        }
        #endregion

        //* ・メアドからUserIDを取得
        [WebMethod]
        [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
        public String GetUserIdByMailAddress(String mailAddress)
        {
            try
            {
                string returnValue = string.Empty;
                using (var transaction = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions
                {
                    IsolationLevel = IsolationLevel.ReadUncommitted,
                    Timeout = TransactionManager.DefaultTimeout,
                }))
                {
                    using (DataClasses1DataContext c = new DataClasses1DataContext())
                    {
                        var userTables = from u in c.UserTables
                                    where u.MailAddress == mailAddress
                                    where u.IsLogicalDelete == false
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
        [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
        public String GetUserIdByNickName(String nickName)
        {
            try
            {
                string returnValue = string.Empty;
                using (var transaction = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions
                {
                    IsolationLevel = IsolationLevel.ReadUncommitted,
                    Timeout = TransactionManager.DefaultTimeout,
                }))
                {
                    using (DataClasses1DataContext c = new DataClasses1DataContext())
                    {
                        var userTables = from u in c.UserTables
                                         where u.Nickname == nickName
                                         where u.IsLogicalDelete == false
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

        #region 引数チェック基本メソッド
        /// <summary>
        /// 引数チェック基本メソッド1
        /// </summary>
        /// <param name="targetData">引数をチェックしたい対象</param>
        /// <param name="targetDataStingLenght">チェック対象の最大文字数＋１</param>
        /// <returns>
        /// true:NULLではなく、空欄でもなく、スペースのみでもなくチェック対象の最大文字数より小さい
        /// false;NULLもしくは、空欄もしくは、スペースもしくはチェック対象の最大文字数より小さい
        /// </returns>
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
            if (targetData.Length >= targetDataStingLenght)
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// 引数チェックメソッド２
        /// </summary>
        /// <param name="targetData"></param>
        /// <returns></returns>
        private bool CanInput(string targetData)
        {
            if (string.IsNullOrEmpty(targetData))
            {
                return false;
            }
            if (string.IsNullOrWhiteSpace(targetData))
            {
                return false;
            }
            return true;
        }
        #endregion
    }
}
