using System;
using System.Collections.Generic;
using System.Linq;
using com.tweetapp.Models;

namespace com.tweetapp.DALClasses
{
    class TweetAppRepository : ITweetAppRepository
    {
        public TweetAppRepository()
        {

        }
        public bool ForgotPassword(string email, DateTime DOB, string newPass)
        {
            //Get data and use entity framework to access data
            using (var tweetContext = new TweetAppContext())
            {
                var user = tweetContext.UserCreds.FirstOrDefault(u => u.EmailId == email);
                if(user.DateOfBirth == DOB)
                {
                    user.Password = newPass;
                    return true;
                }
                else
                {
                    return false;
                }
            }

        }

        public UserCred UserLogin(User user)
        {
            //Get data and use entity framework to access data
            using (var tweetContext = new TweetAppContext())
            {
                var myUser = tweetContext.UserCreds.FirstOrDefault(u => u.EmailId == user.Email
                                                                        && u.Password == user.Pass);

                if (myUser != null)    //User was found
                {
                    myUser.ActiveStatus = true;
                    tweetContext.SaveChanges();
                    return myUser;
                }
                else    //User was not found
                {
                    return null;
                }
            }
        }

        public bool UserRegister(UserCred NewUser)
        {
            //Get data and use entity framework to access data
                using (var tweetContext = new TweetAppContext())
                {
                    try
                    {
                    NewUser.ActiveStatus = false;
                    tweetContext.UserCreds.Add(NewUser);
                    tweetContext.SaveChanges();
                    return true;
                    }
                    catch(Exception)
                    {
                    return false;
                    }
                } 
        }

        public bool PostTweet(Tweet tweet)
        {
            using (var tweetContext = new TweetAppContext())
            {
                try
                {
                    tweetContext.Tweets.Add(tweet);
                    tweetContext.SaveChanges();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public List<Tweet> GetUserTweets(string email)
        {
            using (var tweetContext = new TweetAppContext())
            {
                var tweets = tweetContext.Tweets.Where(t => t.EmailId == email);
                if(tweets != null)
                {
                    return tweets.ToList<Tweet>();
                }
                else
                {
                    return null;
                }  
            }  
        }

        public List<Tweet> GetAllTweets()
        {
            using (var tweetContext = new TweetAppContext())
            {
                try
                {
                     return tweetContext.Tweets.ToList<Tweet>();
                }
                catch(Exception)
                {
                    return null;
                }
            }
        }

        public List<UserCred> GetAllUsers()
        {
            using (var tweetContext = new TweetAppContext())
            {
                try
                {
                    return tweetContext.UserCreds.ToList<UserCred>();
                }
                catch(Exception)
                {
                    return null;
                }
            }
        }

        public bool ResetPassword(string email, string oldPass, string newPass)
        {
            using (var tweetContext = new TweetAppContext())
            {
                UserCred user = tweetContext.UserCreds.FirstOrDefault(e => e.EmailId == email);

                if(user.Password == oldPass)
                {
                    user.Password = newPass;
                    tweetContext.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public bool UserLogout(string email)
        {
            using (var tweetContext = new TweetAppContext())
            {
                UserCred user = tweetContext.UserCreds.FirstOrDefault(e => e.EmailId == email);

                if (user.ActiveStatus == true)
                {
                    user.ActiveStatus = false;
                    tweetContext.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        public bool EmailExists(string email)
        {
            //Get data and use entity framework to access data
            using (var tweetContext = new TweetAppContext())
            {
                var user = tweetContext.UserCreds.FirstOrDefault(u => u.EmailId == email);
                if (user != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

        }
    }
}


