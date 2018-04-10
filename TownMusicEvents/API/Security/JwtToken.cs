using Domain.Enums;
using Jose;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace WebAPI.Security
{
    public class JwtToken
    {
        private readonly SecretKey secretKey = null;

        public JwtToken()
        {
            secretKey = new SecretKey();
        }

        public string CreateJwt(String id, int userId, string name, int role, long ttlMillis)
        {
            if (id == null) throw new ArgumentNullException("id");
            if (name == null) throw new ArgumentNullException("subject");
            var currentTime = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
            var expiration = currentTime + ttlMillis;

            var payload = new Dictionary<string, object>()
            {
                {"user_id", userId },
                {"name", name },
                {"exp", expiration},
                {"role", role}
            };

            var key = Encoding.ASCII.GetBytes(secretKey.GetSecretKey());

            var token = Jose.JWT.Encode(payload, key, JwsAlgorithm.HS256);

            return token;
        }


        public static IDictionary<string, object> Decode(string token)
        {
            var payloadJson = Jose.JWT.Payload<IDictionary<string, object>>(token);
            return payloadJson;
        }


        public bool VerifyToken(string token)
        {
            if (token == null) throw new ArgumentNullException("token");
            var key = Encoding.ASCII.GetBytes(secretKey.GetSecretKey());

            try
            {
                var decodeValue = Jose.JWT.Decode(token, key, JwsAlgorithm.HS256);
            }
            catch
            {
                return false;
            }
            return true;
        }

        public int GetIdFromToken(string token)
        {
            if (token == null) throw new ArgumentNullException("token");
            var key = Encoding.ASCII.GetBytes(secretKey.GetSecretKey());

            try
            {
                var decodeValue = Jose.JWT.Decode(token, key, JwsAlgorithm.HS256);
                var result = new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize<dynamic>(decodeValue);
                return result["user_id"];
            }
            catch
            {
                return -1;
            }
        }
    

        public bool VerifyTokenAndRole(string token,int role)
        {
            if (token == null) throw new ArgumentNullException("token");
            var key = Encoding.ASCII.GetBytes(secretKey.GetSecretKey());

            try
            {
                var decodeValue = Jose.JWT.Decode(token, key, JwsAlgorithm.HS256);
                var result = new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize<dynamic>(decodeValue);
                if ((result["role"] != role))
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
            return true;
        }

    }
}