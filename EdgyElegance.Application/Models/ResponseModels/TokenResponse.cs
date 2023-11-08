using EdgyElegance.Application.Models.BaseModels;

namespace EdgyElegance.Application.Models.ResponseModels {
    public class TokenResponse : BaseResponse {
        public string Token { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;

        public TokenResponse() { }

        public TokenResponse(string token) {
            Token = token;
        }

        public TokenResponse(string token, string refreshToken) : this (token) {
            RefreshToken = refreshToken;
        }
    }
}
