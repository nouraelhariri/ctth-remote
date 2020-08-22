using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CTTH_MVC.Models
{
    public class User_mail
    {
       public  String email;
       public  String password;
      public   String Nom;
        public String Prenom;
        public String subject;
        public String message;

        public User_mail() { }
        public User_mail(String email, String password,String subject ,String message) {
            this.email = email;
            this.password = password;
        
  
            this.message = message;
            this.subject = subject;

        }
        public User_mail(String email, String password)
        {
            this.email = email;
                this.password = password;
        }



    }
}