using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using com.tweetapp.DALClasses;
using com.tweetapp.Models;

namespace com.tweetapp.ServiceLayer
{
    class TweetAppService : ITweetAppService
    {
        private readonly static string NAME_REGEX = "^[A-Za-z]{2,}$";
        private readonly static string EMAIL_REGEX = "^[A-Za-z0-9]+@[a-zA-Z]+.[a-zA-Z]+$";
        private readonly static string PASSWORD_REGEX = "^[A-Za-z0-9]{3,}$";
        private readonly static string GENDER_REGEX = "^m(ale)?$|^M(ale)?$|^F(emale)?$|^f(emale)?$";


       
        private readonly ITweetAppRepository _tweetAppRepo;

        public TweetAppService(ITweetAppRepository repo)
        {
            this._tweetAppRepo = repo;
        }

        public void WelcomeBoard()
        {
            Console.WriteLine("\t\t\t\t\t\t\t************************************************************\t\t\t\t\t\t");
            Console.WriteLine("\t\t\t\t\t\t\t************************************************************\t\t\t\t\t\t");
            Console.WriteLine("\t\t\t\t\t\t\t\t\tWelcome to Tweet App\n");
            Console.WriteLine("\t\t\t\t\t\t\t************************************************************\t\t\t\t\t\t");
            Console.WriteLine("\t\t\t\t\t\t\t************************************************************\t\t\t\t\t\t");
            Console.WriteLine("\n\t\t\t\t\t\t\t\t...Login and Start posting tweets...\n\n");
        }
        public string MainList()
        {
            string mainListChoice = null;
            Console.WriteLine("\n\t\t\t\t\t\t\t\t1. Login. \n\t\t\t\t\t\t\t\t2. Register. \n\t\t\t\t\t\t\t\t3. Forgot Password. \n\t\t\t\t\t\t\t\t4. Close Application.\n\n");
            Console.WriteLine("What you want to do ?");
            try
            {
                mainListChoice = Console.ReadLine().Trim();
            }
            catch (Exception)
            {
                Console.WriteLine("\n\n\t\t\t\t\t\t\t\t!!! Something went wrong to read input. !!!");
                return null;
            }
            return mainListChoice;
        }

        public string SubList()
        {
            string subListChoice = null;
            Console.WriteLine("\n\t\t\t\t\t\t\t\t1. Post a tweet. \n\t\t\t\t\t\t\t\t2. View my tweets. \n\t\t\t\t\t\t\t\t3. View all tweets. \n\t\t\t\t\t\t\t\t4. View all users. \n\t\t\t\t\t\t\t\t5. Reset password. \n\t\t\t\t\t\t\t\t6. Logout. \n\n");
            Console.WriteLine("\t\t\t\t\t\t\t\tWhat you want to do ?");
            try
            {
                subListChoice = Console.ReadLine().Trim();
            }
            catch (Exception)
            {
                Console.WriteLine("\n\n\t\t\t\t\t\t\t\t!!! Something went wrong to read input. !!!");
                return null;
            }
            return subListChoice;
        }

