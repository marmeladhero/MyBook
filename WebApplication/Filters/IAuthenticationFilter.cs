namespace WebApplication.Filters
{
    public interface IAuthenticationFilter
    {
        void OnAuthentication(System.Web.Mvc.Filters.AuthenticationContext filterContext);
        void OnAuthenticationChallenge(System.Web.Mvc.Filters.AuthenticationChallengeContext filterContext);
    }
}