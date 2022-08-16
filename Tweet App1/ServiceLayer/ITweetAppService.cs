namespace com.tweetapp.ServiceLayer
{
    interface ITweetAppService
    {
        void WelcomeBoard();
        string MainList();
        void MenuNonLoggedUser();
        string SubList();
    }
}