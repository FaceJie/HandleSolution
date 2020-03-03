using Dos.Common;
using Dos.ORM;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DataBase
{
    
    public abstract class Repository<T> where T : Entity
    {
        public static class Db
        {
            public static readonly DbSession Context = new DbSession(DbProvider.databaseType, DbProvider.connectionStr);
            public static DbTrans _tran;
            static Db()
            {
                Context.RegisterSqlLogger(delegate (string sql)
                {
                    if (!string.IsNullOrEmpty(DbProvider.isOpenOrg) &&  DbProvider.isOpenOrg=="1")
                    {
                        LogHelper.Debug(sql, "SQL日志");
                    }
                });
            }

            static void DbUseTrans(DbTrans trans)
            {
                trans = _tran;
                Context.RegisterSqlLogger(delegate (string sql)
                {
                    if (!string.IsNullOrEmpty(DbProvider.isOpenOrg) && DbProvider.isOpenOrg == "1")
                    {
                        LogHelper.Debug(sql, "SQL日志");
                    }
                });
            }
        }

        public bool Commit()
        {
            DbTrans trans = Db.Context.BeginTransaction();
            try
            {
                trans.Commit();
                return true;
            }
            catch (Exception ex)
            {
                trans.Rollback();
                throw new Exception(ex.Message.ToString());
            }
            finally
            {
                trans.Close();
            }
        }
        public void Dispose()
        {
            if (Db._tran != null)
            {
                Db._tran.Close();
            }
            GC.SuppressFinalize(this);
        }

        #region 查询
        public static List<T> GetAll()
        {
            return Db.Context.From<T>().ToList();
        }
        public static List<T> GetAll(Expression<Func<T, bool>> where)
        {
            return Db.Context.From<T>().Where(where).ToList();
        }
        public static List<T> GetAll(Where<T> where, OrderByClip orderBy = null)
        {
            var fs = Db.Context.From<T>().Where(where);
            if (orderBy != null)
            {
                fs.OrderBy(orderBy);
            }
            return fs.ToList();
        }
        public static List<T> GetAll(OrderByClip orderBy = null)
        {
            var fs = Db.Context.From<T>();
            if (orderBy != null)
            {
                fs.OrderBy(orderBy);
            }
            return fs.ToList();
        }
        public static List<T> GetAll(Expression<Func<T, object>> orderBy = null, string ascOrDesc = "asc")
        {
            var fs = Db.Context.From<T>();
            if (orderBy != null)
            {
                if (ascOrDesc.ToLower() == "asc")
                {
                    fs.OrderBy(orderBy);
                }
                else
                {
                    fs.OrderByDescending(orderBy);
                }
            }
            return fs.ToList();
        }
        public static List<T> GetAll(OrderByClip[] orderBy = null, string ascOrDesc = "asc")
        {
            var fs = Db.Context.From<T>();
            if (orderBy != null)
            {
                fs.OrderBy(orderBy);
            }
            return fs.ToList();
        }
        /// <summary>
        /// 通用查询
        /// </summary>
        public static List<T> Query(Expression<Func<T, bool>> where, Expression<Func<T, object>> orderBy = null, string ascOrDesc = "asc", int? top = null, int? pageSize = null, int? pageIndex = null)
        {
            var fs = Db.Context.From<T>().Where(where);
            if (top != null)
            {
                fs.Top(top.Value);
            }
            else if (pageIndex != null && pageSize != null)
            {
                fs.Page(pageSize.Value, pageIndex.Value);
            }
            if (orderBy != null)
            {
                if (ascOrDesc.ToLower() == "asc")
                {
                    fs.OrderBy(orderBy);
                }
                else
                {
                    fs.OrderByDescending(orderBy);
                }
            }
            return fs.ToList();
        }
        public static List<T> Query(Where<T> where, OrderByClip[] orderBy = null, string ascOrDesc = "asc", int? top = null, int? pageSize = null, int? pageIndex = null)
        {
            var fs = Db.Context.From<T>().Where(where);
            if (top != null)
            {
                fs.Top(top.Value);
            }
            else if (pageIndex != null && pageSize != null)
            {
                fs.Page(pageSize.Value, pageIndex.Value);
            }
            if (orderBy != null)
            {
                fs.OrderBy(orderBy);
            }
            return fs.ToList();
        }
        /// <summary>
        /// 通用查询
        /// </summary>
        public static List<T> Query(Where<T> where, Expression<Func<T, object>> orderBy = null, string ascOrDesc = "asc", int? top = null, int? pageSize = null, int? pageIndex = null)
        {
            var fs = Db.Context.From<T>().Where(where);
            if (top != null)
            {
                fs.Top(top.Value);
            }
            else if (pageIndex != null && pageSize != null)
            {
                fs.Page(pageSize.Value, pageIndex.Value);
            }
            if (orderBy != null)
            {
                if (ascOrDesc.ToLower() == "asc")
                {
                    fs.OrderBy(orderBy);
                }
                else
                {
                    fs.OrderByDescending(orderBy);
                }
            }
            return fs.ToList();
        }
        /// <summary>
        /// 通用查询
        /// </summary>
        public static List<T> Query(Where<T> where, OrderByClip orderBy = null, string ascOrDesc = "asc", int? top = null, int? pageSize = null, int? pageIndex = null)
        {
            var fs = Db.Context.From<T>().Where(where);
            if (top != null)
            {
                fs.Top(top.Value);
            }
            else if (pageIndex != null && pageSize != null)
            {
                fs.Page(pageSize.Value, pageIndex.Value);
            }
            if (orderBy != null)
            {
                fs.OrderBy(orderBy);
            }
            return fs.ToList();
        }
        /// <summary>
        /// 通用查询
        /// </summary>
        public static T First(Expression<Func<T, bool>> where, Expression<Func<T, object>> orderBy = null, string ascOrDesc = "asc", int? top = null, int? pageSize = null, int? pageIndex = null)
        {
            var fs = Db.Context.From<T>().Where(where);
            if (top != null)
            {
                fs.Top(top.Value);
            }
            else if (pageIndex != null && pageSize != null)
            {
                fs.Page(pageSize.Value, pageIndex.Value);
            }
            if (orderBy != null)
            {
                if (ascOrDesc.ToLower() == "asc")
                {
                    return fs.OrderBy(orderBy).First();
                }
                return fs.OrderByDescending(orderBy).First();
            }
            var model = fs.First();
            return model;
        }
        /// <summary>
        /// 通用查询
        /// </summary>
        public static T First(Where<T> where, Expression<Func<T, object>> orderBy = null, string ascOrDesc = "asc", int? top = null, int? pageSize = null, int? pageIndex = null)
        {
            var fs = Db.Context.From<T>().Where(where);
            if (top != null)
            {
                fs.Top(top.Value);
            }
            else if (pageIndex != null && pageSize != null)
            {
                fs.Page(pageSize.Value, pageIndex.Value);
            }
            if (orderBy != null)
            {
                if (ascOrDesc.ToLower() == "asc")
                {
                    return fs.OrderBy(orderBy).First();
                }
                return fs.OrderByDescending(orderBy).First();
            }
            return fs.First();
        }
        /// <summary>
        /// 通用查询
        /// </summary>
        public static T First(Where<T> where)
        {
            var fs = Db.Context.From<T>().Where(where);
            return fs.First();
        }

        /// <summary>
        /// 根据条件判断是否存在数据
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public static bool Any(Expression<Func<T, bool>> where)
        {
            return Db.Context.Exists<T>(where);
        }

        /// <summary>
        /// 取总数
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public static int Count(Where<T> where)
        {
            return Db.Context.From<T>().Where(where).Count();
        }

        /// <summary>
        /// 取总数
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public static int Count(Expression<Func<T, bool>> where)
        {
            return Db.Context.From<T>().Where(where).Count();
        }
        #endregion
        #region 插入
        /// <summary>
        /// 插入单个实体
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static int Insert(T entity)
        {
            return Db.Context.Insert<T>(entity);
        }
        /// <summary>
        /// 插入单个实体带事务
        /// </summary>
        /// <param name="context"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static void Insert(DbTrans context, T entity)
        {
            Db.Context.Insert<T>(context, entity);
        }
        /// <summary>
        /// 插入多个实体
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public static int Insert(IEnumerable<T> entities)
        {
            return Db.Context.Insert<T>(entities);
        }
        public static void Insert(DbTrans context, IEnumerable<T> entities)
        {
            Db.Context.Insert<T>(context, entities.ToArray());
        }
        #endregion
        #region 更新
        /// <summary>
        /// 更新单个实体
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static int Update(T entity)
        {
            return Db.Context.Update(entity);
        }
        /// <summary>
        /// 更新单个实体
        /// </summary>
        public static int Update(T entity, Where where)
        {
            return Db.Context.Update(entity, where);
        }
        /// <summary>
        /// 更新单个实体
        /// </summary>
        public static int Update(T entity, Expression<Func<T, bool>> lambdaWhere)
        {
            return Db.Context.Update(entity, lambdaWhere);
        }
        public static void Update(DbTrans context, T entity)
        {
            Db.Context.Update(context, entity);
        }
        /// <summary>
        /// 更新多个实体
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public static int Update(IEnumerable<T> entities)
        {
            var enumerable = entities as T[] ?? entities.ToArray();
            Db.Context.Update(enumerable.ToArray());
            return 1;
        }
        public static void Update(DbTrans context, IEnumerable<T> entities)
        {

            Db.Context.Update(context, entities.ToArray());

        }
        #endregion
        #region 删除
        /// <summary>
        /// 删除单个实体
        /// </summary>
        public static int Delete(T entitie)
        {
            return Db.Context.Delete<T>(entitie);
        }
        /// <summary>
        /// 删除多个实体
        /// </summary>
        public static int Delete(IEnumerable<T> entities)
        {
            return Db.Context.Delete<T>(entities);
        }
        /// <summary>
        /// 删除单个实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int Delete(Guid? id)
        {
            if (id == null)
            {
                return 0;
            }
            return Db.Context.Delete<T>(id.Value);
        }
        /// <summary>
        /// 删除单个实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int Delete(int id)
        {
            if (id == 0)
            {
                return 0;
            }
            return Db.Context.Delete<T>(id);
        }

        /// <summary>
        /// 删除单个实体
        /// </summary>
        public static int Delete(Expression<Func<T, bool>> where)
        {
            return Db.Context.Delete<T>(where);
        }
        /// <summary>
        /// 删除单个实体
        /// </summary>
        public static int Delete(Where<T> where)
        {
            return Db.Context.Delete<T>(where.ToWhereClip());
        }
        #endregion
    }
}
