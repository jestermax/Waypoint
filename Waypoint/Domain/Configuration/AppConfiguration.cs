using System;

namespace Domain.Configuration
{
    public static class AppConfiguration
    {
        // Unit tests constants
        public static readonly Guid UnitTestsApiTokenId = Guid.Parse("00a6fb6e-9c9a-4754-9fc6-5c3401113314");
        public static readonly Guid UnitTestsHomePlaceId = Guid.Parse("2b148da2-b629-443f-8cbd-98eb14312ea7");
        public static readonly string UnitTestsHomeName = "Unit Tests Home Place";
        public static readonly Guid UnitTestsSchoolPlaceId = Guid.Parse("e71b12ac-3d2a-4518-b635-b0720c26970d");
        public static readonly string UnitTestsSchoolPlaceName = "Unit Tests School Place";
        public static readonly Guid UnitTestsWorkPlaceId = Guid.Parse("c187c70c-a143-4876-b36e-f2a895f0f0c2");
        public static readonly string UnitTestsWorkPlaceName = "Unit Tests Work Place";
        public static readonly Guid UnitTestsFriendPlaceId = Guid.Parse("607be66b-2974-42fe-8432-30874c9d6572");
        public static readonly string UnitTestsFriendPlaceName = "Unit Tests Friend Place";

        // Unit tests account
        public static readonly Guid UnitTestsAccountId = Guid.Parse("283e41e4-228e-42ee-9193-1cde05ae14b0");
        public static readonly string UnitTestsAccountName = "Waypoint Unit Tests";
        public static readonly Guid UnitTestsOtherAccountId = Guid.Parse("35c8cb08-7538-4146-a06e-ffdd7ef39860");
        public static readonly string UnitTestsOtherAccountName = "Waypoint Unit Tests Other";

        // Unit tests user
        public static readonly int UnitTestsUserId = 33;
        public static readonly string UnitTestsFirstName = "Unit";
        public static readonly string UnitTestsLastName = "Tests";
        public static readonly string UnitTestsEmail = "unittests@waypoint.io";
        public static readonly string UnitTestsPassword = "unittests772081";
        public static readonly string UnitTestsApiToken = "VTOXZXHCYOIBKBMBAXMKEJLVVOMINUTNUKIWTAJP";

        // Unit tests friend
        public static readonly int UnitTestsFriendId = 289;
        public static readonly string UnitTestsFriendFirstName = "Unit";
        public static readonly string UnitTestsFriendLastName = "Tests-Friend";
        public static readonly string UnitTestsFriendEmail = "unittestsfriend@waypoint.io";
        public static readonly string UnitTestsFriendPassword = "unittests876124";
        public static readonly string UnitTestsFriendApiToken = "LQZEWYWUSPIGSMYKYUBFURNADQDETIVOZUMZLTQN";

        // Unit tests friend
        public static readonly int UnitTestsNotFriendId = 290;
        public static readonly string UnitTestsNotFriendFirstName = "Unit";
        public static readonly string UnitTestsNotFriendLastName = "Tests-NotFriend";
        public static readonly string UnitTestsNotFriendEmail = "unittestsnotfriend@waypoint.io";
        public static readonly string UnitTestsNotFriendPassword = "unittests569201";
        public static readonly string UnitTestsNotFriendApiToken = "YNTESEZDSTTTNERVNPQTYVMYKKQAEQZCEOKDSCKZ";

        // Global constants
        public static readonly string ConnectionStringName = "WaypointConnection";
        public static readonly string DbContextName = "WaypointContext";
        public static readonly string NotificationEmailAddress = "notifications@waypoint.io";
        public static readonly string NotificationDisplayName = "Waypoint";
        public static readonly string UserTableName = "UserProfile";
        public static readonly string UserIdColumn = "UserId";
        public static readonly string UserNameColumn = "Email";
        public static readonly int CoordinateSystemId = 4326;
        public static readonly string AddressUnavailableMessage = "Address is unavailable";
        public static readonly string ApiTokenHeader = "X-Waypoint-Token";
        public static readonly string ApiRouteName = "DefaultApi";
        public static readonly int ApiTokenDaysIssued = 30;
        public static readonly int ApiTokenLength = 40;
        public static readonly double MinimumLatitude = -90.0;
        public static readonly double MaximumLatitude = 90.0;
        public static readonly double MinimumLongitude = -180.0;
        public static readonly double MaximumLongitude = 180.0;
        public static readonly double MinimumLocationAccuracy = Double.Epsilon;
        public static readonly double MinimumSpeed = 0;
        public static readonly double MaximumSpeed = 300;

        // Account table constants
        public static readonly int AccountNameMinimumLength = 2;
        public static readonly int AccountNameMaximumLength = 250;

        // Account type table constants
        public static readonly Guid FreeAccountTypeId = Guid.Parse("fca93b04-3432-4223-9f6d-668124c385cf");

        // Place table constants
        public static readonly int PlaceNameMaximumLength = 250;
        public static readonly int PlaceDescriptionMaximumLength = 250;

        // UserLocation table constants
        public static readonly int UserLocationAddressMaximumLength = 250;

        // UserProfile table constants
        public static readonly string DefaultUserProfileImage = "default.png";
        public static readonly bool DefaultUserProfileMetric = true;
        public static readonly int FirstNameMinimumLength = 1;
        public static readonly int FirstNameMaximumLength = 50;
        public static readonly int LastNameMinimumLength = 1;
        public static readonly int LastNameMaximumLength = 50;
        public static readonly int MinimumPasswordLength = 5;
        public static readonly int MaximumPasswordLength = 50;

        // Mail templates
        public static readonly string ArrivedAtPlaceTemplate = "arrived_at_place_template.html";
        public static readonly string FriendRequestAcceptedTemplate = "friend_request_accepted_template.html";
        public static readonly string FriendRequestDeniedTemplate = "friend_request_denied_template.html";
        public static readonly string FriendRequestTemplate = "friend_request_template.html";
        public static readonly string LeftPlaceTemplate = "left_place_template.html";
        public static readonly string PaswordResetTemplate = "password_reset_template.html";
        public static readonly string UserRegisteredTemplate = "user_registered_template.html";

        // API constants
        public static readonly string BingApiKey = "ApRJmmqaDrQtczTlBjdUQ8QikmPETCZt5E5Da3ZnRnxVtISy8d0RdirROfB3tJLH";
        public static readonly string MapQuestApiKey = "Fmjtd%7Cluur2102lu%2C7l%3Do5-90yl14";
        public static readonly string MandrillApiKey = "cOcdNHpc5J96UgFi5XfUJQ";
        public static readonly string MandrillHostName = "smtp.mandrillapp.com";
        public static readonly int MandrilSmtpPort = 587;
        public static readonly string MandrillStmpUserName = "adamstirtan@gmail.com";
        public static readonly string EasySmtpHostName = "ssrs.reachmail.net";
        public static readonly int EasySmtpPort = 587;
        public static readonly string EasySmtpUserName = "WAYPOINT\admin";
        public static readonly string EasySmtpPassword = "184M!$Um";
        public static readonly string MailJetHostName = "in.mailjet.com";
        public static readonly string MailJetUserName = "94a1702fa2c316c30b714d3202a99880";
        public static readonly string MailJetPassword = "f8141c4618ca386b8e660a3bf093aa7f";
        public static readonly int MailJetPort = 587;
    }
}
