using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Transactions;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Diagnostics;
using System.Text;

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
    [WebService(Namespace = "http://colonywebservices.azurewebsites.net/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class WebService1 : System.Web.Services.WebService
    {
        #region 定数
        private const int TARGET_WANT = 0;
        private const int TARGET_GET = 1;
        private const int TARGET_GIVE = 2; 
        #endregion  

        #region HelloWord
        /// <summary>
        /// HelloWord
        /// </summary>
        [WebMethod]
        [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
        public void HelloWorld()
        {
            test[] test = new test[]{ new test() { name = "HelloWord1" }, new test() { name = "HelloWord2" } };
           // test[] test = new test[] { new test() { name = "HelloWord1" }};
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
        public void CreateUserIdByNickName(string nickName)
        {
            string returnValue = string.Empty;
            try
            {
                if (CanInput(nickName, 3501))
                {
                    using (var transaction = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions
                    {
                        IsolationLevel = IsolationLevel.ReadUncommitted,
                        Timeout = TransactionManager.DefaultTimeout,
                    }))
                    {
                        using (DataClasses1DataContext c = new DataClasses1DataContext())
                        {
                            var users = from u in c.UserTables
                                         where u.Nickname == nickName
                                         where u.IsLogicalDelete == false
                                         select u.Id;
                            if (users.Count() == 0 || users.Count() > 1)
                            {
                                returnValue = Guid.NewGuid().ToString();
                            }
                            else
                            {
                                foreach (var user in users)
                                {
                                    returnValue = user.ToString();
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Trace.Write(ex.Message);
                returnValue = string.Empty;
            }
            finally
            {
                JavaScriptSerializer js = new JavaScriptSerializer();
                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                Context.Response.Write(js.Serialize(returnValue));
            }
        }
        #endregion

        #region グループID取得
        /// <summary>
        /// グループID取得
        /// </summary>
        [WebMethod]
        [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
        public void CreateGroupId(string groupName)
        {
            string returnValue = string.Empty;
            try
            {
                if (CanInput(groupName, 3501))
                {
                    using (var transaction = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions
                    {
                        IsolationLevel = IsolationLevel.ReadUncommitted,
                        Timeout = TransactionManager.DefaultTimeout,
                    }))
                    {
                        using (DataClasses1DataContext c = new DataClasses1DataContext())
                        {
                            var groups = from g in c.GroupTables
                                         join ugt in c.UserGroupTables on g.GroupId equals ugt.GroupId01
                                         where g.GroupName == groupName
                                         where g.IsLogicalDelete == false
                                         where ugt.IsLogicalDelete == false
                                         select g.GroupId;
                            //新規の場合はGUIDをここで生成。既存のものがあればそれを返す
                            if (groups.Count() == 0 || groups.Count() > 1)
                            {
                                returnValue = Guid.NewGuid().ToString();
                            }
                            else
                            {
                                foreach(var group in groups)
                                {
                                    returnValue = group.ToString();
                                }
                            }             
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Trace.Write(ex.Message);
                returnValue = string.Empty;
            }
            finally
            {
                JavaScriptSerializer js = new JavaScriptSerializer();
                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                Context.Response.Write(js.Serialize(returnValue));
            }
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
                Trace.WriteLine(ex.Message);
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
                        Timeout = TransactionManager.DefaultTimeout
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
                Trace.WriteLine(ex.Message);
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
                Trace.WriteLine(ex.Message);
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
                Trace.WriteLine(ex.Message);
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
        public void ModifyNickName(string userId, string oldNickname, string newNickname)
        {
            bool judgement = false;
            try
            {
                /* 入力値チェック */
                Guid p = new Guid();
                if (!Guid.TryParse(userId, out p)) { judgement = false; }
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
                                        where ut.Id == Guid.Parse(userId)
                                        where ut.IsLogicalDelete == false
                                        select ut;

                            if (query.Count() != 1) { judgement = false; }

                            foreach (UserTable u in query)
                            {
                                u.Nickname = newNickname;
                            }
                            c.SubmitChanges();
                        }
                        t.Complete();
                        judgement = true;
                    }
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message);
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

        #region メアド変更
        /// <summary>
        /// メールアドレス変更
        /// </summary>
        /// <param name="userId">ユーザーID</param>
        /// <param name="oldMailAddress">修正前メールアドレス</param>
        /// <param name="newMailAddress">修正後メールアドレス</param>
        [WebMethod]
        [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
        public void ModifyMailAddress(string userId, string oldMailAddress, string newMailAddress)
        {
            bool judgement = false;
            try
            {
                /* 入力値チェック */
                Guid p = new Guid();
                if (!Guid.TryParse(userId, out p)) { judgement = false; }
                if (judgement)
                {
                    judgement = CanInput(oldMailAddress, 301);
                    if (judgement)
                    {
                        judgement = CanInput(newMailAddress, 301);
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
                                        where ut.Nickname == oldMailAddress
                                        where ut.Id == Guid.Parse(userId)
                                        where ut.IsLogicalDelete == false
                                        select ut;

                            if (query.Count() != 1) { judgement = false; }

                            foreach (UserTable u in query)
                            {
                                u.MailAddress = newMailAddress;
                            }
                            c.SubmitChanges();
                        }
                        t.Complete();
                        judgement = true;
                    }
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message);
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

        #region グループ追加
        #endregion

        #region グループ変更
        #endregion

        #region グループ削除
        #endregion

        #region タグ追加
        #endregion

        #region タグ修正
        #endregion

        #region メアドからUserIDを取得
        /// <summary>
        /// メアドからUserIDを取得 
        /// </summary>
        /// <param name="mailAddress">メールアドレス</param>
        [WebMethod]
        [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
        public void GetUserIdByMailAddress(String mailAddress)
        {
            string returnValue = string.Empty;
            try
            {
                if(!CanInput(mailAddress, 301))
                {
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
                            if (userTables.Count() != 1) { returnValue = String.Empty; }
                            foreach (UserTable userTable in userTables)
                            {
                                returnValue = userTable.Id.ToString();
                            }
                        }
                    }
                }else
                {
                    returnValue = string.Empty;
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message);
                returnValue = String.Empty;
            }
            finally
            {
                /* 最終的には judgement の値を返す */
                JavaScriptSerializer js = new JavaScriptSerializer();
                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                Context.Response.Write(js.Serialize(returnValue));
            }
        }
        #endregion

        #region グループ名からGroupIDを取得
        /// <summary>
        /// グループ名からGroupIDを取得
        /// </summary>
        /// <param name="groupName">グループ名</param>
        [WebMethod]
        [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
        public void GetGroupIdByGroupName(string groupName)
        {
            string groupId = string.Empty;
            try
            {
                /* 入力値チェック */
                if (!CanInput(groupName, 351))
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
                                        select g;
                            if (query.Count() != 1) { groupId = string.Empty; }
                            foreach (GroupTable gt in query)
                            {
                                groupId = gt.GroupId.ToString();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message);
                groupId = string.Empty;
            }
            finally
            {
                /* 最終的には judgement の値を返す */
                JavaScriptSerializer js = new JavaScriptSerializer();
                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                Context.Response.Write(js.Serialize(groupId));
            }
        }
        #endregion

        #region TargetIDを新規作成（GUID）
        /// <summary>
        /// TargetIDを新規作成
        /// </summary>
        [WebMethod]
        [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
        public void CreateTargetId()
        {
            Guid targetId = Guid.NewGuid();
            JavaScriptSerializer js = new JavaScriptSerializer();
            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            Context.Response.Write(js.Serialize(targetId));
        }
        #endregion

        #region 情報作成
        /// <summary>
        /// 情報作成
        /// </summary>
        /// <param name="userId">ユーザーID</param>
        /// <param name="mode">モード</param>
        /// <param name="tags">タグ</param>
        /// <param name="groupName">グループ名</param>
        /// <param name="whatAttribute">Whatの内容</param>
        /// <param name="whenAttribute">Whenの内容</param>
        /// <param name="whyAttribute">Whyの内容</param>
        /// <param name="whoAttribute">Whoの内容</param>
        /// <param name="whereAttribute">whereの内容</param>
        /// <param name="whomAttribute">whomの内容</param>
        /// <param name="howAttribute">howの内容</param>
        /// <param name="howMuchAttribute">how muchの内容</param>
        /// <param name="howManyAttribute">how manyの内容</param>
        /// <returns>
        ///  false : 作成失敗,
        ///  true  : 作成OK
        /// </returns>
        private bool CreateTargetData(string userId, int mode, string tags, string groupName
                                                  , string whatAttribute, string whenAttribute, string whyAttribute, string whoAttribute
                                                  , string whereAttribute, string whomAttribute, string howAttribute
                                                  , string howMuchAttribute, string howManyAttribute)
        {
            /* バリデーション */
            // ・引数のものは全部必須入力項目
            // ・チェックは何か値が入っている
            // ・ユーザーIDはGUIDに変換可能である
            // ・WhenAttributeは日付型に変換可能である
            bool returnValue = false;
            try
            {
                /* バリデーション */
                if (CanInput(userId)) { returnValue = true; } else { returnValue = false; }
                if (returnValue == true & CanInput(tags)) { returnValue = true; } else { returnValue = false; }
                if (returnValue == true & CanInput(groupName)) { returnValue = true; } else { returnValue = false; }
                if (returnValue == true & CanInput(whatAttribute)) { returnValue = true; } else { returnValue = false; }
                if (returnValue == true & CanInput(whenAttribute)) { returnValue = true; } else { returnValue = false; }
                if (returnValue == true & CanInput(whyAttribute)) { returnValue = true; } else { returnValue = false; }
                if (returnValue == true & CanInput(whoAttribute)) { returnValue = true; } else { returnValue = false; }
                if (returnValue == true & CanInput(whereAttribute)) { returnValue = true; } else { returnValue = false; }
                if (returnValue == true & CanInput(whomAttribute)) { returnValue = true; } else { returnValue = false; }
                if (returnValue == true & CanInput(howAttribute)) { returnValue = true; } else { returnValue = false; }
                if (returnValue == true & CanInput(howMuchAttribute)) { returnValue = true; } else { returnValue = false; }
                if (returnValue == true & CanInput(howManyAttribute)) { returnValue = true; } else { returnValue = false; }
                Guid gp;
                if (returnValue == true & Guid.TryParse(userId, out gp)) { returnValue = true; } else { returnValue = false; }
                DateTime dp;
                if (returnValue == true & DateTime.TryParse(userId, out dp)) { returnValue = true; } else { returnValue = false; }

                if (returnValue)
                {
                    /* 入力値変換 */
                    Guid GUIDuserId = Guid.Parse(userId);
                    DateTime DatetimeWhenAttribute = DateTime.Parse(whenAttribute);
                    DateTime nowDateTime = DateTime.Now;
                    Guid targetDataId = Guid.NewGuid();
                    /* Insert対象データ生成 */
                    UserTargetDataTable utdt = new UserTargetDataTable
                    {
                        UserId = GUIDuserId,
                        TargetDataId = targetDataId,
                        IsLogicalDelete = false,
                        CreateUser = GUIDuserId,
                        CreateDate = nowDateTime
                    };
                    TargetDataTable tdt = new TargetDataTable
                    {
                        Id = targetDataId,
                        Mode = mode,
                        Tags = tags,
                        OccurrenceDateTime = nowDateTime,
                        WhatAttribute = whatAttribute,
                        WhenAttribute = DatetimeWhenAttribute,
                        WhyAttribute = whyAttribute,
                        WhoAttribute = GUIDuserId,
                        WhereAttribute = whereAttribute,
                        WhomAttribute = whomAttribute,
                        HowAttribute = howAttribute,
                        HowMuchAttribute = howMuchAttribute,
                        HowManyAttribute = howManyAttribute,
                        GroupNames = groupName,
                        IsLogicalDelete = false,
                        CreateUser = GUIDuserId,
                        CreateDate = nowDateTime
                    };
                    /* 2テーブルは同一トランザクション内でデータ挿入する */
                    using (TransactionScope t = new TransactionScope())
                    {
                        using (DataClasses1DataContext c = new DataClasses1DataContext())
                        {
                            c.TargetDataTable.InsertOnSubmit(tdt);
                            c.UserTargetDataTable.InsertOnSubmit(utdt);
                            c.SubmitChanges();
                        }
                        t.Complete();
                        returnValue = true;
                    }
                }
            }
            catch(Exception ex)
            {
                Trace.WriteLine(ex.Message);
                returnValue =  false;
            }
            return returnValue;
        }
        #endregion

        #region 情報変更
        /// <summary>
        /// 情報変更
        /// </summary>
        /// <param name="userId">ユーザーID</param>
        /// <param name="mode">モード</param>
        /// <param name="tags">タグ</param>
        /// <param name="groupName">グループ名</param>
        /// <param name="targetDataID">ターゲットデータID</param>
        /// <param name="whatAttribute">whatの内容</param>
        /// <param name="whenAttribute">whenの内容</param>
        /// <param name="whyAttribute">whyの内容</param>
        /// <param name="whoAttribute">whoの内容</param>
        /// <param name="whereAttribute">whereの内容</param>
        /// <param name="whomAttribute">whomの内容</param>
        /// <param name="howAttribute">howの内容</param>
        /// <param name="howMuchAttribute">how muchの内容</param>
        /// <param name="howManyAttribute">how manyの内容</param>
        /// <returns></returns>
        private bool modifyTargetData(string userId, int mode, string tags, string groupName, string targetDataID
                                                  , string whatAttribute, string whenAttribute, string whyAttribute, string whoAttribute
                                                  , string whereAttribute, string whomAttribute, string howAttribute
                                                  , string howMuchAttribute, string howManyAttribute)
        {
            bool returnValue = false;
            try
            {
                /* バリデーション */
                // ・引数のものは全部必須入力項目
                // ・チェックは何か値が入っている
                // ・ユーザーID、ターゲットデータIDはGUIDに変換可能である
                // ・WhenAttributeは日付型に変換可能である
                if (CanInput(userId)) { returnValue = true; }
                if (returnValue == true & CanInput(tags)) { returnValue = true; } else { returnValue = false; }
                if (returnValue == true & CanInput(groupName)) { returnValue = true; } else { returnValue = false; }
                if (returnValue == true & CanInput(whatAttribute)) { returnValue = true; } else { returnValue = false; }
                if (returnValue == true & CanInput(whenAttribute)) { returnValue = true; } else { returnValue = false; }
                if (returnValue == true & CanInput(whyAttribute)) { returnValue = true; } else { returnValue = false; }
                if (returnValue == true & CanInput(whoAttribute)) { returnValue = true; } else { returnValue = false; }
                if (returnValue == true & CanInput(whereAttribute)) { returnValue = true; } else { returnValue = false; }
                if (returnValue == true & CanInput(whomAttribute)) { returnValue = true; } else { returnValue = false; }
                if (returnValue == true & CanInput(howAttribute)) { returnValue = true; } else { returnValue = false; }
                if (returnValue == true & CanInput(howMuchAttribute)) { returnValue = true; } else { returnValue = false; }
                if (returnValue == true & CanInput(howManyAttribute)) { returnValue = true; } else { returnValue = false; }
                if (returnValue == true & CanInput(targetDataID)) { returnValue = true; } else { returnValue = false; }
                Guid gp;
                if(returnValue == true & Guid.TryParse(userId, out gp)) { returnValue = true; } else { returnValue = false; }
                DateTime dp;
                if (returnValue == true & DateTime.TryParse(userId, out dp)) { returnValue = true; } else { returnValue = false; }
                if (returnValue == true & Guid.TryParse(targetDataID, out gp)) { returnValue = true; } else { returnValue = false; }

                if (returnValue)
                {
                    /* 入力値変換 */
                    Guid GUIDuserId = Guid.Parse(userId);
                    DateTime DatetimeWhenAttribute = DateTime.Parse(whenAttribute);
                    Guid GUIDTargetDataId = Guid.Parse(targetDataID);
                    DateTime nowDateTime = DateTime.Now;
                    Guid targetDataId = Guid.NewGuid();
                    /* 2テーブルは同一トランザクション内でデータ挿入する */
                    using (TransactionScope t = new TransactionScope())
                    {
                        using (DataClasses1DataContext c = new DataClasses1DataContext())
                        {
                            //念のためUserTargetDataTableとtargetDataTableをJoinしたものを更新データの対象とする
                            var query =
                                from utdt in c.UserTargetDataTable
                                join tdt in c.TargetDataTable on utdt.TargetDataId equals tdt.Id
                                where utdt.TargetDataId == GUIDTargetDataId
                                where utdt.UserId == GUIDuserId
                                where utdt.IsLogicalDelete == false
                                where tdt.IsLogicalDelete == false
                                select tdt;

                            foreach(TargetDataTable td in query)
                            {
                                td.Mode = mode;
                                td.Tags = tags;
                                td.WhatAttribute = whatAttribute;
                                td.WhenAttribute = DatetimeWhenAttribute;
                                td.WhyAttribute = whyAttribute;
                                td.WhoAttribute = GUIDuserId;
                                td.WhereAttribute = whereAttribute;
                                td.WhomAttribute = whomAttribute;
                                td.HowAttribute = howAttribute;
                                td.HowMuchAttribute = howMuchAttribute;
                                td.HowManyAttribute = howManyAttribute;
                                td.GroupNames = groupName;
                                td.IsLogicalDelete = false;
                                td.UpdateUser = GUIDuserId;
                                td.UpdateDate = nowDateTime;
                            }
                            c.SubmitChanges();
                        }
                        t.Complete();
                        returnValue = true;
                    }
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message);
                returnValue = false;
            }
            return returnValue;
        }
        #endregion

        #region Want情報作成
        /// <summary>
        /// Want情報作成
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="tags"></param>
        /// <param name="groupName"></param>
        /// <param name="whatAttribute"></param>
        /// <param name="whenAttribute"></param>
        /// <param name="whyAttribute"></param>
        /// <param name="whoAttribute"></param>
        /// <param name="whereAttribute"></param>
        /// <param name="whomAttribute"></param>
        /// <param name="howAttribute"></param>
        /// <param name="howMuchAttribute"></param>
        /// <param name="howManyAttribute"></param>
        [WebMethod]
        [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
        public void CreateDataWant(string userId, string tags, string groupName
                                                    , string whatAttribute, string whenAttribute, string whyAttribute, string whoAttribute
                                                    , string whereAttribute, string whomAttribute, string howAttribute
                                                    , string howMuchAttribute, string howManyAttribute)
        {
            bool returnValue = false;
            try
            {
                returnValue = CreateTargetData(userId, TARGET_WANT, tags, groupName
                                                            , whatAttribute, whenAttribute, whyAttribute
                                                            , whoAttribute, whereAttribute, whomAttribute
                                                            , howAttribute, howMuchAttribute, howManyAttribute);
            }catch(Exception ex)
            {
                Trace.Write(ex.Message);
                returnValue = false;
            }
            finally
            {
                /* 最終的には judgement の値を返す */
                JavaScriptSerializer js = new JavaScriptSerializer();
                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                Context.Response.Write(js.Serialize(returnValue));
            }
        }
        #endregion

        #region Want情報更新
        /// <summary>
        /// Want情報更新
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="tags"></param>
        /// <param name="groupName"></param>
        /// <param name="targetDataID"></param>
        /// <param name="whatAttribute"></param>
        /// <param name="whenAttribute"></param>
        /// <param name="whyAttribute"></param>
        /// <param name="whoAttribute"></param>
        /// <param name="whereAttribute"></param>
        /// <param name="whomAttribute"></param>
        /// <param name="howAttribute"></param>
        /// <param name="howMuchAttribute"></param>
        /// <param name="howManyAttribute"></param>
        [WebMethod]
        [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
        public void ModifyDataWant(string userId, string tags, string groupName, string targetDataID
                                                   , string whatAttribute, string whenAttribute, string whyAttribute, string whoAttribute
                                                   , string whereAttribute, string whomAttribute, string howAttribute
                                                   , string howMuchAttribute, string howManyAttribute)
        {
            bool returnValue = false;
            try
            {
                returnValue = modifyTargetData(userId, TARGET_WANT, tags, groupName, targetDataID
                                                                  , whatAttribute, whenAttribute, whyAttribute
                                                                  , whoAttribute, whereAttribute, whomAttribute
                                                                  , howAttribute, howMuchAttribute, howManyAttribute);
            }
            catch (Exception ex)
            {
                Trace.Write(ex.Message);
                returnValue = false;
            }
            finally
            {
                /* 最終的には judgement の値を返す */
                JavaScriptSerializer js = new JavaScriptSerializer();
                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                Context.Response.Write(js.Serialize(returnValue));
            }
        }
        #endregion

        #region Get情報作成
        /// <summary>
        /// Get情報作成
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="tags"></param>
        /// <param name="groupName"></param>
        /// <param name="whatAttribute"></param>
        /// <param name="whenAttribute"></param>
        /// <param name="whyAttribute"></param>
        /// <param name="whoAttribute"></param>
        /// <param name="whereAttribute"></param>
        /// <param name="whomAttribute"></param>
        /// <param name="howAttribute"></param>
        /// <param name="howMuchAttribute"></param>
        /// <param name="howManyAttribute"></param>
        [WebMethod]
        [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
        public void CreateDataGet(string userId, string tags, string groupName
                                                    , string whatAttribute, string whenAttribute, string whyAttribute, string whoAttribute
                                                    , string whereAttribute, string whomAttribute, string howAttribute
                                                    , string howMuchAttribute, string howManyAttribute)
        {
            bool returnValue = false;
            try
            {
                returnValue = CreateTargetData(userId, TARGET_GET, tags, groupName
                                                            , whatAttribute, whenAttribute, whyAttribute
                                                            , whoAttribute, whereAttribute, whomAttribute
                                                            , howAttribute, howMuchAttribute, howManyAttribute);
            }
            catch (Exception ex)
            {
                Trace.Write(ex.Message);
                returnValue = false;
            }
            finally
            {
                /* 最終的には judgement の値を返す */
                JavaScriptSerializer js = new JavaScriptSerializer();
                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                Context.Response.Write(js.Serialize(returnValue));
            }
        }
        #endregion

        #region Get情報更新
        /// <summary>
        /// Get情報更新
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="tags"></param>
        /// <param name="groupName"></param>
        /// <param name="targetDataID"></param>
        /// <param name="whatAttribute"></param>
        /// <param name="whenAttribute"></param>
        /// <param name="whyAttribute"></param>
        /// <param name="whoAttribute"></param>
        /// <param name="whereAttribute"></param>
        /// <param name="whomAttribute"></param>
        /// <param name="howAttribute"></param>
        /// <param name="howMuchAttribute"></param>
        /// <param name="howManyAttribute"></param>
        [WebMethod]
        [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
        public void ModifyDataGet(string userId, string tags, string groupName, string targetDataID
                                                , string whatAttribute, string whenAttribute, string whyAttribute, string whoAttribute
                                                , string whereAttribute, string whomAttribute, string howAttribute
                                                , string howMuchAttribute, string howManyAttribute)
        {
            bool returnValue = false;
            try
            {
                returnValue = modifyTargetData(userId, TARGET_GET, tags, groupName, targetDataID
                                                                  , whatAttribute, whenAttribute, whyAttribute
                                                                  , whoAttribute, whereAttribute, whomAttribute
                                                                  , howAttribute, howMuchAttribute, howManyAttribute);
            }
            catch (Exception ex)
            {
                Trace.Write(ex.Message);
                returnValue = false;
            }
            finally
            {
                /* 最終的には judgement の値を返す */
                JavaScriptSerializer js = new JavaScriptSerializer();
                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                Context.Response.Write(js.Serialize(returnValue));
            }
        }
        #endregion

        #region Give情報作成
        /// <summary>
        /// Give情報作成
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="tags"></param>
        /// <param name="groupName"></param>
        /// <param name="whatAttribute"></param>
        /// <param name="whenAttribute"></param>
        /// <param name="whyAttribute"></param>
        /// <param name="whoAttribute"></param>
        /// <param name="whereAttribute"></param>
        /// <param name="whomAttribute"></param>
        /// <param name="howAttribute"></param>
        /// <param name="howMuchAttribute"></param>
        /// <param name="howManyAttribute"></param>
        [WebMethod]
        [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
        public void CreateDataGive(string userId, string tags, string groupName
                                                    , string whatAttribute, string whenAttribute, string whyAttribute, string whoAttribute
                                                    , string whereAttribute, string whomAttribute, string howAttribute
                                                    , string howMuchAttribute, string howManyAttribute)
        {
            bool returnValue = false;
            try
            {
                returnValue = CreateTargetData(userId, TARGET_GIVE, tags, groupName
                                                            , whatAttribute, whenAttribute, whyAttribute
                                                            , whoAttribute, whereAttribute, whomAttribute
                                                            , howAttribute, howMuchAttribute, howManyAttribute);
            }
            catch (Exception ex)
            {
                Trace.Write(ex.Message);
                returnValue = false;
            }
            finally
            {
                /* 最終的には judgement の値を返す */
                JavaScriptSerializer js = new JavaScriptSerializer();
                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                Context.Response.Write(js.Serialize(returnValue));
            }
        }
        #endregion

        #region Give情報更新
        /// <summary>
        /// Give情報更新
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="tags"></param>
        /// <param name="groupName"></param>
        /// <param name="targetDataID"></param>
        /// <param name="whatAttribute"></param>
        /// <param name="whenAttribute"></param>
        /// <param name="whyAttribute"></param>
        /// <param name="whoAttribute"></param>
        /// <param name="whereAttribute"></param>
        /// <param name="whomAttribute"></param>
        /// <param name="howAttribute"></param>
        /// <param name="howMuchAttribute"></param>
        /// <param name="howManyAttribute"></param>
        [WebMethod]
        [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
        public void ModifyDataGive(string userId, string tags, string groupName, string targetDataID
                                                  , string whatAttribute, string whenAttribute, string whyAttribute, string whoAttribute
                                                  , string whereAttribute, string whomAttribute, string howAttribute
                                                  , string howMuchAttribute, string howManyAttribute)
        {
            bool returnValue = false;
            try
            {
                returnValue = modifyTargetData(userId, TARGET_GIVE, tags, groupName, targetDataID
                                                                  , whatAttribute, whenAttribute, whyAttribute
                                                                  , whoAttribute, whereAttribute, whomAttribute
                                                                  , howAttribute, howMuchAttribute, howManyAttribute);
            }
            catch (Exception ex)
            {
                Trace.Write(ex.Message);
                returnValue = false;
            }
            finally
            {
                /* 最終的には judgement の値を返す */
                JavaScriptSerializer js = new JavaScriptSerializer();
                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                Context.Response.Write(js.Serialize(returnValue));
            }
        }
        #endregion

        #region 検索(条件に合致した情報をリスト化)
        /// <summary>
        /// 検索(条件に合致した情報をリスト化)
        /// </summary>
        /// <param name="searchTargetData"></param>
        [WebMethod]
        [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
        public void SearchForList(string searchTargetData)
        {
            var returnValue = new Dictionary<int, string>();            
            try
            {
                using (var transaction = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions
                {
                    IsolationLevel = IsolationLevel.ReadUncommitted,
                    Timeout = TransactionManager.DefaultTimeout,
                }))
                {
                    using (DataClasses1DataContext c = new DataClasses1DataContext())
                    {
                        var list = from u in c.UserTables
                                   join utdt in c.UserTargetDataTable on u.Id equals utdt.UserId
                                   join tdt in c.TargetDataTable on utdt.TargetDataId equals tdt.Id
                                   join ugt in c.UserGroupTables on u.Id equals ugt.UserId
                                   join g in c.GroupTables on ugt.GroupId01 equals g.GroupId
                                   where u.IsLogicalDelete == false
                                   where utdt.IsLogicalDelete == false
                                   where tdt.IsLogicalDelete == false
                                   where ugt.IsLogicalDelete == false
                                   where g.IsLogicalDelete == false
                                   where tdt.WhenAttribute <= DateTime.Now
                                   where tdt.WhoAttribute == u.Id
                                   where u.Nickname.Contains(searchTargetData)
                                   || u.MailAddress.Contains(searchTargetData)
                                   || tdt.Tags.Contains(searchTargetData)
                                   || tdt.WhatAttribute.Contains(searchTargetData)
                                   || tdt.WhyAttribute.Contains(searchTargetData)
                                   || tdt.WhomAttribute.Contains(searchTargetData)
                                   || tdt.HowAttribute.Contains(searchTargetData)
                                   || tdt.HowManyAttribute.Contains(searchTargetData)
                                   || tdt.HowMuchAttribute.Contains(searchTargetData)
                                   select new
                                   {
                                       name = u.Nickname,
                                       mailAddress = u.MailAddress,
                                       groupName = g.GroupName,
                                       mode = tdt.Mode,
                                       tags = tdt.Tags,
                                       OccurrenceDateTime = tdt.OccurrenceDateTime,
                                       whatAttr = tdt.WhatAttribute,
                                       whenAttr = tdt.WhenAttribute,
                                       whereAttr = tdt.WhereAttribute,
                                       whyAttr = tdt.WhyAttribute,
                                       whoAttr = tdt.WhoAttribute,
                                       whomAttr = tdt.WhomAttribute,
                                       howAttr = tdt.HowAttribute,
                                       howMuchAttr = tdt.HowMuchAttribute,
                                       howManyAttr = tdt.HowManyAttribute
                                   };
                        int i = 0;
                        foreach (var content in list)
                        {
                            var sb = new StringBuilder();
                            sb.Append(content.name).Append(", ");                       
                            sb.Append(content.mailAddress).Append(", ");
                            sb.Append(content.groupName).Append(", ");
                            sb.Append(content.mode).Append(", ");
                            sb.Append(content.tags).Append(", ");
                            sb.Append(content.OccurrenceDateTime).Append(", ");
                            sb.Append(content.whatAttr).Append(", ");
                            sb.Append(content.whenAttr).Append(", ");
                            sb.Append(content.whereAttr).Append(", ");
                            sb.Append(content.whyAttr).Append(", ");
                            sb.Append(content.whoAttr).Append(", ");
                            sb.Append(content.whomAttr).Append(", ");
                            sb.Append(content.howAttr).Append(", ");
                            sb.Append(content.howMuchAttr).Append(", ");
                            sb.Append(content.howManyAttr).Append(", ");
                            returnValue.Add(i, sb.ToString());
                            i++;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Trace.Write(ex.Message);
            }
            finally
            {
                JavaScriptSerializer js = new JavaScriptSerializer();
                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                Context.Response.Write(js.Serialize(returnValue));
            }
        }
        #endregion

        #region 検索(リストから選択されたものの詳細情報取得)
        /// <summary>
        /// 検索(リストから選択されたものの詳細情報取得)
        /// </summary>
        /// <param name="rawTargetDataId"></param>
        [WebMethod]
        [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
        public void SearchForDetail(string rawTargetDataId)
        {
            string returnValue = string.Empty;
            try
            {
                Guid test;
                Guid targetDataId = Guid.NewGuid();
                if (Guid.TryParse(rawTargetDataId, out test))
                {
                    targetDataId = Guid.Parse(rawTargetDataId);
                    using (var transaction = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions
                    {
                        IsolationLevel = IsolationLevel.ReadUncommitted,
                        Timeout = TransactionManager.DefaultTimeout,
                    }))
                    {
                        using (DataClasses1DataContext c = new DataClasses1DataContext())
                        {
                            var detailData = from u in c.UserTables
                                        join utdt in c.UserTargetDataTable on u.Id equals utdt.UserId
                                        join t in c.TargetDataTable on utdt.TargetDataId equals t.Id
                                        join ug in c.UserGroupTables on u.Id equals ug.UserId
                                        join g in c.GroupTables on ug.GroupId01 equals g.GroupId
                                        where utdt.TargetDataId == targetDataId
                                        where u.IsLogicalDelete == false
                                        where utdt.IsLogicalDelete == false
                                        where t.IsLogicalDelete == false
                                        where ug.IsLogicalDelete == false
                                        where g.IsLogicalDelete == false
                                        select new { name = u.Nickname, mailAddress = u.MailAddress, groupName = g.GroupName
                                                         , mode = t.Mode, tags = t.Tags, OccurrenceDateTime = t.OccurrenceDateTime
                                                         , whatAttr = t.WhatAttribute, whenAttr = t.WhenAttribute, whereAttr = t.WhereAttribute
                                                         , whyAttr = t.WhyAttribute, whoAttr= t.WhoAttribute, whomAttr = t.WhomAttribute
                                                         , howAttr = t.HowAttribute, howMuchAttr= t.HowMuchAttribute, howManyAttr = t.HowManyAttribute};
                            if (detailData.Count() != 1)
                            {
                                returnValue = string.Empty;
                            }else
                            {
                                var sb = new StringBuilder();
                                foreach (var d in detailData)
                                {
                                    sb.Append("Name:").Append(d.name).Append("").Append(", ");
                                    sb.Append("MailAddress:").Append(d.mailAddress).Append("").Append(", ");
                                    sb.Append("GroupName:").Append(d.groupName).Append("").Append(", ");
                                    sb.Append("Mode:").Append(d.mode).Append("").Append(", ");
                                    sb.Append("Tags:").Append(d.tags).Append("").Append(", ");
                                    sb.Append("OccurrenceDataTime:").Append(d.OccurrenceDateTime).Append("").Append(", ");
                                    sb.Append("What:").Append(d.whatAttr).Append("").Append(", ");
                                    sb.Append("When:").Append(d.whenAttr).Append("").Append(", ");
                                    sb.Append("Where:").Append(d.whereAttr).Append("").Append(", ");
                                    sb.Append("Why:").Append(d.whyAttr).Append("").Append(", ");
                                    sb.Append("Who:").Append(d.whoAttr).Append("").Append(", ");
                                    sb.Append("Whom:").Append(d.whomAttr).Append("").Append(", ");
                                    sb.Append("How:").Append(d.howAttr).Append("").Append(", ");
                                    sb.Append("HowMuch:").Append(d.howMuchAttr).Append("").Append(", ");
                                    sb.Append("HowMany:").Append(d.howManyAttr).Append("");
                                }
                                returnValue = sb.ToString();
                            }
                        }
                    }
                }else { returnValue = string.Empty; }
            }
            catch (Exception ex)
            {
                Trace.Write(ex.Message);
                returnValue = string.Empty;
            }
            finally
            {
                JavaScriptSerializer js = new JavaScriptSerializer();
                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                Context.Response.Write(js.Serialize(returnValue));
            }
        }
        #endregion

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
