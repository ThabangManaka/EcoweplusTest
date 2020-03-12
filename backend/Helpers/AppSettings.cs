using System;
namespace backend.Helpers

{
   public class AppSettings
    {
        //Properties for JWT Tojen Signature
        public string Site { get; set; }
        public string Audience { get; set; }
        public string ExpireTime { get; set; }
        public string Secret { get; set; }
  
    }
}