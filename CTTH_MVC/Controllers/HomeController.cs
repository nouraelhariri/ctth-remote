using CTTH_MVC.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Web;
using System.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;



using System.Web;
using System.Net;


using System.IO;
using System.Net;

using System.Data.SqlClient;
namespace Projet1_ASP.Controllers
{
    public class HomeController : Controller
    {
        List<FileResult> files ;
       string fichier;


        public ActionResult Index()
        {
            /*
            MailMessage email = new MailMessage();
            // Expéditeur
            email.From = new MailAddress("karima.elhariri@gmail.com");
            // Destinataire
            email.To.Add(new MailAddress("nouraelhariri09@gmail.com"));
            // Destinataire en copie
            email.CC.Add(new MailAddress("nouraelhariri09@gmail.com"));
            // Destinataire en copie cache
            email.Bcc.Add(new MailAddress("nouraelhariri09@gmail.com"));
            // Sujet de l'email
            email.Subject = "Email de démonstration";
            // Corps de l'email en texte
            email.Body = "Contenu de l'email au format texte";
            SmtpClient smtp = new SmtpClient();
            smtp.Credentials = new System.Net.NetworkCredential("karima.elhariri@gmail.com", "famille123");
           // smtp.UseDefaultCredentials = false;
           // smtp.Port = 465;
            smtp.Host = "smtp.gmail.com";
            smtp.EnableSsl = true;
            smtp.Send(email);*/
            return View();
        }


        [HttpPost]
        public ActionResult Index(User_mail m, HttpPostedFileBase file)
        {


           
            String ser = Server.MapPath("");
            String fileName = file.FileName;

            // Append the name of the file to upload to the path.
            ser += "\\"+fileName;
            file.SaveAs(ser);
            
           fichier = ser;            User_mail a = new User_mail(Request.Form["m"], Request.Form["p"],Request.Form["subject"],Request.Form["message"]);

            SMTP(a.email, "nouraelhariri09@gmail.com",a.password,  a.message,a.subject);

            //file suppression
 //System.IO.File.Delete(fichier);

            return View("About");
        }

     

        
            public ActionResult Presentation(string fichier )
        {
           

            return View("About");
        }
        public ActionResult Espace_telechargement()
        {
            files = new List<FileResult>();
            string pdfFilePath = Server.MapPath("..") +"/963852.pdf";
            byte[] bytes = System.IO.File.ReadAllBytes(pdfFilePath);

            files.Add(GetReport());



            return View("Espace_telechargement"  );
        }


        public FileResult GetReport()
        {
            string ReportURL = "C:/Users/DELL/Desktop/git_ctth/ctth-remote/CTTH_MVC/963852.pdf";
            byte[] FileBytes = System.IO.File.ReadAllBytes(ReportURL);
            return File(FileBytes, "application/pdf");
        }
        public ActionResult Get(string path)
        {

            
            byte[] bytes = System.IO.File.ReadAllBytes(Server.MapPath("..") +path);
            //If file exists....

            MemoryStream ms = new MemoryStream(bytes, 0, 0, true, true);
            Response.ContentType = "application/pdf";
            //Response.AddHeader("content-disposition", "inline;filename=" + file.Name);
            Response.Buffer = true;
            Response.Clear();
            Response.OutputStream.Write(ms.GetBuffer(), 0, ms.GetBuffer().Length);
            Response.OutputStream.Flush();
            Response.End();
            return new FileStreamResult(Response.OutputStream, "application/pdf");

        }




        public ActionResult Mot()
        {


            return View();
        }
        public ActionResult Pourquoi_CTTH()
        {


            return View();
        }


        public ActionResult contact()
        {
            //Instanciation du client
             SmtpClient smtpClient = new SmtpClient(Request.Form["email"], 25);
            //On indique au client d'utiliser les informations qu'on va lui fournir
            smtpClient.UseDefaultCredentials = true;
            //Ajout des informations de connexion
            smtpClient.Credentials = new System.Net.NetworkCredential(Request.Form["email"],Request.Form["password"]);
            //On indique que l'on envoie le mail par le réseau
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            //On active le protocole SSL
            smtpClient.EnableSsl = true;
            MailMessage mail = new MailMessage();
            //Expéditeur
            mail.From = new MailAddress(Request.Form["email"], "");
            //Destinataire
            mail.To.Add(new MailAddress("karimaelhariri@gmail.com"));
            //Copie
            mail.CC.Add(new MailAddress("karimaelhariri@gmail.com"));
            return View("index");
        }


        private void SMTP(String from,String to,String password, String message ,String subject)
        {
            MailMessage email = new MailMessage();
            // Expéditeur
            email.From = new MailAddress(from);
            // Destinataire
            email.To.Add(new MailAddress(to));
            // Destinataire en copie
            email.CC.Add(new MailAddress(to));
            // Destinataire en copie cache
            email.Bcc.Add(new MailAddress(to));
            // Sujet de l'email
            email.Subject = subject;
            // Corps de l'email en texte
            email.Body = message;

            SmtpClient smtp = new SmtpClient();
            smtp.Credentials = new System.Net.NetworkCredential(from, password);
            // smtp.UseDefaultCredentials = false;
            // smtp.Port = 465;
            smtp.Host = "smtp.gmail.com";
            smtp.EnableSsl = true;
           attachement(email, fichier);
            
          
            smtp.Send(email);
          
        }
      public Attachment attachement(MailMessage email ,string fichier )
        {
            Attachment data = new Attachment(fichier, MediaTypeNames.Application.Octet);

email.Attachments.Add(data);
          
            return data;
        }
    }

}