using System;
using System.Collections.Generic;
using com.tweetapp.Models;

namespace com.tweetapp.DALClasses
{
    interface ITweetAppRepository
    {
        bool ForgotPassword(string email, DateTime DOB, string newPass);
        bool PostTweet(Tweet tweet);
        bool ResetPassword(string email, string oldPass, string newPass);
        UserCred UserLogin(User user);
        bool UserLogout(string email);
        bool UserRegister(UserCred NewUser);
        List<Tweet> GetAllTweets();
        List<UserCred> GetAllUsers();
        List<Tweet> GetUserTweets(string email);
        public bool EmailExists(string email);
    }
}