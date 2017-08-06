using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ObjectModel;
using DataAccessLayer;
using System.Data;
using MySql.Data.MySqlClient;
using System.Web;
using System.IO;

namespace WebAPI.Controllers
{
    public class SkillsController : ApiController
    {
        MySqlDAL dataAccessLayer = new MySqlDAL();

        [HttpGet]
        public object GetSkills()
        {
            DataTable table = dataAccessLayer.ExecuteReader(
                "skills_select",
                CommandType.StoredProcedure
                );
            return Json(table);
        }

        [HttpPost]
        public void InsertSkill()
        {
            HttpPostedFile image = HttpContext.Current.Request.Files["image"];
            byte[] imageByte = new BinaryReader(image.InputStream).ReadBytes(image.ContentLength);
            dataAccessLayer.ExecuteNonQuery(
                "skills_insert",
                CommandType.StoredProcedure,
                dataAccessLayer.CreateParameter("IdParam", MySqlDbType.String, Guid.NewGuid()),
                dataAccessLayer.CreateParameter("TypeParam", MySqlDbType.String, HttpContext.Current.Request.Form["type"]),
                dataAccessLayer.CreateParameter("NameParam", MySqlDbType.String, HttpContext.Current.Request.Form["name"]),
                dataAccessLayer.CreateParameter("ImageParam", MySqlDbType.LongBlob, imageByte),
                dataAccessLayer.CreateParameter("ImageUrlParam", MySqlDbType.String, "TBD"),
                dataAccessLayer.CreateParameter("EnabledParam", MySqlDbType.Int16, Convert.ToBoolean(HttpContext.Current.Request.Form["enabled"]))
                );
        }

        [HttpPut]
        public void EditSkill(Skills skills)
        {
            dataAccessLayer.ExecuteNonQuery(
                "skills_enabledupdate",
                CommandType.StoredProcedure,
                dataAccessLayer.CreateParameter("IdParameter", MySqlDbType.String, skills.id),
                dataAccessLayer.CreateParameter("EnabledParameter", MySqlDbType.Int16, Convert.ToBoolean(skills.enabled))
                );
        }

        public void DeleteSkill(Skills skills)
        {
            dataAccessLayer.ExecuteNonQuery(
                "skills_delete",
                CommandType.StoredProcedure,
                dataAccessLayer.CreateParameter("IdParameter", MySqlDbType.String, skills.id)
                );
        }
    }
}
