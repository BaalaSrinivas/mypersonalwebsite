using System.Web.Http;
using System.Configuration;
using DataAccessLayer;
using System;
using System.IO;
using System.Data;
using MySql.Data.MySqlClient;
using System.Web;
using ObjectModel;

namespace WebAPI.Controllers
{
    public class PhotoGalleryController : ApiController
    {
        MySqlDAL dataAccessLayer = new MySqlDAL();

        [HttpGet]
        public object GetImages()
        {
            DataTable table = dataAccessLayer.ExecuteReader(
                "photogallery_select",
                CommandType.StoredProcedure);
            return Json(table);
        }

        [HttpPost]
        public void InsertImage()
        {
            HttpPostedFile imageFile = HttpContext.Current.Request.Files["image"];
            byte[] imageBytes = new BinaryReader(imageFile.InputStream).ReadBytes(imageFile.ContentLength);
            string imageUrl = "serverlocation" + imageFile.FileName;

            dataAccessLayer.ExecuteNonQuery(
                "photogallery_insert",
                CommandType.StoredProcedure,
                dataAccessLayer.CreateParameter("IdParam", MySqlDbType.String, Guid.NewGuid()),
                dataAccessLayer.CreateParameter("NameParam", MySqlDbType.String, HttpContext.Current.Request.Form["name"]),
                dataAccessLayer.CreateParameter("ImageParam", MySqlDbType.LongBlob, imageBytes),
                dataAccessLayer.CreateParameter("ImageUrlParam", MySqlDbType.String, imageUrl),
                dataAccessLayer.CreateParameter("EnabledParam", MySqlDbType.Int16, Convert.ToBoolean(HttpContext.Current.Request.Form["enabled"]))
                );
        }

        [HttpPut]
        public void EditImage(PhotoGallery photoGallery)
        {
            dataAccessLayer.ExecuteNonQuery(
                "photogallery_enabledupdate",
                CommandType.StoredProcedure,
                dataAccessLayer.CreateParameter("IdParam", MySqlDbType.String, photoGallery.id),
                dataAccessLayer.CreateParameter("EnabledParam", MySqlDbType.Int16, Convert.ToBoolean(photoGallery.enabled))
                );
        }

        [HttpDelete]
        public void DeleteImage(PhotoGallery photoGallery)
        {
            dataAccessLayer.ExecuteNonQuery(
                "photogallery_delete",
                CommandType.StoredProcedure,
                dataAccessLayer.CreateParameter("IdParam", MySqlDbType.String, photoGallery.id)
                );
        }
    }
}
