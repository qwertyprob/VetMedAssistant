namespace Medcard.Client.Services
{

    public class TokenService
    {
        private string _token = "";

        public string GetToken()
        {

            return _token.Replace("\"", "");
        }

        public void SetToken(string token)
        {
            _token = token;
        }

        public void ClearToken()
        {
            _token = null;
        }

        public bool HasToken()
        {
            return !string.IsNullOrEmpty(_token);
        }
    }

    
}