        public UserCred UserDetails()
        {
            UserCred newUser = new UserCred();
            try
            {
                Console.WriteLine("\nEnter Email Id (eg. mrunal12@gmail.com)...");
                newUser.EmailId = Console.ReadLine().Trim();
                if (!(IsValidEmail(newUser.EmailId)))
                {
                    Console.WriteLine("\n\n\t\t\t\t\t\t\t\t!!! Invalid email provided. !!!");
                    throw new ArgumentException();
                }
                else
                {
                    if(_tweetAppRepo.EmailExists(newUser.EmailId))
                    {
                        Console.WriteLine("\n\n\t\t\t\t\t\t\t\t!!!Email already exist. Please select another one. !!!");
                        throw new ArgumentException();
                    }
                }
                Console.WriteLine("\nEnter First Name (At least 2 character)...");
                newUser.FirstName = Console.ReadLine().Trim();
                if(!(IsValidName(newUser.FirstName)))
                {
                    Console.WriteLine("\n\n\t\t\t\t\t\t\t\t!!! Invalid name provided. !!!");
                    throw new ArgumentException();
                }
                Console.WriteLine("\nEnter Last Name (At least 2 character)...");
                newUser.LastName = Console.ReadLine().Trim();
                if (!(IsValidName(newUser.LastName)))
                {
                    Console.WriteLine("\n\n\t\t\t\t\t\t\t\t!!! Invalid name provided. !!!");
                    throw new ArgumentException();
                }
                Console.WriteLine("\nEnter Gender (Female/Male)...");
                newUser.Gender = Console.ReadLine().Trim();
                if (!(IsValidGender(newUser.Gender)))
                {
                    Console.WriteLine("\n\n\t\t\t\t\t\t\t\t!!! Invalid gender provided. !!!");
                    throw new ArgumentException();
                }
                Console.WriteLine("\nEnter Password (At least 3 character/digit)...");
                newUser.Password = Console.ReadLine().Trim();
                if (!(IsValidPassword(newUser.Password)))
                {
                    Console.WriteLine("\n\n\t\t\t\t\t\t\t\t!!! Invalid password provided. !!!");
                    throw new ArgumentException();
                }
                Console.WriteLine("\nEnter Date of Birth (dd/mm/yyyy)...");
                newUser.DateOfBirth = Convert.ToDateTime(Console.ReadLine().Trim());
                if (!(IsValidPassword(newUser.Password)))
                {
                    Console.WriteLine("\n\n\t\t\t\t\t\t\t\t!!! Invalid password provided. !!!");
                    throw new ArgumentException();
                }
            }
           
            catch(Exception)
            {
                return null;
            }
            return newUser;

        }
        public void MenuNonLoggedUser()
        {
            UserCred loggedUser = null;
            WelcomeBoard();
            try
            {
                string mainListChoice;
                do
                {
                    mainListChoice = MainList();
                    switch (mainListChoice)
                    {
                        case "1":
                            //UserLogin
                            try
                            {
                                User user = new User();
                                Console.WriteLine("\nEnter Your Email Id...");
                                user.Email = Console.ReadLine().Trim();
                                if(_tweetAppRepo.EmailExists(user.Email))
                                {
                                    Console.WriteLine("\nEnter Your Password...");
                                    user.Pass = Console.ReadLine().Trim();
                                }
                                else
                                {
                                    Console.WriteLine("\n\n\t\t\t\t\t\t\t\t!!! You have not registered yet. !!!");
                                    break;
                                }
                                loggedUser = _tweetAppRepo.UserLogin(user);
                                if (loggedUser != null)
                                {
                                    Console.WriteLine($"\t\t\t\t\t\t\t\t*********** Hello {loggedUser.FirstName}  {loggedUser.LastName}. *************\n\n");

                                    //Sub list display
                                    string subListChoice = null;
                                    try
                                    {
                                        do
                                        {
                                            subListChoice = SubList();
                                            switch (subListChoice)
                                            {
                                                case "1":
                                                        //Posting logged in user tweet
                                                    {
                                                        Tweet tweet = new Tweet();
                                                        Console.WriteLine("\nWrite your tweet...");
                                                        tweet.TweetText = Console.ReadLine();
                                                        tweet.PostDateAndTime = DateTime.Now;
                                                        tweet.EmailId = loggedUser.EmailId;
                                                        if (_tweetAppRepo.PostTweet(tweet))
                                                        {
                                                            Console.WriteLine("\t\t\t\t\t\t\t****Your tweet Posted successfully. ****\n\n");
                                                        }
                                                        else
                                                        {
                                                            Console.WriteLine("\n\n\t\t\t\t\t\t\t\t!!! Failed to post your tweet. !!!\n\n");
                                                        }
                                                    }
                                                    break;

                                                case "2":
                                                        //Get logged in user tweets
                                                    {
                                                        List<Tweet> tweets = _tweetAppRepo.GetUserTweets(loggedUser.EmailId);
                                                        if (tweets.Count!=0)
                                                        {
                                                            Console.WriteLine($"\t\t\t\t\t\t\t\t{loggedUser.FirstName} {loggedUser.LastName}, here are your tweets...");
                                                            foreach (Tweet tweet in tweets)
                                                            {
                                                                Console.WriteLine("\n" + tweet.TweetText);
                                                            }
                                                        }
                                                        else
                                                        {
                                                            Console.WriteLine("\n\t\t\t\t\t\t\t\t!!! You haven't posted yet. !!!");
                                                        }
                                                    }
                                                    break;

                                                case "3":
                                                        //Get All users tweets
                                                    {
                                                        List<Tweet> tweets = _tweetAppRepo.GetAllTweets();   
                                                        if (tweets.Count!=0)
                                                        {
                                                            Console.WriteLine("\n\t\t\t\t\t\t\t**** All Posted Tweets. ****\n\n");
                                                            Console.WriteLine("\t\t\t\t\t\t\t\t" + string.Format("{0,-30}|{1,30}", "User", "Post"));
                                                            Console.WriteLine("\t\t\t\t\t\t\t\t-------------------------------------------------------------");
                                                            foreach (Tweet tweet in tweets)
                                                            {
                                                                Console.WriteLine("\t\t\t\t\t\t\t\t" + string.Format("{0,-30}|{1,30}", tweet.EmailId, tweet.TweetText));
                                                                Console.WriteLine($"{tweet.EmailId}\t\t{tweet.TweetText}");
                                                            }
                                                        }
                                                        else
                                                        {
                                                            Console.WriteLine("\t\t\t\t\t\t\t\t!!! No Tweets found. !!!");
                                                        }
                                                    }
                                                    break;

                                                case "4":
                                                        //Get all users
                                                    {
                                                        List<UserCred> users = _tweetAppRepo.GetAllUsers();
                                                        if (users != null)
                                                        {
                                                            Console.WriteLine("\n\t\t\t\t\t\t\t\tAll Users...\n");
                                                            Console.WriteLine("\t\t\t\t\t\t\t\t" + string.Format("{0,-30}|{1,30}","User Email","User Name"));
                                                            Console.WriteLine("\t\t\t\t\t\t\t\t-------------------------------------------------------------");
                                                            foreach (UserCred userShow in users)
                                                            {
                                                                Console.WriteLine("\t\t\t\t\t\t\t\t" + string.Format("{0,-30}|{1,30}",userShow.EmailId,userShow.FirstName+" "+userShow.LastName));
                                                            }
                                                        }
                                                        else
                                                        {
                                                            Console.WriteLine("\t\t\t\t\t\t\t\t!!! No Users found. !!!");
                                                        }
                                                    }
                                                    break;

                                                case "5":
                                                        //Reset password for logged in user
                                                    {
                                                        Console.WriteLine("\nEnter old password...");
                                                        string oldPass = Console.ReadLine().Trim();
                                                        Console.WriteLine("\nEnter new password to reset...");
                                                        string newPass = Console.ReadLine().Trim();
                                                        if (_tweetAppRepo.ResetPassword(loggedUser.EmailId, oldPass, newPass))
                                                        {
                                                            Console.WriteLine("\n\t\t\t\t\t\t\t**** Password reset successfully. ****");
                                                        }
                                                        else
                                                        {
                                                            Console.WriteLine("\t\t\t\t\t\t\t\t!!! Please enter correct old password. !!!");
                                                        }
                                                    }
                                                    break;

                                                case "6":
                                                        //User logout
                                                    {
                                                        if (_tweetAppRepo.UserLogout(loggedUser.EmailId))
                                                        {
                                                            Console.WriteLine("\t\t\t\t\t\t\t\tLogged off...\n\n");
                                                            Console.WriteLine($"\t\t\t\t\t\t\t\t...Thank you {loggedUser.FirstName} {loggedUser.LastName}...\n\n");
                                                            loggedUser = null;
                                                        }
                                                        else
                                                        {
                                                            Console.WriteLine("\t\t\t\t\t\t\t\t!!! User already logged off. !!!");
                                                        }
                                                    }
                                                    break;

                                                default:
                                                    Console.WriteLine("\t\t\t\t\t\t\t\t!!! Please enter number from the list. !!!\n\n");
                                                    break;
                                            }
                                        } while (!(subListChoice.Equals("6")) && !(subListChoice.Equals("null")));
                                    }
                                    catch (Exception)
                                    {
                                        Console.WriteLine("\t\t\t\t\t\t\t\t!!! User can not perform operations. !!! ");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("\t\t\t\t\t\t\t\t!!! Please enter valid credentials and login again. !!!");
                                    break;
                                }
                            }
                            catch (Exception)
                            {
                                Console.WriteLine("\t\t\t\t\t\t\t\t!!! Failed to Login. !!! ");
                            }

                            break;

                        case "2":
                            //UserRegister();

                            UserCred NewUser = UserDetails();
                            if (NewUser != null)
                            {
                                if (_tweetAppRepo.UserRegister(NewUser))
                                {
                                    Console.WriteLine("\t\t\t\t\t\t\t**** Registration Successfull. ****\n\n");
                                }
                                else
                                {
                                    Console.WriteLine("\t\t\t\t\t\t\t\t!!! Failed to Register. !!!\n\n");
                                }
                            }
                            else
                            {
                                Console.WriteLine("\t\t\t\t\t\t\t\t!!! Please Enter valid details. !!!");
                            }
                            break;

                        case "3":
                            //ForgotPassword();
                            try
                            {
                                Console.WriteLine("Enter your Email Id...");
                                string Email = Console.ReadLine().Trim();
                                if (!_tweetAppRepo.EmailExists(Email)) {
                                    Console.WriteLine("\t\t\t\t\t\t\t\t!!! Email Id does not exist. !!!");
                                    break;
                                }
                                Console.WriteLine("Enter Date of Birth (dd/mm/yyyy)...");
                                DateTime DOB = Convert.ToDateTime(Console.ReadLine().Trim());
                                Console.WriteLine("Enter new password...");
                                string newPass = Console.ReadLine().Trim();
                                if (_tweetAppRepo.ForgotPassword(Email, DOB, newPass))
                                {
                                    Console.WriteLine("\n\t\t\t\t\t\t\t\t**** New password set. ****");
                                }
                                else
                                {
                                    Console.WriteLine("\t\t\t\t\t\t\t\t!!! Please enter valid details. !!!");
                                }
                            }
                            catch (Exception)
                            {
                                Console.WriteLine("\t\t\t\t\t\t\t\t!!! Can not set password. !!!");
                            }
                            break;

                        case "4":
                            //Exited from application;
                            Console.WriteLine("\t\t\t\t\t\t\t\t... Thank you for visiting ...\n\n");
                            Console.WriteLine("\t\t\t\t\t\t\t\t.... Developed by Mrunal Patil (2065027) ...\n\n");
                            break;

                        default:
                            Console.WriteLine("\t\t\t\t\t\t\t\t!!! Please enter number from the list. !!!\n\n");
                            break;
                    }
                } while (!(mainListChoice.Equals("4")));
            }
            catch (Exception)
            {
            }
        
        }


        public bool IsValidName(string name)
        {
            return Regex.IsMatch(name, NAME_REGEX);
        }

        public bool IsValidPassword(string pass)
        {
            return Regex.IsMatch(pass, PASSWORD_REGEX);
        }

        public bool IsValidEmail(string email)
        {
            return Regex.IsMatch(email, EMAIL_REGEX);
        }

        public bool IsValidGender(string gender)
        {
            return Regex.IsMatch(gender, GENDER_REGEX);
        }
    }
}
