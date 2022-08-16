using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace com.tweetapp.Models
{
    class Tweet
    {
        public int TweetId { get; set; }
       
        public string TweetText { get; set; }
        public DateTime PostDateAndTime { get; set; }
       
        public string EmailId { get; set; }

    }
}
